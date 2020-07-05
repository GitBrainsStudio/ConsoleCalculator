using ConsoleCalculator.ExpressionCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    class MathCalcContext
    {
        Dictionary<string, IMathCalc> strategy_ = new Dictionary<string, IMathCalc>();

        public MathCalcContext()
        {
            strategy_.Add("+", new Addition());
            strategy_.Add("-", new Subtraction());
            strategy_.Add("/", new Division());
            strategy_.Add("*", new Multiplication());
        }

        public float Result(string _operator, float _result, float _number)
        {
            return strategy_[_operator].Result(_result, _number);
        }
    }
}
