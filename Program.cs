using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            Console.WriteLine("Добро пожаловать в калькулятор. \n\n\n");

            while (true)
            {
                try
                {
                    Console.Write("Введите выражение: ");
                    var inputExpression = Console.ReadLine();

                    ExpressionInParentheses(inputExpression).ForEach(v =>
                    {
                        inputExpression = inputExpression.Replace(v.ToString(), Calc(PlusAndMinusCalc(v.Remove(0, 1).Substring(0, v.Length - 2))).ToString());
                    });

                    inputExpression = PlusAndMinusCalc(inputExpression);

                    Console.WriteLine("Результат: " + Calc(inputExpression));
                }
                catch(Exception ex)
                {
                    string errMessage = "Произошла непредвиденная ошибка в приложении. Администрация приложения уже бежит на помощь.";
                    if (ex is FormatException || ex is ArgumentOutOfRangeException ex2)
                    {
                        errMessage = "Введённые данные не корректные! \nВалидный формат: целые и десятично-дробные числа (например: 10,2 через запятую), знаки +, -, *, / и скобки.\n";
                    }

                    Console.WriteLine(errMessage);
                }
            }
        }

        private static List<string> ExpressionInParentheses(string _mathExpression)
        {
            var regex = new Regex(@"\(([^)]*)\)");
            return regex.Matches(_mathExpression).Cast<Match>().Select(m => m.Value).ToList();
        }
        
        static string PlusAndMinusCalc(string _mathExpression)
        {
            var array = _mathExpression.Split("+-)".ToCharArray());

            array.ToList().ForEach(o =>
            {
                if (o.Split("*/".ToCharArray()).Length != 1)
                {
                    var res = Calc(o);
                    _mathExpression = _mathExpression.Replace(o, res.ToString());
                }
            });

            return _mathExpression;
        }

        static float Calc(string _mathExpression)
        {
            MathCalcContext calcContext = new MathCalcContext();

            var operation = ParseOperators(_mathExpression);
            var number = ParseNumbers(_mathExpression);

            float result = Convert.ToSingle(number[0]);

            for (int i = 0; i < operation.Count(); i++)
            {
                result = calcContext.Result(operation[i], result, number[i + 1]);
            }
            return result;
        }

        private static List<string> ParseOperators(string _mathExpression)
        {
            var operators = new List<string>();

            var array = _mathExpression.Split("0123456789".ToCharArray());

            array.ToList().ForEach(operator_ =>
            {
                if (!operator_.Equals("") && !operator_.Equals(" ") && !operator_.Equals(","))
                {
                    operators.Add(operator_);
                }
            });

            return operators;
        }

        private static List<float> ParseNumbers(string _mathExpression)
        {
            var numbers = new List<float>();

            var array = _mathExpression.Split("+-*/()".ToCharArray());

            array.ToList().ForEach(number_ =>
            {
                if (!number_.Equals("") && !number_.Equals(" "))
                {
                    numbers.Add(Convert.ToSingle(number_));
                }
            });

            return numbers;
        }


        //на случай, чтобы консоль не зависла в ожидании. в любом случае, закроется.
        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Критическая ошибка! Приложение закрывается.");
            Environment.Exit(1);
        }

    }



}
