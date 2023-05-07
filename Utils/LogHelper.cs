namespace Utils
{
    public static class LogHelper
    {
        public static void Log(string message, ConsoleColor consoleColor = ConsoleColor.Black)
        {
            if ( consoleColor != ConsoleColor.Black)
            {
                var backup = Console.ForegroundColor;
                Console.ForegroundColor = consoleColor;
                Console.WriteLine(message);
                Console.ForegroundColor = backup;
            }else
            {
                Console.WriteLine(message);
            }
        }
    }
}