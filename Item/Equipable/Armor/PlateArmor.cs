class PlateArmor : Armor
{
    public PlateArmor(string name, int ac) : base(name, ac)
    {

    }

    public override string ToString()
    {
        return $"{Name} - {AC} armor - Plate Armor";
    }
}