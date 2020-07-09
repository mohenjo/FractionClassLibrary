using System;
using System.Collections.Generic;
using System.Linq;

namespace HPark.Fraction
{
    /// <summary>
    /// 분수(Fraction) 클래스
    /// </summary>
    public class Fraction
    {
        #region 멤버 변수 및 속성

        private int _Numerator;

        /// <summary>
        /// 분자
        /// </summary>
        public int Numerator
        {
            get => _Numerator;
            private set
            {
                if (value < int.MinValue || value > int.MaxValue)
                {
                    throw new OverflowException();
                }
                else
                {
                    _Numerator = value;
                }
            }
        }

        private int _Denominator;

        /// <summary>
        /// 분모
        /// </summary>
        public int Denominator
        {
            get => _Denominator;
            private set
            {
                if (value == 0)
                {
                    throw new DivideByZeroException("분모는 0이 될 수 없습니다.");
                }
                else if (value < int.MinValue || value > int.MaxValue)
                {
                    throw new OverflowException();
                }
                else if (value < 0) // 양의 분모 유지
                {
                    /* 용이한 계산을 위해 이 클래스에서는 항상 양의 분모를 사용함을 가정합니다.
                     * 이를 위해 항상 분자 값이 분모보다 먼저 설정되어야 합니다.
                     */
                    Numerator = -Numerator;
                    _Denominator = -value;
                }
                else
                {
                    _Denominator = value;
                }
            }
        }

        #endregion 멤버 변수 및 속성

        #region 생성자

        /// <summary>
        /// 분수 클래스의 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="numerator">분자</param>
        /// <param name="denominator">분모</param>
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        /// <summary>
        /// 순환소수로부터 분수 클래스의 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="number">소숫점 이하 첫 순환마디까지 표시된 순환소수</param>
        /// <param name="repetendLength">순환마디의 개수(길이)</param>
        public Fraction(double number, int repetendLength)
        {
            string numStr = number.ToString();
            // 소숫점 이하 길이 및 순환 마디의 길이로 부터 양변에 곱할 10^? 크기 구함
            int decimalLenAfterPoint = numStr.Split('.').Last().Length;
            if (repetendLength <= 0 || repetendLength > decimalLenAfterPoint)
            {
                throw new ArgumentException("순환 마디의 길이가 유효하지 않습니다.");
            }
            int nonRepetendLenAfterPoint = decimalLenAfterPoint - repetendLength;
            int powForDecimal = (int)(Math.Pow(10, decimalLenAfterPoint));
            int powForRepentend = (int)(Math.Pow(10, nonRepetendLenAfterPoint));
            // 좌변
            int leftHand = powForDecimal - powForRepentend;
            // 우변
            int righrHand = (int)(number * powForDecimal)
                - (int)(number * powForRepentend);

            Numerator = righrHand;
            Denominator = leftHand;
        }

        /// <summary>
        /// 소수로부터 분수 클래스의 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="number"></param>
        public Fraction(double number)
        {
            string numStr = number.ToString();
            int dotIdx = numStr.IndexOf(".");
            int multiplier = numStr.Length - (dotIdx + 1);
            Numerator = (int)(number * Math.Pow(10, multiplier));
            Denominator = (int)Math.Pow(10, multiplier);
        }

        /// <summary>
        /// 정수로부터 분수 클래스의 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="number"></param>
        public Fraction(int number) : this(number, 1) { }

        /// <summary>
        /// 분수 표현 문자열로부터 분수 클래스의 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="aString"></param>
        public Fraction(string aString)
        {
            string[] parsed = aString.Split('/');
            Numerator = int.Parse(parsed[0]);
            Denominator = int.Parse(parsed[1]);
        }

        #endregion 생성자

        #region 인스턴스 메소드

        /// <summary>
        /// 분수 인스턴스의 기약분수 인스턴스를 반환합니다.
        /// </summary>
        public Fraction Irreducible()
        {
            int numerator = Numerator;
            int denominator = Denominator;
            int gcd = NumericOperation.GCD(numerator, denominator);
            numerator /= gcd;
            denominator /= gcd;
            return new Fraction(numerator, denominator);
        }

