using System;

class CustomConverter
{
    public void ConvertToInt(string input, out int result)
    {
        result = System.Convert.ToInt32(input);
    }

    public void ConvertToDouble(string input, out double result)
    {
        result = System.Convert.ToDouble(input);
    }

    public void ConvertToBool(string input, out bool result)
    {
        result = System.Convert.ToBoolean(input);
    }
}

class Program
{
    static void Main(string[] args)
    {
        CustomConverter converter = new CustomConverter();

        string input1 = "342344";
        converter.ConvertToInt(input1, out int intResult);
        Console.WriteLine($"Converted '{input1}' to int: {intResult}");

        string input2 = "154,76";
        converter.ConvertToDouble(input2, out double doubleResult);
        Console.WriteLine($"Converted '{input2}' to double: {doubleResult}");

        string input3 = "true";
        converter.ConvertToBool(input3, out bool boolResult);
        Console.WriteLine($"Converted '{input3}' to bool: {boolResult}");
    }
}
