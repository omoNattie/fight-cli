using fight.Source.Classes.Base;

namespace fight.Source.Classes;

public class EnemyPlayer : IBaseEntity
{
    public int Health;

    public EnemyPlayer(int health)
    {
        this.Health = health;
    }

    public int Attack()
    {
        var rand = new Random();
        return rand.Next(47, 134);
    }
    
    public bool Defend()
    {
        var rand = new Random();

        var chance = rand.Next(0, 100);

        if (chance < 45)
        {
            Console.WriteLine("\n# Scorety tries to dodge..! Scorety missed.");
            return false;
        }

        Console.WriteLine("\n# Scorety tries to dodge... and Scorety managed to succeed!");
        return true;
    }
}