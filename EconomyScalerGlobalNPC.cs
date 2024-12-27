using System;
using Terraria;
using Terraria.ModLoader;

namespace EconomyScaler
{
    public class EconomyScalerGlobalNPC : GlobalNPC
    {
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
