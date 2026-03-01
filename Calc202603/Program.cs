namespace Calc202603;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Write what do you want to calc");
        string pp = Console.ReadLine();
        float result = 0;
        Calculator dapofig = new Calculator();
        dapofig.doCalc(pp, out result);
        Console.WriteLine($"result: {result}");
    }
}