abstract class Potion : IConsumable
{
    public string Name { get; set; }

    public Potion(string name)
    {
        Name = name;
    }
    public abstract void OnUse(Player player);
    public override string ToString()
    {
        return $"{Name}";
    }
}