using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.HAL.Network;
using Cosmos.System.Network;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;

namespace FrameOS.Systems.Networking
{
    public static class NetworkSystem
    {

        public static void SetLocalIP()
        {
            using (var xClient = new DHCPClient())
            {
                /** Send a DHCP Discover packet **/
                //This will automatically set the IP config after DHCP response
                if (xClient.SendDiscoverPacket() == -1)
                {
                    Cosmos.HAL.Terminal.WriteLine("DHCP server timed out, trying manual set.");
                    NetworkDevice nic = NetworkDevice.GetDeviceByName("eth0"); //get network device by name
                    IPConfig.Enable(nic, new Address(192, 168, 0, 69), new Address(255, 255, 255, 0), new Address(192, 168, 0, 1));
                    DNSConfig.Add(new Address(8, 8, 8, 8));
                    return;
                }

                NetworkDevice _nic = NetworkDevice.GetDeviceByName("eth0"); //get network device by name
                Address ipAdress = NetworkConfig.Get(_nic).IPAddress;
                Address subnet = NetworkConfig.Get(_nic).SubnetMask;
                Address gateway = DNSConfig.DNSNameservers[0]; // FOR SOME REASON THIS ONE IS THE GATEWAY???

                IPConfig.Enable(_nic, ipAdress, subnet, gateway);

                xClient.Close();
            }
        }

        public static string GetLocalIP()
        {
            return NetworkConfig.CurrentConfig.Value.IPAddress.ToString();
        }

        public static void StartWebServer()
        {
            using (var xServer = new TcpListener(80))
            {
                /** Start server **/
                xServer.Start();

                /** Accept incoming TCP connection **/
                var client = xServer.AcceptTcpClient(); //blocking

                /** Send data **/
                client.Send(Encoding.ASCII.GetBytes("<html>" +
                    "<head>" +
                    "</head>" +
                    "<body>HELLO WORLD AAAAA</body>" +
                    "</html>"));

                client.Close();

            }
        }

        private static TcpClient tcpc = new TcpClient(80);
        private static Address dns = new Address(8, 8, 8, 8);
        private static EndPoint endPoint = new EndPoint(dns, 80);

        public static string Get(string url)
        {
            string httpresponse = "";
            try
            {
                var dnsClient = new DnsClient();
                var tcpClient = new TcpClient(80);

                //Uri uri = new Uri(arguments[0]); Missing plugs

                dnsClient.Connect(DNSConfig.Server(0));
                dnsClient.SendAsk(GetHost(url));
                Address address = dnsClient.Receive();
                dnsClient.Close();

                if (address == null)
                {
                    return "DNS: Could not find " + url;
                }

                tcpClient.Connect(address, 80);

                string httpget = "GET " + GetResource(url) + " HTTP/1.1\r\n" +
                                 "User-Agent: Wurl (CosmosOS)\r\n" +
                                 "Accept: */*\r\n" +
                                 "Accept-Encoding: identity\r\n" +
                                 "Host: " + GetHost(url) + "\r\n" +
                                 "Connection: Keep-Alive\r\n\r\n";

                tcpClient.Send(Encoding.ASCII.GetBytes(httpget));

                var ep = new EndPoint(Address.Zero, 0);
                var data = tcpClient.Receive(ref ep);
                tcpClient.Close();

                httpresponse = Encoding.ASCII.GetString(data);

            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }

            return httpresponse;
        }

        public static string GetHost(string url)
        {
            string newurl = url;
            if (newurl.StartsWith("http://"))
            {
                newurl = newurl.Remove(0, 7);
            }
            else if (newurl.StartsWith("https://"))
            {
                throw new Exception("HTTPS not supported!");
            }
            string[] spliturl = newurl.Split("/");
            return spliturl[0];
        }

        public static string GetResource(string url)
        {
            string newurl = url;
            if (newurl.StartsWith("http://"))
            {
                newurl = newurl.Remove(0, 7);
            }
            else if (newurl.StartsWith("https://"))
            {
                throw new Exception("HTTPS not supported!");
            }
            /*string[] spliturl = newurl.Split("/");
            for (int i = 1; i < spliturl.Length - 1; i++)
            {
                newurl += spliturl[i];
            }
            return newurl;*/

            newurl = newurl.Replace(GetHost(url), "");

            if (!newurl.Contains("/"))
            {
                newurl += '/';
            }

            return newurl;
        }

