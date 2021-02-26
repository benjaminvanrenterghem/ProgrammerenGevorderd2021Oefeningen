using System;

class Program
{
    public delegate void Print(int value);
    public delegate int Calc(int a, int b); // eigen benoemd delegate type

    static int Sum(int x, int y)
    {
        return x + y;
    }

    static void ConsolePrint(int v)
    {
        System.Diagnostics.Debug.WriteLine("Waarde: {0}", v);
    }

    static void Main(string[] args)
    {
        Print printDel = PrintNumber;
        printDel += PrintHexadecimal;
        printDel += PrintMoney;

        printDel(1000);

        printDel -= PrintHexadecimal;
        printDel(2000);

        Func<int, int, int> x1 = Sum; // generieke niet benoemde delegate type Func
        Calc x2 = Sum;

        Action<int> a1 = ConsolePrint; // benoemde method
        a1(5);

        Action<int> a2 = delegate (int v) { System.Diagnostics.Debug.WriteLine("Waarde: {0}", v); }; // anonieme method!
        a2(6);

        var result = x1(10, 10);
        System.Diagnostics.Debug.WriteLine(result);
    }

    public static void PrintNumber(int num)
    {
        System.Diagnostics.Debug.WriteLine("Number: {0,-12:N0}", num);
    }

    public static void PrintMoney(int money)
    {
        System.Diagnostics.Debug.WriteLine("Money: {0:C}", money);
    }

    public static void PrintHexadecimal(int dec)
    {
        System.Diagnostics.Debug.WriteLine("Hexadecimal: {0:X}", dec);
    }

}