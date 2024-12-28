using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EconomyScaler
{
    public class EconomyScalerGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            var config = ModContent.GetInstance<EconomyScalerConfig>();
            var multiplier = config.GetItemPriceMultiplier();

            item.value = (int)(item.value * multiplier);
        }
    }
}
