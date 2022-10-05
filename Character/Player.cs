class Player : Character
{
    public int MaxHealth { get; set; }
    public int BaseAC { get; set; }
    public int BaseAttack { get; set; }
    public List<Item> Inventory { get; }
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }

    public Player(string name) : base(name, 100, 5)
    {
        MaxHealth = 100;
        BaseAC = 0;
        BaseAttack = 5;
        Inventory = new List<Item>();
        Armor = new PlateArmor("Normal Clothes", 0);
        Weapon = new Sword("Dagger", 5);
    }

    public string Stats()
    {
        return $"{Name}\nHealth: {HP} / {MaxHealth}\nArmor: {BaseAC} - {Armor.Name}\nAttack: {ATT} - {Weapon.Name} ";
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg - BaseAC);
    }

    public override void Heal(int amount)
    {
        HP += amount;
        if (HP > MaxHealth)
        {
            HP = MaxHealth;
        }
    }
    public void UseItem(Item item)
    {
        Console.WriteLine($"Using {item}");
        switch (item)
        {
            case IConsumable c:
                ConsumeItem(c);
                break;
            case Weapon w:
                EquipWeapon(w);
                break;
            case Armor a:
                EquipArmor(a);
                break;
        }
        Inventory.Remove(item);
    }

    private void ConsumeItem(IConsumable item)
    {
        item.OnUse(this);
    }

    private void EquipWeapon(Weapon item)
    {
        if (Weapon != null)
        {
            Weapon.OnUnEquip(this);
            Inventory.Add(Weapon);
        }
        Weapon = item;
        item.OnEquip(this);
    }

    public void DropItem(Item item)
    {
        Inventory.Remove(item);
    }
    private void EquipArmor(Armor item)
    {
        if (Armor != null)
        {
            Armor.OnUnEquip(this);
            Inventory.Add(Armor);
        }
        Armor = item;
        item.OnEquip(this);
    }
}