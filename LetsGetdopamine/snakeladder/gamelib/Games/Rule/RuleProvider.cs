namespace gamelib.V2
{
    public class RuleProvider : IRuleProvider
    {
        private static int[,] Rules()
        {
            int[,] snakesLadder = { { 97, 78 } ,
                                    { 95,56 },
                                    {88,24 },
                                    { 62,18 },
                                    {48,26 },
                                    {36,6 },
                                    {32,10 },
                                    { 80,99 },
                                    {71,92 },
                                    {50,67 },
                                    {21,42 },
                                    {28,76 },
                                    {1,38 },
                                    {4,14 },
                                    {8,30 } };
            return snakesLadder;
        }

        public dynamic GetRules()
        {
            return Rules();
        }
    }
}
