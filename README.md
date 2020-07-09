# 분수 클래스 | Fraction Class

*C# 분수 계산을 위한 .NET framework 클래스 라이브러리*



## Description

### Solution structure

+ `HPark.Fraction`:  분수 클래스 (.NET Framework  클래스 라이브러리 프로젝트)
+ `HPark.Fraction.Usages`:  분수 클래스 사용 예제 (.NET Framework 콘솔앱 프로젝트)

### Classes, members and methods

+ `Using Hpark.Fraction`

+ `Fraction` 클래스: 분수 클래스
  
  + 생성자:
    ```c#
    new Fraction(int 분자, int 분모)
    new Fraction(int 정수)
    new Fraction(double 소수)
    new Fraction(double 무한소수, int 반복마디길이)
    ```

  + 멤버 변수/속성
    ```c#
    int Numerator // 분자
    int Denominator // 분모
    ```
    
  + 인스턴스 메소드:
    ```c#
    Fraction Irreducible() // 기약 분수 반환
    void ToIrreducible() // 기약 분수로 변환
    Fraction Abs() // 분수의 절대값
    Fraction Inverse() // 분수의 역수
    string ToString() // 분수의 문자열 표시
    bool IsAnInteger() // 정수로 표현 가능 여부
    bool IsProper() // 진분수인지 확인
    bool IsUnit() // 단위분수인지 확인
    bool IsReducible() // 약분가능한지 확인
    ```
    
  + 지원 연산자:
    + Unary: `+`, `-`
    + Binary: `+`, `-`, `*`, `/`
    + 비교연산: `==`, `!=`, `>`, `<`, `>=`, `<=`
  
  + 정적 메소드:
    ```c#
    (Fraction, Fraction) ReductionToCommonDenominator(Fraction fractionA, Fraction fractionB) // 두 분수의 통분된 인스턴스 튜플
    List<Fraction> GetEgyptianFraction(Fraction fraction) // 이집트식 분수 표기법(단위분수의 합)으로 변환
    ```



## Project Info

### Version

- Version 1.1905

### Dev Tools

+ [C#](https://docs.microsoft.com/ko-kr/dotnet/csharp/)
+ [Microsoft Visual Studio Community Edition](https://visualstudio.microsoft.com/ko/)

### Environments

+ Test Environment

    + Microsoft Windows 10 (x64)
    + .NET Framework 4.7.2 +

+ Dependencies / 3rd-party package(s)

    + None




## License

+ [MIT License](https://github.com/mohenjo/FractionClassLibrary/blob/master/LICENSE)
