using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTest
{
    class CalculatorModel
    {
        string expression;
        Curve cr;
        private static Dictionary<char, int> priority = new Dictionary<char, int>();

        public CalculatorModel(Curve curve)
        {
            priority.Add('(',1);
            priority.Add('+', 2);
            priority.Add('-', 2);
            priority.Add('/', 3);
            priority.Add('*', 3);
            priority.Add(')', 4);
            cr = curve;
            expression = "";
        }

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }      

        public Curve Curve
        {
            get { return cr; }
            set { cr = value; }
        }

        private bool isOperatorChar(char c)
        {
            foreach(char op in priority.Keys)            
                if (op == c) { return true; }            
            return false;
        } 

        private Point read_point(ref int ind)
        {
            StringBuilder bx = new StringBuilder();
            StringBuilder by = new StringBuilder();

            if (expression[ind] == 'P') { ind++; }
            else { throw new Exception("Не верный формат зписи точки кривой"); }

            if (expression[ind] == '0') { ind++; return cr.Zero; }

            if (expression[ind] == '(') { ind++; }
            else { throw new Exception("Не верный формат зписи точки кривой"); }

            while (Char.IsDigit(expression[ind]))
            {
                bx.Append(expression[ind]);
                ind++;
            }

            if (expression[ind] == ',') { ind++; }
            else { throw new Exception("Не верный формат зписи точки кривой"); }

            while (Char.IsDigit(expression[ind]))
            {
                by.Append(expression[ind]);
                ind++;
            }

            if (expression[ind] == ')') { ind++; }
            else { throw new Exception("Не верный формат зписи точки кривой"); }


            return new Point(int.Parse(bx.ToString()), int.Parse(by.ToString()), cr);
        }

        

        public List<object> to_polska()
        {
            List<object> res = new List<object>();
            StringBuilder numBuild = new StringBuilder();        

            Stack<char> operators = new Stack<char>();

            for (int i = 0; i < expression.Length; i++)
            {
                try
                {
                    if (expression[i] == 'P') { res.Add(read_point(ref i)); i--; continue; }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                

                if (Char.IsDigit(expression[i]))
                {
                    numBuild.Append(expression[i]);
                }
                                   

                if(isOperatorChar(expression[i]))
                {
                    if (numBuild.Length != 0)
                    {
                        res.Add(Int32.Parse(numBuild.ToString()));
                        numBuild.Clear();
                    }

                    if('(' == expression[i]) { operators.Push('('); continue; }

                    if(')' == expression[i])
                    {
                        while (operators.Peek() != '(')
                        {
                            res.Add(operators.Pop());                            
                        }
                        operators.Pop();
                        continue;
                    }

                    if (operators.Count == 0) { operators.Push(expression[i]); }
                    else
                    {
                        while (priority[operators.Peek()] >= priority[expression[i]])
                        {
                            res.Add(operators.Pop());
                            if (operators.Count == 0) { break; }
                        }
                        operators.Push(expression[i]);
                    }
                }                
            }

            if (numBuild.Length != 0)
            {
                res.Add(int.Parse(numBuild.ToString()));
            }

            while (operators.Count != 0)
            {
                res.Add(operators.Pop());
            }

            return res;
        }

        public object calculate()
        {
            Stack<object> elements = new Stack<object>();
            foreach (object i in to_polska())
            {               
                if(i is char)
                {
                    var o2 = elements.Pop();
                    var o1 = elements.Pop();                   

                    switch (i)
                    {
                        case '+':
                            if( (o1 is int || o2 is int) && (o1 is Point || o2 is Point)) { 
                                throw new Exception("не определена операция суммы между целым числом и точкой эллептической кривой");
                            }                          
                            break;

                        case '-':
                            if ( (o1 is int || o2 is int) && (o1 is Point || o2 is Point))
                            {
                                throw new Exception("не определена операция разности между целым числом и точкой эллептической кривой");
                                                           }
                            if (o1 is Point && o2 is Point)
                            {
                                throw new Exception("не определена операция вычитания между точкой эллептической кривой и точкой эллептической кривой");
                            }
                            break;

                        case '/':
                            if ( (o1 is int || o2 is int) && (o1 is Point || o2 is Point))
                            {
                                throw new Exception("не определена операция деления между целым числом и точкой эллептической кривой");
                            }

                            if (o1 is Point && o2 is Point)
                            {
                                throw new Exception("не определена операция деления между точкой эллептической кривой и точкой эллептической кривой");
                            }                          
                            break;                            

                        case '*':
                           
                            if (o1 is Point && o2 is Point)
                            {
                                throw new Exception("не определена операция умножения между точкой эллептической кривой и точкой эллептической кривой");
                            }                           
                            break;
                        default:
                            throw new Exception("Ошибочка");
                            
                    }
                    elements.Push(MakeOperation(o1, o2, (char)i));
                }
                else
                {
                    elements.Push(i);
                }
            }
            return elements.Peek();
        }


        private object MakeOperation(object op1, object op2, char operation)
        {           

            if(op1 is Point && op2 is Point && operation=='+') 
            {                
                return (Point)op1 + (Point)op2;
            }

            if (op1 is int && op2 is Point && operation == '*')
            {
                return (int)op1 * (Point)op2;
            }

            if (op2 is int && op1 is Point && operation == '*')
            {
                return (int)op2 * (Point)op1;
            }

            if (op2 is int && op1 is int && operation == '*')
            {
                return (int)op2 * (int)op1;
            }

            if (op2 is int && op1 is int && operation == '+')
            {
                return (int)op2 + (int)op1;
            }

            if (op2 is int && op1 is int && operation == '/')
            {
                return (int)op1 / (int)op2;
            }

            if (op2 is int && op1 is int && operation == '-')
            {
                return (int)op1 - (int)op2;
            }
            throw new Exception("Не определена операция");
        }
    }
}
