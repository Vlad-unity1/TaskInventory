using Model;
using PotionItem;

public class Potion : Item
{
    public int HealthAmount { get; private set; }

    public Potion(string id, ItemConfig config) : base(id, config)
    {
        if (config is PotionConfig potionConfig)
        {
            HealthAmount = potionConfig.HealthAmount;
        }
        else
        {
            HealthAmount = 0;  
        }
    }

    public override void UseItemEffect(Player player)
    {
        player.Heal(HealthAmount);
    }
}
