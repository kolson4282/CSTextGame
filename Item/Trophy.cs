class Trophy : Item
{
    public string Name { get; set; }
    public Trophy(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return "Trophy: " + Name;
    }
}