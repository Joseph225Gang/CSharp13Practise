public partial class Test
{
    // Declaring declaration
    public partial string Name { get; set; }
}

public partial class Test
{
    // implementation declaration:
    private string _name;
    public partial string Name
    {
        get => _name;
        set => _name = value;
    }

    public void Concat<T>(params ReadOnlySpan<T> items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Console.Write(items[i]);
            Console.Write(" ");
        }
        Console.WriteLine();
    }
}