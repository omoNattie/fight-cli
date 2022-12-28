using fight.Source.Classes.Base;

namespace fight.Source.Classes;

public class Player : IBaseEntity
{
    public int Health;
    public readonly int MaxHealth;
    public Player(int health)
    { 
        this.Health = health;
        this.MaxHealth = health;
    }

    public int Attack()
    {
        var rand = new Random();
        if (rand.Next(0, 100) < 2)
            return 999999;
        
        return rand.Next(50, 150);
    }
    
    public bool Defend()
    {
        var rand = new Random();

        var chance = rand.Next(0, 100);

        if (chance < 45)
        {
            Console.WriteLine("\n# You tried to dodge..! You missed.");
            return false;
        }

        Console.WriteLine("\n# You tried to dodge... and you managed to succeed!");
        return true;
    }
}