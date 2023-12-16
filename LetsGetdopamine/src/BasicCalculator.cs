namespace BasicCalculator;

public class BasicCalculator
{
    public int TestOne(string mathExpression){
        int leftNumber = 0;
        int rightNumber = 0;
        bool operationFound =false;
        char operation ='+';
        int result =0;
        foreach(var item in mathExpression){
            if(isDigit(item)){
                if(!operationFound){
                    leftNumber = leftNumber*10 + item -'0';
                }
                if(operationFound){
                    rightNumber = rightNumber*10 + item-'0';
                }
            }
            else{
                if(!operationFound){
                    operationFound=true;
                }
                else{
                    leftNumber = performOperation(leftNumber,rightNumber,operation);
                    rightNumber=0;
                }
                operation = item;
            }
        }
       return performOperation(leftNumber,rightNumber,operation);
    }

    public static bool isDigit(char c){
        if('0'<=c && '9'>=c ){
            return true;
        }
        return false;
    }

    public static int performOperation(int leftNumber, int rightNumber, char operation){
         int result=0;
        switch(operation){
            case '+':
            result = leftNumber + rightNumber;
            break;
            case '-':
            result =leftNumber - rightNumber;
            break;
        }
        return result;
    }

}
