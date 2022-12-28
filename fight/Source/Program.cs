using fight.Source.Classes;
using fight.Source.Cli;

namespace fight.Source;

public static class Game
{
    private enum GameState
    {
        Menu,
        Started
    }

    private static int _turn = 1;

    public static void Main(string[] args)
    {
        var game = GameState.Menu;
        
        Handler.Cli(args);
        var name = Handler.Name;
        
        Console.WriteLine("> Starting.. This will be a tough one...");
        Console.WriteLine("\n> Type start to begin the game.\n> Type quit to exit the game");

        while (game == GameState.Menu)
        {
            Console.Write("$ >> ");
            var startInput = Console.ReadLine();
            
            switch (startInput) 
            {
                case "start":
                    game = GameState.Started;
                    Thread.Sleep(200);
                    Console.Clear();
                    break;
                
                case "quit":
                    Environment.Exit(0);
                    break;
                
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
        
        Console.WriteLine("> Type clear to clear the terminal.");
        
        var player = new Player(867);
        var playerItems = new List<Item>
        {
            new("Papaya", 45),
            new("Banana", 21),
            new("Apple", 33),
            new("CinnamonPie", 128),
            new("GoldenApple", 159)
        };
        
        var enemy = new EnemyPlayer(1121);

        while (player.Health >= 0 && enemy.Health >= 0)
        {
            Thread.Sleep(500);
            Console.WriteLine($"\nWhat will you do? - Turn {_turn}");
            Console.WriteLine($$"""
                Attack
                Defend
                Heal - {{name}} won't attack if you heal.

                Check
                """);
            Console.Write($"\n$ {player.Health}hp >> ");
            
            var input = Console.ReadLine();
            var aInput = input?.ToLower();

            switch (aInput)
            {
                case "attack":
                    Console.Clear();

                    var scoreChance = new Random().Next(0, 100);
                    var willDefend = scoreChance < 30;

                    Console.WriteLine("# You decide on attacking!");

                    Thread.Sleep(1000);
                    var dmg = player.Attack();

                    if (!willDefend)
                    {
                        enemy.Health -= dmg;
                        if(enemy.Health < 0)
                            enemy.Health = 0;
                        
                        Console.WriteLine($"# {name} took {dmg} damage!\n# {name} has {enemy.Health} hp left!");

                        Thread.Sleep(500);
                        if (enemy.Health == 0)
                            break;
                        
                        Console.WriteLine($"\n# {name}'s turn!");
                        Thread.Sleep(1000);

                        var newDmg = enemy.Attack();
                        player.Health -= newDmg;
                        if(player.Health < 0)
                            player.Health = 0;

                        Console.WriteLine($"# You took {newDmg} damage!\n# You have {player.Health} hp left!");
                    }
                    else
                    {
                        var enemyDodge = enemy.Defend();

                        if (!enemyDodge)
                            Console.WriteLine($"# {name} took {dmg} damage!\n# {name} has {enemy.Health} hp left!");
                    }

                    break;

                case "defend":
                    Console.Clear();
                    
                    Console.WriteLine("# You decide on defending!");
                    Thread.Sleep(1000);
                    
                    var defendDmg = enemy.Attack();
                    var hasDodged = player.Defend();

                    if (!hasDodged)
                    {
                        player.Health -= defendDmg;
                        Console.WriteLine($"# You took {defendDmg} damage!\n# You have {player.Health} hp left!");
                    }

                    break;
                
                case "heal":
                    Console.Clear();
                    Console.WriteLine("# You decide on healing!");
                    Thread.Sleep(1000);

                    foreach (var t in playerItems)
                        Console.WriteLine($"{t.Name}: {t.HpHeal}");

                    var isDone = false;
                    Thread.Sleep(500);
                    Console.WriteLine("\n# Which will you choose?");
                    Console.Write($"\n$ {player.Health}hp >> ");
                    
                    var itemInput = Console.ReadLine();
                    var aItemInput = itemInput?.ToLower();

                    Thread.Sleep(500);

                    foreach (var t in playerItems.Where(t => aItemInput == t.Name.ToLower()))
                    {
                        var hasHealed = t.Heal(player);
                        if (hasHealed)
                        {
                            Console.WriteLine($"# You have {player.Health} hp!");
                            playerItems.Remove(t);
                        }
                        isDone = true;
                        
                        break;
                    }
                    
                    if(!isDone)
                        Console.WriteLine($"# You search your bag for {itemInput}, but you cant find anything...");
                    break;
                
                case "clear":
                    Console.Clear();
                    _turn--;
                    break;
                
                case "check":
                    Console.Clear();
                    Console.WriteLine($"# {name} has {enemy.Health} hp left!");
                    Console.WriteLine("> You can do this...");

                    _turn--;
                    break;
                
                default:
                    Console.Clear();
                    Console.WriteLine($"You try to {input}, but you have no idea what that is...");
                    break;
            }

            if (player.Health == 0)
            {
                Console.WriteLine("> Despite all your efforts... you died....");
                break;
            }

            if (enemy.Health == 0)
            {
                Console.WriteLine("> {name} has been defeated! Congrats!");
                break;
            }

            _turn++;
            
            if (aInput == "quit") break;
        }
    }
}