using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPark.Fraction
{
    public static class NumericOperation
    {
        /// <summary>
        /// 두 정수의 최대 공약수(GCD, Greatest Common Divisor)를 구합니다.
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static int GCD(int numberA, int numberB)
        {
            while (numberB != 0)
            {
                int temp = numberA % numberB;
                numberA = numberB;
                numberB = temp;
            }
            return numberA;
        }

        /// <summary>
        /// 두 정수의 최소 공배수(LCM, Least Common Multiple)를 구합니다.
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <returns></returns>
        public static int LCM(int numberA, int numberB)
        {
            return checked(numberA * numberB / GCD(numberA, numberB));
        }
    }
}
