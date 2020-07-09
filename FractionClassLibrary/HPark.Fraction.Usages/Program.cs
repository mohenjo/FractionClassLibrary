using HPark.Fraction;
using System;
using System.Collections.Generic;

namespace HPark.Fraction.Usages
{
    internal class Program
    {
        private static void Main()
        {
            Fraction test;

            // ------ 분수 인스턴스 생성 ------
            // 분자, 분모 지정
            test = new Fraction(1, 2);
            Console.WriteLine(test); // 1/2
            test = new Fraction(0, 7);
            Console.WriteLine(test); // 0
            // 문자열에서 생성
            test = new Fraction("1/5");
            Console.WriteLine(test); // 1/5
            // 정수에서 생성
            test = new Fraction(3);
            Console.WriteLine(test); // 3
            // 소수에서 생성
            test = new Fraction(0.7);
            Console.WriteLine(test); // 7/10
            // 순환소수에서 생성
            test = new Fraction(1.0123, 3);
            Console.WriteLine(test); // 1.0123123123123... = 10113/9990
            // ------

            // ------ 속성 및 멤버 ------
            test = new Fraction(2, 3); // 2/3
            Console.WriteLine($"분자: {test.Numerator}, 분모: {test.Denominator}");
            // ------

            // ------ 인스턴스 매소드 ------
            test = new Fraction("4/6"); // 4/6
            Console.WriteLine($"분수: {test} => 기약분수: {test.Irreducible()}");
            test.ToIrreducible(); // 인스턴스를 기약분수로 바꿈
            Console.WriteLine($"기약분수: {test}"); // 2/3

            test = -test;
            Console.WriteLine(test); // -2/3
            Console.WriteLine(test.Inverse()); // -3/2
            Console.WriteLine(test.Abs()); // 2/3

            test = new Fraction(10, 10);
            Console.WriteLine(test.IsAnInteger());  // True
            test = new Fraction(1, 2);
            Console.WriteLine(test.IsProper());  // True
            test = new Fraction(3, 2);
            Console.WriteLine(test.IsProper());  // False
            test = new Fraction(1, 3);
            Console.WriteLine(test.IsUnit()); // True
            test = new Fraction(2, 3);
            Console.WriteLine(test.IsUnit()); // False
            test = new Fraction(2, 3);
            Console.WriteLine(test.IsReducible()); // False
            test = new Fraction(4, 6);
            Console.WriteLine(test.IsReducible()); // True
            // ------

            // ------ 연산자 오버로딩 및 정적 메소드
            test = new Fraction(7, 8);
            Console.WriteLine($"{test} = {(double)test}"); // ToString()과 명시적 변환(double) 0.875

            Fraction test1 = new Fraction("1/2");
            Fraction test2 = new Fraction(1, 3);
            Console.WriteLine(test1 + test2); // 1/2 + 1/3 = 5/6
            Console.WriteLine(test1 - test2); // 1/2 - 1/3 = 1/6
            Console.WriteLine(test1 * test2); // 1/2 * 1/3 = 1/6
            Console.WriteLine(test1 / test2); // 1/2 / 1/3 = 3/2

            Fraction test3 = new Fraction(2, 6);
            Console.WriteLine(test2 == test3); // True
            Console.WriteLine(test2 != test3); // False
            Console.WriteLine(test1 > test2); // True
            Console.WriteLine(test1 < test2); // False

            (Fraction result1, Fraction result2)
                = Fraction.ReductionToCommonDenominator(test1, test2);
            Console.WriteLine($"{result1} {result2}");  // 1/2, 1/3 => 3/2, 2/6

            test = new Fraction(21, 97);
            List<Fraction> fractions = Fraction.GetEgyptianFraction(test);
            Console.Write($"{test} = ");
            Console.WriteLine(string.Join(" + ", fractions));
            // ------

            Console.ReadKey();
        }
    }
}
