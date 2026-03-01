namespace Calc202603;

public class Calculator
{
    public void doCalc(string input, out float result)
    {
        string clearInput ="";
        foreach (var letter in input)
        {
            if (letter == ' ')
            {
                clearInput = clearInput;
            }
            else if (letter == ',')
            {
                clearInput += ".";
            }
            else
            {
                clearInput += letter;
            }
        }
            
        
        
        
        Console.WriteLine($"clearInput = {clearInput}");
        result = 1;
        
    }
}