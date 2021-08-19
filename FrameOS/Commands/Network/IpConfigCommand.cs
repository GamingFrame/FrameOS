using Cosmos.HAL;
using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using FrameOS.Systems.CommandSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.Commands
{
    class IpConfigCommand : ICommand
    {
        public string description => "Configurate your IP Configuration";

        public string command => "ipconfig";

        public void Run(CommandArg[] commandArgs)
        {
            if (commandArgs.Length < 0)
            {
                throw new Exception("Invalid arguments. Use ipconfig /help to see the usage.");
            }
            switch (commandArgs[0].String)
            {
                case "/release":
                    DHCPClient client = new DHCPClient();
                    client.SendReleasePacket();
                    client.Close();
                    client.Dispose();
                    Terminal.WriteLine("Successfully released IP Address");
                    break;
                case "/ask":
                    DHCPClient _client = new DHCPClient();
                    if (_client.SendDiscoverPacket() == -1)
                    {
                        throw new Exception("can't ask the DHCP server for a new IP, please set it manually");
                    }
                    _client.Close();
                    _client.Dispose();
                    NetworkDevice _nic = NetworkDevice.GetDeviceByName("eth0");
                    if (NetworkConfig.Get(_nic).DefaultGateway == new Address(0,0,0,0))
                    {
                        Address ipAdress = NetworkConfig.Get(_nic).IPAddress;
                        Address subnet = NetworkConfig.Get(_nic).SubnetMask;
                        Address gateway = DNSConfig.DNSNameservers[0];
                        IPConfig.Enable(_nic, ipAdress, subnet, gateway);
                    }
                    Terminal.WriteLine("Successfully received IP Address " + NetworkConfig.Get(_nic).IPAddress);
                    break;
                case "/set":
                    //if (commandArgs.Length != 4)
                    //{
                    //    throw new Exception("Invalid arguments. Use ipconfig /help to see the usage.");
                    //}

                    //NetworkDevice nic = NetworkDevice.GetDeviceByName("eth0");
                    //Address ip = Address.Zero;
                    //Address _subnet = Address.Zero;
                    //Address _gateway = Address.Zero;
                    //try
                    //{
                    //    ip = Address.Parse(commandArgs[2].String);
                    //    _subnet = Address.Parse(commandArgs[3].String);
                    //    _gateway = Address.Parse(commandArgs[4].String);
                    //}
                    //catch (Exception)
                    //{

                    //    throw new Exception("t");
                    //}


                    //IPConfig.Enable(nic, ip, _subnet, _gateway);
                    //Terminal.WriteLine("Set IP Adress to " + ip);

                    throw new Exception("Feature not working yet!");

                    break;

                case "/help":
                    Terminal.WriteLine("Description: " + description);
                    Terminal.NewLine();
                    Terminal.WriteLine("Usage:");
                    Terminal.WriteLine("- ipconfig /release     Tell the DHCP Server to release the IP Address");
                    Terminal.WriteLine("- ipconfig /ask         Ask the DHCP Server for a new IP Address");
                    Terminal.WriteLine("- ipconfig /set         Manualy set the IP Address");
                    Terminal.WriteLine("    - ipconfig /set {IPv4} {Subnet} {Gateway}");

                    break;
                default:
                    throw new Exception("Invalid arguments. Use ipconfig /help to see the usage.");
            }
        }
    }
}
