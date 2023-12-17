namespace test;
using BasicCalculator;

[TestClass]
public class BasicCalculatorTest
{
    [TestMethod]
    public void BaseTest()
    {
        var target = new BasicCalculator();
        int firstNumber = 18;
        int secondNumber =2;
        int thirdNumber=4;
        int forthNumber =5;
        var result = target.Calculate($"{firstNumber}+{secondNumber}+{thirdNumber}-{forthNumber}");
        Assert.AreEqual(firstNumber+secondNumber+thirdNumber-forthNumber,result);
    }
}