using System.ComponentModel;
using System.Globalization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace EconomyScaler
{
    public class EconomyScalerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [LabelKey("$Mods.EconomyScaler.Config.MoneyDropMultiplier.Label")]
        [TooltipKey("$Mods.EconomyScaler.Config.MoneyDropMultiplier.Tooltip")]
        [DefaultValue("1.0")]
        public string MoneyDropMultiplierText { get; set; } = "1.0";

        [LabelKey("$Mods.EconomyScaler.Config.ItemPriceMultiplier.Label")]
        [TooltipKey("$Mods.EconomyScaler.Config.ItemPriceMultiplier.Tooltip")]
        [ReloadRequired]
        [DefaultValue("1.0")]
        public string ItemPriceMultiplierText { get; set; } = "1.0";

        public float GetMoneyDropMultiplier() => SafeToFloat(MoneyDropMultiplierText, 1f);

        public float GetItemPriceMultiplier() => SafeToFloat(ItemPriceMultiplierText, 1f);

        private static float SafeToFloat(string value, float def)
        {
            value = value.Replace(',', '.');

            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            return def;
        }

        private static float ToRange(float value, float min, float max, float def)
        {
            if (value < min || value > max)
            {
                return def;
            }
            return value;
        }

        private void EnsureValidValues()
        {
            var drop = GetMoneyDropMultiplier();
            var price = GetItemPriceMultiplier();

            var validDrop = ToRange(drop, 0.01f, 10_000f, 1f);
            var validPrice = ToRange(price, 0.01f, 10_000f, 1f);

            if (drop != validDrop)
            {
                MoneyDropMultiplierText = validDrop.ToString();
            }

            if (price != validPrice)
            {
                ItemPriceMultiplierText = validPrice.ToString();
            }
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
