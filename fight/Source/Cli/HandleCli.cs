namespace fight.Source.Cli;

public static class Handler
{
    public static string Name = "Natalie";
    
    public static void Cli(string[] args)
    {
        var helpString = """
        Usage:
            fight --start [name]

        Example:
            fight --start Foo

        Options:
            --start    Starts the fight with the specifid name.
            --help     The help command.
        """;
        
        if (args.Length == 0)
        {
            Console.WriteLine(helpString);
            Environment.Exit(1);
        }

        if (args[0] == "--help")
        {
            Console.WriteLine(helpString);
            Environment.Exit(1);
        }


        if (args.Length != 2)
        {
            Console.WriteLine("$ Usage: fight --start <name>");
            Console.WriteLine("$ Defaulting to name \"Natalie\"");
            
            Thread.Sleep(2000);
            Console.Clear();
            
            return;
        }

        if (args[0] != "--start")
        {
            Console.WriteLine("$ Usage: Fight --start <name>");
            Environment.Exit(1);
        }

        Name = args[1];    
    }
}