using BookItem;
using Model;

public class Book : Item
{
    public int EXP { get; private set; }

    public Book(string id, ItemConfig config) : base(id, config)
    {
        if (config is BookConfig bookConfig)
        {
            EXP = bookConfig.EXP;
        }
        else
        {
            EXP = 0;
        }
    }

    public override void UseItemEffect(Player player)
    {
        player.ReadBook(this);
    }
}
