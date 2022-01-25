using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    public class Point
    {
        private long? x;
        private long? y;
        private Curve curve;


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var p2 = (Point)obj;
            return (p2.X == x && p2.Y == y && p2.Curve == curve);
        }


        public static bool operator ==(Point p1, Point p2) => p1.Equals(p2);
        public static bool operator !=(Point p1, Point p2) => !p1.Equals(p2);

        public long? X { get { return x; } }
        public long? Y { get { return y; } }
        public Curve Curve { get { return curve; } }

        public Point(long? x, long? y, Curve curve)
        {
            this.x = x;
            this.y = y;
            this.curve = curve;
            if (x != null)
            {
                if (!PointInCurve())
                {
                    throw new Exception("Не существует такой точки в данной кривой");
                }
            }
           
        }


        public static Point operator +(Point p, Point q)
        {
            if (p.Curve != q.Curve) { throw new Exception(); }
            if (p.X == q.X && p.Y != q.Y) { return p.curve.Zero; }
            if (p == p.curve.Zero) { return q; }
            if (q == q.curve.Zero) { return p; }

            long? x3, y3;


            long? lambda;

            if (p != q)
            {
                long? t1 = Algorithms.Rest((q.y - p.y), p.curve.Mod);
                long? t2 = Algorithms.Rest((q.x - p.x), p.curve.Mod);
                long? t3 = Algorithms.mulBack(t2, p.curve.Mod);
                lambda = Algorithms.binMulMod(t3, t1, p.curve.Mod);

                x3 = Algorithms.binPowMod(lambda, 2, p.Curve.Mod);
                x3 = Algorithms.Rest(x3 - p.x, p.Curve.Mod);
                x3 = Algorithms.Rest(x3 - q.x, p.Curve.Mod);
            }
            else
            {

                long? t1 = Algorithms.binPowMod(p.x, 2, p.Curve.Mod);
                long? t2 = Algorithms.binMulMod(t1, 3, p.Curve.Mod);
                long? t3 = Algorithms.Rest((t2 + p.Curve.A), p.Curve.Mod);

                long? t4 = Algorithms.binMulMod(p.y, 2, p.Curve.Mod);
                if (t4 == 0)
                {
                    return p.Curve.Zero;
                }

                long? t5 = Algorithms.mulBack(t4, p.Curve.Mod);

                lambda = Algorithms.binMulMod(t3, t5, p.Curve.Mod);

                x3 = Algorithms.Rest(Algorithms.binPowMod(lambda, 2, p.Curve.Mod) - Algorithms.binMulMod(2, p.x, p.Curve.Mod), p.Curve.Mod);

            }
            y3 = Algorithms.Rest(Algorithms.binMulMod(lambda, Algorithms.Rest(p.x - x3, p.Curve.Mod), p.Curve.Mod) - p.y, p.Curve.Mod);

            return new Point(x3, y3, p.Curve);

        }

        public static Point operator *(int k, Point p)
        {
            Point R = p.Curve.Zero;
            while (k > 0)
            {
                if (k % 2 == 1) { R = R + p; }
                p = p + p;
                k /= 2;
            }
            return R;
        }

        public int Order
        {
            get
            {
                int c = 1;
                Point q = new Point(x, y, curve);
                while (q != curve.Zero)
                {
                    q = this + q;
                    c++;
                }
                return c;
            }
        }

        public void Print()
        {
            Console.WriteLine($"{x} {y}");
        }

        private bool PointInCurve()
        {
            return (Algorithms.binPowMod(y,2,curve.Mod) == Algorithms.Rest( Algorithms.binPowMod(x,3,curve.Mod)+curve.A*x+curve.B,curve.Mod));
        }
    }
}
