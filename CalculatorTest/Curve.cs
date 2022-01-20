using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    public class Curve:INotifyPropertyChanged
    {
        long? a, b;
        long mod;
       

        Point[] points;

        Point zero;

        

        public Curve(int a, int b, int mod)
        {
            this.a = a;
            this.b = b;
            this.mod = mod;
            
            //Console.WriteLine(points.Length);
            //Console.WriteLine((int)((2 * Math.Sqrt(Mod)) + mod));
            //CalculatePoints();
            zero = new Point(null, null, this);
        }

        public long? A
        {
            get { return a; }
            set 
            { 
                a = value;
                OnPropertyChanged(nameof(Delta));
                OnPropertyChanged(nameof(J));
            }
        }

        public long? B
        {
            get { return b; }
            set 
            {
                b = value;
                OnPropertyChanged(nameof(Delta));
                OnPropertyChanged(nameof(J));
            }
        }

        public long Mod
        {
            get { return mod; }
            set 
            { 
                mod = value;
                OnPropertyChanged(nameof(Delta));
                OnPropertyChanged(nameof(J));
            }
        }

      

        public long? Delta
        {
            get
            {
                if (!Algorithms.isPrime(mod)) { return null; }
                long? delta = 0;
                long? a_val = Algorithms.Rest(a, mod);
                long? b_val = Algorithms.Rest(b, mod);
                delta += Algorithms.binPowMod(Algorithms.binMulMod(a_val,Algorithms.mulBack(3,mod),mod),3,mod);
                delta += Algorithms.binPowMod(Algorithms.binMulMod(b_val, Algorithms.mulBack(2, mod), mod), 2, mod);
                delta = Algorithms.Rest(delta, mod);
                return delta;
            }
        }

        public long? J
        {
            get
            {
                if (Delta == 0 || Delta == null) { return null; }
                long? j = 1728 * 4 * Algorithms.binMulMod(a,3,mod);
                j *= Algorithms.mulBack((int)Delta, mod);
                return Algorithms.Rest(j, mod);
            }
        }


        public Point Zero { get { return zero; } }

        public Point[] Points
        {
            get
            {
                return points;
            }
        }

        //private void CalculatePoints()
        //{

        //    order = 0;
        //    points[0] = zero;
        //    order++;

        //    long?[] x_values = new long?[mod];
        //    long?[] y_values = new long?[mod];

        //    Parallel.Invoke(() => calculateXPoints(ref x_values), () => calculateYPoints(ref y_values));

        //    for (long i = 0; i < mod; i++)
        //    {
        //        for (long j = 0; j < mod; j++)
        //        {
        //            if (x_values[i] == y_values[j])
        //            {
        //                Point p = new Point(i, j, this);
        //                points[order] = p;
        //                order++;
        //            }
        //        }
        //    }
        //}

        //private void calculateYPoints(ref long?[] y_values)
        //{
        //    for (int y = 0; y < mod; y++)
        //        y_values[y] = Algorithms.binPowMod(y, 2, mod);
        //    Console.WriteLine("Calculated Y");
        //}


        //private void calculateXPoints(ref long?[] x_values)
        //{
        //    for (int x = 0; x < mod; x++)
        //    {
        //        long? x3 = Algorithms.binPowMod(x, 3, mod);
        //        long? ax = Algorithms.binMulMod(a, x, mod);
        //        x_values[x] = (x3 + ax + b) % mod;
        //    }
        //    Console.WriteLine("Calculated X");
        //}


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(prop));
        }
    }
}
