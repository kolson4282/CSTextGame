class HealthPotion : Potion
{
    public int Amount { get; set; }

    public HealthPotion(string name, int amount) : base(name)
    {
        Amount = amount;
    }

    public override void OnUse(Player player)
    {
        player.Heal(Amount);
    }

    public override string ToString()
    {
        return $"{Name} - Heals {Amount} HP";
    }
}