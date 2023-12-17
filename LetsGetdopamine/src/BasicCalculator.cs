namespace BasicCalculator;

using System.Collections;
using System.Globalization;

public class BasicCalculator
{
    public int Calculate(string mathExpression){
        mathExpression = mathExpression.Replace(" ","");
        var stack = new Stack<int>();
        int number=0;
        char sign='+';
        for(int i=0;i<mathExpression.Length;i++){
            char c =mathExpression[i];
            if(IsDigit(c)){
                number = number *10 + mathExpression[i] -'0';
            }
            if (i + 1 == mathExpression.Length || (c == '+' || c == '-' || c == '*' || c == '/')){
                switch(sign){
                    case '+':
                        stack.Push(number);
                        break;
                    case '-':
                        stack.Push(-1*number);
                        break;
                    case '*':
                        var result = stack.Pop()*number;
                        stack.Push(result);
                        break ;
                    case '/':
                        var result1 = stack.Pop() / number;
                        stack.Push(result1);
                        break ;
                }
                number=0;
                sign = mathExpression[i];  
            }
        }
        return SumStack(stack);
    }
    private static bool IsDigit(char c){
        return '0'<=c && '9'>=c ;
    }
    private static int SumStack(Stack<int> stack){
        int finalResult=0;
        while(stack.Count>0){
            finalResult +=stack.Pop();
        }
        return finalResult;
    }
}