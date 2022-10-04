abstract class Armor : IEquipable
{
    public string Name { get; set; }
    public int AC { get; set; }

    public Armor(string name, int ac)
    {
        Name = name;
        AC = ac;
    }

    public void OnEquip(Player player)
    {
        player.BaseAC += AC;
    }

    public void OnUnEquip(Player player)
    {
        player.BaseAC -= AC;
    }

    public abstract override string ToString();

}