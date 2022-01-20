using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    class Algorithms
    {
        public static long? binMulMod(long? a, long? b, long? mod)
        {
            if (b == 1) { return a % mod; }
            if (b == 0) { return 0; }
            if (b % 2 == 0) { long? x = binMulMod(a, b / 2, mod); return (2 * x) % mod; }
            return (binMulMod(a, b - 1, mod) + a) % mod;
        }

        public static long? binPowMod(long? x, long? n, long? mod)
        {
            if (n == 0) { return 1; }
            if (n % 2 == 1) { return binMulMod(x, binPowMod(x, n - 1, mod), mod) % mod; }
            long? t = binPowMod(x, n / 2, mod);
            return binMulMod(t, t, mod);
        }

        public static bool isPrime(long? n)
        {
            if (n == 1) { return false; }
            for (long? i = 2; i * i <= n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        public static long? gcdEx(long? a, long? b, ref long? x, ref long? y)
        {
            if (a == 0) { x = 0; y = 1; return b; }
            long? x1 = 0;
            long? y1 = 0;
            long? d = gcdEx(b % a, a, ref x1, ref y1);
            x = y1 - (long?)(b / a) * x1;
            y = x1;
            return d;
        }


        public static long? Rest(long? a, long? b)
        {
            return a - Divide((long)a, (long)b) * b;
        }

        private static long? Divide(long a, long b)
        {
            int k = 1;
            if (a < 0 && b >= 0 || a >= 0 && b < 0) { k = -1; }
            int t = 0;
            if (a < 0) { t++; }
            return k * ((Math.Abs(a) / Math.Abs(b)) + t);
        }


        public static long? mulBack(long? a, long? mod)
        {
            if (a == 0) { throw new Exception("Нельзя найти мультипликативное обратное к нулю"); }

            if (isPrime(mod))
            {
                long? x1, x2;
                x1 = 0;
                x2 = 0;
                gcdEx(a, mod, ref x1, ref x2);
                return Rest(x1, mod);
            }

            throw new Exception("Модуль не является простым");
        }
    }
}