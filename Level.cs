class Level
{
    public Enemy Enemy { get; set; }
    public Item? Item { get; }

    public Level()
    {
        Enemy = new Enemy("", 0, 0);
        Item = null;
    }
    public Level(Enemy e, Item i)
    {
        Enemy = e;
        Item = i;
    }
}