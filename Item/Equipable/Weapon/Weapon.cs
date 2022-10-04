abstract class Weapon : IEquipable
{
    public string Name { get; set; }
    public int ATT { get; set; }

    public Weapon(string name, int att)
    {
        Name = name;
        ATT = att;
    }
    public virtual void OnEquip(Player player)
    {
        player.ATT = ATT;
    }

    public virtual void OnUnEquip(Player player)
    {
        player.ATT = player.BaseAC;
    }

    public abstract override string ToString();
}