
class Character
{

    public string Name { get; set; }
    public int HP { get; set; }

    public int ATT { get; set; }


    public Character(string cName, int cHealth, int cAtt)
    {
        Name = cName;
        HP = cHealth;
        ATT = cAtt;
    }

    public virtual void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP < 0)
        {
            HP = 0;
        }
    }

    public virtual void Heal(int amount)
    {
        HP += amount;
    }
}