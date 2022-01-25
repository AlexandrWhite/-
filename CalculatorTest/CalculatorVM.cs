using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    class CalculatorVM:INotifyPropertyChanged
    {
        Curve cvm;
        CalculatorModel cm;

        public CalculatorVM()
        {
            cvm = new Curve(2,3,5);
            cm = new CalculatorModel(cvm);
            cvm.PropertyChanged += Cvm_PropertyChanged;
            cm.Expression = "";
        }

        private void Cvm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Result));
        }

        public Curve Curve
        {
            get { return cvm; }
        }

        public string Expression
        {
            get
            {
                return cm.Expression;
            }
            set
            {
                cm.Expression = value;
                OnPropertyChanged(nameof(Result));
                OnPropertyChanged(nameof(ResultObject));
                OnPropertyChanged(nameof(Expression));
            }
        }

        public string Result
        {
            get
            {
                object result = null;
                try
                {
                    result = cm.calculate();                   
                }               

                catch (SystemException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (result is int) { return result.ToString(); }
                if (result is Point)
                {
                    if ((Point)result != cvm.Zero) { return $"P({(result as Point).X},{(result as Point).Y})"; }
                    else
                    {
                        return "P0";
                    }
                }

                return "";
            }
        }

        public object ResultObject
        {
            get
            {
                object result = null;
                try
                {
                    result = cm.calculate();
                }

                catch (SystemException e)
                {
                    Console.WriteLine(e.Message);
                }
                return result;
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
