using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.ExpressionCalculator
{
    class Multiplication : IMathCalc
    {
        public float Result(float _result, float _number)
        {
            _result *= Convert.ToSingle(_number);
            return _result;
        }
    }
}
