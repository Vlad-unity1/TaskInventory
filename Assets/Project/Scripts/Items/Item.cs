using Model;

public abstract class Item
{
    public string Id { get; private set; }
    public ItemConfig Config { get; private set; }

    public Item(string id, ItemConfig config)
    {
        Id = id;
        Config = config;
    }

    public abstract void UseItemEffect(Player player);
}
