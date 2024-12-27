using Terraria;
using Terraria.ModLoader;

namespace EconomyScaler
{
    public class EconomyScalerGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            var config = ModContent.GetInstance<EconomyScalerConfig>();

            item.value = (int)(item.value * config.GetItemPriceMultiplier());
        }
    }
}
