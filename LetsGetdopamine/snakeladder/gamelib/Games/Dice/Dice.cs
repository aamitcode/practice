namespace gamelib.V2
{
    public class Dice : IDice
    {
        public int Roll()
        {
            Random rnd = new Random();
            int dice = rnd.Next(1, 7);   
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("___");
            Console.WriteLine($"|{dice}|");
            Console.WriteLine("***");
            Console.ForegroundColor = color;
            return dice;
        }
    }
}
