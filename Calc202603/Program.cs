namespace Calc202603;

class Program
{
    static void Main(string[] args)
    {
        string Linput = "-7+4*(2-1)";
        string lOutput = "";
        
        Console.WriteLine("Write what do you want to calc");
        // string equation = Console.ReadLine();
        string equation = Linput;
        double result = 0;
        Console.WriteLine($"User's input:{equation}");
        Calculator calc1 = new Calculator();
        calc1.doCalc(equation, out result);
        Console.WriteLine($"Result: {result}");
    }
}