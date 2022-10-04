class Sword : Weapon
{

    public Sword(string name, int att) : base(name, att)
    {
    }

    public override string ToString()
    {
        return $"{Name} - +{ATT} attack ";
    }
}