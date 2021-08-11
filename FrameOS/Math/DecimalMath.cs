using System;
using System.Collections.Generic;
using System.Text;

namespace FrameOS.MathEX
{
    public static partial class DecimalEx
    {

        /// <summary>
        /// Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
        public static decimal Log10(decimal d)
        {
            if (d < 0) throw new ArgumentException("Logarithm is a complex number for values less than zero!", nameof(d));
            if (d == 0) throw new OverflowException("Logarithm is defined as negative infinity at zero which the Decimal data type can't represent!");

            // Shrink precision from the input value and get bits for analysis
            var parts = decimal.GetBits(d / 1.000000000000000000000000000000000m);
            var scale = (parts[3] >> 16) & 0x7F;

            // Handle special cases of .1, .01, .001, etc.
            if (parts[0] == 1 && parts[1] == 0 && parts[2] == 0)
            {
                return -1 * scale;
            }

            // Handle special cases of powers of 10
            // Note: A binary search was actually found to be faster on average probably because it takes fewer iterations to find no match.
            //       It's even faster than doing a modulus 10 check first.
            if (scale == 0)
            {
                var powerOf10 = Array.BinarySearch(PowersOf10, d);
                if (powerOf10 >= 0)
                {
                    return powerOf10;
                }
            }

            return Log(d) / Ln10;
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
        /// <remarks>
        /// I'm still not satisfied with the speed. I tried several different
        /// algorithms that you can find in a historical version of this 
        /// source file. The one I settled on was the best of mediocrity.
        /// </remarks>
        public static decimal Log(decimal d)
        {
            if (d < 0) throw new ArgumentException("Natural logarithm is a complex number for values less than zero!", "d");
            if (d == 0) throw new OverflowException("Natural logarithm is defined as negative infinity at zero which the Decimal data type can't represent!");

            if (d == 1) return 0;

            if (d >= 1)
            {
                var power = 0m;

                var x = d;
                while (x > 1)
                {
                    x /= 10;
                    power += 1;
                }

                return Log(x) + power * Ln10;
            }

            // See http://en.wikipedia.org/wiki/Natural_logarithm#Numerical_value
            // for more information on this faster-converging series.

            decimal y;
            decimal ySquared;

            var iteration = 0;
            var exponent = 0m;
            var nextAdd = 0m;
            var result = 0m;

            y = (d - 1) / (d + 1);
            ySquared = y * y;

            while (true)
            {
                if (iteration == 0)
                {
                    exponent = 2 * y;
                }
                else
                {
                    exponent = exponent * ySquared;
                }

                nextAdd = exponent / (2 * iteration + 1);

                if (nextAdd == 0) break;

                result += nextAdd;

                iteration += 1;
            }

            return result;

        }
    }

    public static partial class DecimalEx
    {
        /// <summary> The pi (π) constant. Pi radians is equivalent to 180 degrees. </summary>
        /// <remarks> See http://en.wikipedia.org/wiki/Pi </remarks>
        public const decimal Pi = 3.1415926535897932384626433833m;              // 180 degrees - see http://en.wikipedia.org/wiki/Pi
        /// <summary> π/2 - in radians is equivalent to 90 degrees. </summary> 
        public const decimal PiHalf = 1.5707963267948966192313216916m;          //  90 degrees
        /// <summary> π/4 - in radians is equivalent to 45 degrees. </summary>
        public const decimal PiQuarter = 0.7853981633974483096156608458m;       //  45 degrees
        /// <summary> π/12 - in radians is equivalent to 15 degrees. </summary>
        public const decimal PiTwelfth = 0.2617993877991494365385536153m;       //  15 degrees
        /// <summary> 2π - in radians is equivalent to 360 degrees. </summary>
        public const decimal TwoPi = 6.2831853071795864769252867666m;           // 360 degrees

        /// <summary>
        /// Smallest non-zero decimal value.
        /// </summary>
        public const decimal SmallestNonZeroDec = 0.0000000000000000000000000001m; // aka new decimal(1, 0, 0, false, 28); //1e-28m

        /// <summary>
        /// The e constant, also known as "Euler's number" or "Napier's constant"
        /// </summary>
        /// <remarks>
        /// Full value is 2.718281828459045235360287471352662497757, 
        /// see http://mathworld.wolfram.com/e.html
        /// </remarks>
        public const decimal E = 2.7182818284590452353602874714m;

        /// <summary>
        /// The value of the natural logarithm of 10.
        /// </summary>
        /// <remarks>
        /// Full value is: 2.30258509299404568401799145468436420760110148862877297603332790096757
        /// From: http://oeis.org/A002392/constant
        /// </remarks>
        public const decimal Ln10 = 2.3025850929940456840179914547m;
        /// <summary>
        /// The value of the natural logarithm of 2.
        /// </summary>
        /// <remarks>
        /// Full value is: .693147180559945309417232121458176568075500134360255254120680009493393621969694715605863326996418687
        /// From: http://oeis.org/A002162/constant
        /// </remarks>
        public const decimal Ln2 = 0.6931471805599453094172321215m;

        // Fast access for 10^n
        internal static readonly decimal[] PowersOf10 =
        {
            1m,
            10m,
            100m,
            1000m,
            10000m,
            100000m,
            1000000m,
            10000000m,
            100000000m,
            1000000000m,
            10000000000m,
            100000000000m,
            1000000000000m,
            10000000000000m,
            100000000000000m,
            1000000000000000m,
            10000000000000000m,
            100000000000000000m,
            1000000000000000000m,
            10000000000000000000m,
            100000000000000000000m,
            1000000000000000000000m,
            10000000000000000000000m,
            100000000000000000000000m,
            1000000000000000000000000m,
            10000000000000000000000000m,
            100000000000000000000000000m,
            1000000000000000000000000000m,
            10000000000000000000000000000m,
        };
    }
}
