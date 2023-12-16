using BasicCalculator;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var target = new BasicCalculator.BasicCalculator();
var result = target.TestOne("2+2+2+2-9");
Console.WriteLine(result);