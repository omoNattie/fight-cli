namespace fight.Source.Classes;

public class Item
{
    public readonly string Name;
    public readonly int HpHeal;

    public Item(string name, int hpHeal)
    {
        this.Name = name;
        this.HpHeal = hpHeal;
    }

    public bool Heal(Player player)
    {
        if (player.Health + HpHeal > player.MaxHealth)
        {
            Console.WriteLine("# You tried to heal.. but you already feel refreshed enough.");
            return false;
        }
        
        player.Health += HpHeal;
        Console.WriteLine($"# You eat your {Name}, you feel replenished!");
        return true;
    }
}