using Terraria.ModLoader.Config;

namespace EconomyScaler
{
    public class EconomyScalerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [LabelKey("$Mods.EconomyScaler.Config.MoneyDropMultiplier.Label")]
        [TooltipKey("$Mods.EconomyScaler.Config.MoneyDropMultiplier.Tooltip")]
        [Range(0.1f, 25f)]
        public float MoneyDropMultiplier { get; set; }

        [LabelKey("$Mods.EconomyScaler.Config.ItemBuyPriceMultiplier.Label")]
        [TooltipKey("$Mods.EconomyScaler.Config.ItemBuyPriceMultiplier.Tooltip")]
        [Range(0.1f, 25f)]
        public float ItemBuyPriceMultiplier { get; set; }

        [LabelKey("$Mods.EconomyScaler.Config.ItemSellPriceMultiplier.Label")]
        [TooltipKey("$Mods.EconomyScaler.Config.ItemSellPriceMultiplier.Tooltip")]
        [Range(0.1f, 25f)]
        public float ItemSellPriceMultiplier { get; set; }

        private static float ToRange(float value, float min, float max, float? def)
        {
            if (value < min)
            {
                return def ?? min;
            }
            if (value > max)
            {
                return def ?? max;
            }
            return value;
        }

        private void EnsureValidValues()
        {
            MoneyDropMultiplier = ToRange(MoneyDropMultiplier, 0.1f, 25f, 1f);
            ItemBuyPriceMultiplier = ToRange(ItemBuyPriceMultiplier, 0.1f, 25f, 1f);
            ItemSellPriceMultiplier = ToRange(ItemSellPriceMultiplier, 0.1f, 25f, 1f);
        }

        public override void OnChanged()
        {
            EnsureValidValues();
        }

        public override void OnLoaded()
        {
            EnsureValidValues();
        }

        public override bool NeedsReload(ModConfig pendingConfig) => false;
    }
}