        public static void GetIP(string hostname)
        {
            DnsClient xClient = new DnsClient();

            Cosmos.HAL.Terminal.WriteLine("Connecting to DNS Server");
            xClient.Connect(new Address(8, 8, 4, 4));

            Cosmos.HAL.Terminal.WriteLine("Asking IP for " + hostname);
            xClient.SendAsk(hostname);

            Cosmos.HAL.Terminal.WriteLine("Waiting for data");

            var addr = xClient.Receive();
            if (addr == null)
            {
                Cosmos.HAL.Terminal.WriteLine("Error: connection timed out");
            }
            else
            {
                Cosmos.HAL.Terminal.WriteLine("Got data: " + addr.ToString());
            }

            xClient.Close();
        }

        public static void ListNetworkInfo()
        {
            if (NetworkStack.ConfigEmpty())
            {
                Cosmos.HAL.Terminal.WriteLine("No network configuration detected!");
            }
            foreach (NetworkDevice device in NetworkConfig.Keys)
            {
                switch (device.CardType)
                {
                    case CardType.Ethernet:
                        Cosmos.HAL.Terminal.Write("Ethernet Card : " + device.NameID + " - " + device.Name);
                        break;
                    case CardType.Wireless:
                        Cosmos.HAL.Terminal.Write("Wireless Card : " + device.NameID + " - " + device.Name);
                        break;
                }
                if (NetworkConfig.CurrentConfig.Key == device)
                {
                    Cosmos.HAL.Terminal.WriteLine(" (current)");
                }
                else
                {
                    Cosmos.HAL.Terminal.NewLine();
                }

                Cosmos.HAL.Terminal.WriteLine("MAC Address          : " + device.MACAddress.ToString());
                Cosmos.HAL.Terminal.WriteLine("IP Address           : " + NetworkConfig.Get(device).IPAddress.ToString());
                Cosmos.HAL.Terminal.WriteLine("Subnet mask          : " + NetworkConfig.Get(device).SubnetMask.ToString());
                Cosmos.HAL.Terminal.WriteLine("Default Gateway      : " + NetworkConfig.Get(device).DefaultGateway.ToString());
                Cosmos.HAL.Terminal.WriteLine("DNS Nameservers      : ");
                foreach (Address dnsnameserver in DNSConfig.DNSNameservers)
                {
                    Cosmos.HAL.Terminal.WriteLine("                       " + dnsnameserver.ToString());
                }
            }
        }

        public static void Ping(string destination)
        {

            destination = destination.Replace("http://", "").Replace("https://", "").Replace("/", "");
            DnsClient xClient = new DnsClient();

            Address dest = Address.Parse(destination);

            if (dest == null)
            {
                //make a DNS request
                xClient.Connect(DNSConfig.Server(0));
                xClient.SendAsk(destination);
                dest = xClient.Receive();
                xClient.Close();

                if (dest == null)
                {
                    Cosmos.HAL.Terminal.WriteLine("ERROR: Cannot find " + destination);
                    return;
                }
            }
            int PacketSent = 0;
            int PacketReceived = 0;
            int PacketLost = 0;
            int PercentLoss;

            try
            {
                Cosmos.HAL.Terminal.WriteLine("Sending ping to " + dest.ToString());

                var _xClient = new ICMPClient();
                _xClient.Connect(dest);

                for (int i = 0; i < 4; i++)
                {
                    _xClient.SendEcho();

                    PacketSent++;

                    var endpoint = new EndPoint(Address.Zero, 0);

                    int second = _xClient.Receive(ref endpoint, 4000);

                    if (second == -1)
                    {
                        Cosmos.HAL.Terminal.WriteLine("Destination host unreachable.");
                        PacketLost++;
                    }
                    else
                    {
                        if (second < 1)
                        {
                            Cosmos.HAL.Terminal.WriteLine("Reply received from " + endpoint.address.ToString() + " time < 1s");
                        }
                        else if (second >= 1)
                        {
                            Cosmos.HAL.Terminal.WriteLine("Reply received from " + endpoint.address.ToString() + " time " + second + "s");
                        }

                        PacketReceived++;
                    }
                }

                _xClient.Close();
            }
            catch (Exception x)
            {
                Cosmos.HAL.Terminal.WriteLine("Ping error: " + x.Message);
            }

            PercentLoss = 25 * PacketLost;

            Cosmos.HAL.Terminal.NewLine();
            Cosmos.HAL.Terminal.WriteLine("Ping statistics for " + dest.ToString() + ":");
            Cosmos.HAL.Terminal.WriteLine("    Packets: Sent = " + PacketSent + ", Received = " + PacketReceived + ", Lost = " + PacketLost + " (" + PercentLoss + "% loss)");
        }
    }
}
