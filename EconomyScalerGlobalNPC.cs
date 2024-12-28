using System;
using Terraria;
using Terraria.ModLoader;

namespace EconomyScaler
{
    public class EconomyScalerGlobalNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            var config = ModContent.GetInstance<EconomyScalerConfig>();
            var multiplier = config.GetItemPriceMultiplier();

            foreach (var entry in shop.Entries)
            {
                if (entry.Item.value <= 0) {
                    continue;
                }

                entry.Item.shopCustomPrice = entry.Item.shopCustomPrice.HasValue
                    ? (int)(entry.Item.shopCustomPrice * multiplier)
                    : entry.Item.value;
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
        {
            var config = ModContent.GetInstance<EconomyScalerConfig>();

            if (npc.value > 0)
            {
                npc.value *= config.GetMoneyDropMultiplier();
            }
        }
    }
}