        /// <summary>
        /// 분수 인스턴스를 기약분수로 변환합니다.
        /// </summary>
        public void ToIrreducible()
        {
            Fraction fraction = Irreducible();
            Numerator = fraction.Numerator;
            Denominator = fraction.Denominator;
        }

        /// <summary>
        /// 분수 인스턴스의 절대값 인스턴스를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public Fraction Abs()
        {
            return new Fraction(Math.Abs(Numerator), Math.Abs(Denominator));
        }

        /// <summary>
        /// 분수 인스턴스의 역수 인스턴스를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public Fraction Inverse() => new Fraction(Denominator, Numerator);

        /// <summary>
        /// 분수 클래스의 인스턴스를 문자열로 표시합니다.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Numerator == 0 || Denominator == 1)
            {
                return $"{Numerator}";
            }
            else
            {
                return $"{Numerator}/{Denominator}";
            }
        }

        public override int GetHashCode() => Numerator ^ Denominator.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(this, (Fraction)obj);
            return frA.Numerator == frB.Numerator && frA.Denominator == frB.Denominator;
        }

        /// <summary>
        /// 분수 인스턴스가 정수로 표현 가능한지(분자 == 분모)의 여부
        /// </summary>
        /// <returns></returns>
        public bool IsAnInteger() => Numerator == Denominator;

        /// <summary>
        /// 분수 인스턴스가 진분수인지의 여부
        /// </summary>
        /// <returns></returns>
        public bool IsProper() => Numerator < Denominator;

        /// <summary>
        /// 분수 인스턴스가 단위 분수인지의 여부
        /// </summary>
        /// <returns></returns>
        public bool IsUnit() => Numerator == 1;

        /// <summary>
        /// 분수 인스턴스가 약분 가능한지의 여부
        /// </summary>
        /// <returns></returns>
        public bool IsReducible() => NumericOperation.GCD(Numerator, Denominator) > 1;

        #endregion 인스턴스 메소드

        #region 정적 메소드

        /// <summary>
        /// 분수 인스턴스 값 => double 명시적 변환
        /// </summary>
        /// <param name="fraction"></param>
        public static explicit operator double(Fraction fraction)
        {
            return (double)fraction.Numerator / fraction.Denominator;
        }

        /// <summary>
        /// 분수 인스턴스에 대한 + 단항 연산자를 적용합니다.
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static Fraction operator +(Fraction fraction) => fraction;

        /// <summary>
        /// 두 분수 인스턴스에 대해 더하기(+) 연산을 수행합니다.
        /// </summary>
        /// <param name="fractionA"></param>
        /// <param name="fractionB"></param>
        /// <returns></returns>
        public static Fraction operator +(Fraction fractionA, Fraction fractionB)
        {
            int lcm = checked(NumericOperation.LCM(fractionA.Denominator, fractionB.Denominator));
            int numerator = checked(fractionA.Numerator * lcm / fractionA.Denominator
                + fractionB.Numerator * lcm / fractionB.Denominator);
            Fraction result = new Fraction(numerator, lcm);
            return result;
        }

        /// <summary>
        /// 분수 인스턴스에 대한 - 단항 연산자를 적용합니다.
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static Fraction operator -(Fraction fraction)
            => new Fraction(-fraction.Numerator, fraction.Denominator);

        /// <summary>
        /// 두 분수 인스턴스에 대해 빼기(-) 연산을 수행합니다.
        /// </summary>
        /// <param name="fractionA"></param>
        /// <param name="fractionB"></param>
        /// <returns></returns>
        public static Fraction operator -(Fraction fractionA, Fraction fractionB)
        {
            int lcm = checked(NumericOperation.LCM(fractionA.Denominator, fractionB.Denominator));
            int numerator = checked(fractionA.Numerator * lcm / fractionA.Denominator
                - fractionB.Numerator * lcm / fractionB.Denominator);
            Fraction result = new Fraction(numerator, lcm);
            return result;
        }

        /// <summary>
        /// 두 분수 인스턴스에 대해 곱하기(*) 연산을 수행합니다.
        /// </summary>
        /// <param name="fractionA"></param>
        /// <param name="fractionB"></param>
        /// <returns></returns>
        public static Fraction operator *(Fraction fractionA, Fraction fractionB)
        {
            int numerator = checked(fractionA.Numerator * fractionB.Numerator);
            int denominator = checked(fractionA.Denominator * fractionB.Denominator);
            Fraction result = new Fraction(numerator, denominator);
            return result;
        }

        /// <summary>
        /// 두 분수 인스턴스에 대해 나누기(/) 연산을 수행합니다.
        /// </summary>
        /// <param name="fractionA">피젯수 분수 인스턴스</param>
        /// <param name="fractionB">젯수 분수 인스턴스</param>
        /// <returns></returns>
        public static Fraction operator /(Fraction fractionA, Fraction fractionB)
        {
            return fractionA * fractionB.Inverse();
        }

        /// <summary>
        /// 두 분수 인스턴스에 대해 통분된 표현의 분수 인스턴스 튜플을 반환합니다
        /// </summary>
        /// <param name="fractionA"></param>
        /// <param name="fractionB"></param>
        /// <returns></returns>
        public static (Fraction, Fraction) ReductionToCommonDenominator(Fraction fractionA, Fraction fractionB)
        {
            int lcm = checked(NumericOperation.LCM(fractionA.Denominator, fractionB.Denominator));
            Fraction newFractionA = new Fraction(checked(fractionA.Numerator * lcm / fractionA.Denominator), lcm);
            Fraction newFractionB = new Fraction(checked(fractionB.Numerator * lcm / fractionB.Denominator), lcm);
            return (newFractionA, newFractionB);
        }

        public static bool operator ==(Fraction fractionA, Fraction fractionB)
        {
            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(fractionA, fractionB);
            return frA.Numerator == frB.Numerator;
        }

        public static bool operator !=(Fraction fractionA, Fraction fractionB)
        {
            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(fractionA, fractionB);
            return frA.Numerator != frB.Numerator;
        }

        public static bool operator >(Fraction fractionA, Fraction fractionB)
        {
            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(fractionA, fractionB);
            return frA.Numerator > frB.Numerator;
        }

        public static bool operator <(Fraction fractionA, Fraction fractionB)
        {
            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(fractionA, fractionB);
            return frA.Numerator < frB.Numerator;
        }

        public static bool operator >=(Fraction fractionA, Fraction fractionB)
        {
            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(fractionA, fractionB);
            return frA.Numerator >= frB.Numerator;
        }

        public static bool operator <=(Fraction fractionA, Fraction fractionB)
        {
            (Fraction frA, Fraction frB) = ReductionToCommonDenominator(fractionA, fractionB);
            return frA.Numerator <= frB.Numerator;
        }

        /// <summary>
        /// 주어진 분수 인스턴스를 이집트식 분수 표기법(단위 분수의 합)으로 변환합니다.
        /// </summary>
        /// <param name="fraction"></param>
        public static List<Fraction> GetEgyptianFraction(Fraction fraction)
        {
            List<Fraction> fractions = new List<Fraction>();

            Fraction targetFraction = fraction.Irreducible();
            int lastDenominator = 2;
            do
            {
                Fraction checkFraction = new Fraction(1, lastDenominator);
                while (checkFraction >= targetFraction)
                {
                    lastDenominator++;
                    checkFraction.Denominator++;
                }
                fractions.Add(checkFraction);
                targetFraction = (targetFraction - checkFraction).Irreducible();
                lastDenominator++;
            } while (targetFraction.Numerator != 1);
            fractions.Add(targetFraction);
            return fractions;
        }

        #endregion 정적 메소드
    }
}
