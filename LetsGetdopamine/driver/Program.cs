using BasicCalculator;
// See https://aka.ms/new-console-template for more information
var target = new BasicCalculator.BasicCalculator();
string expression="1-1+1";
var result = target.Calculate(expression);
Console.WriteLine($"{expression} => Result: {result}");