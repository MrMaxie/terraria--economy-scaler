using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Terraria;
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
        [DefaultValue("1.0")]
        public string ItemPriceMultiplierText { get; set; } = "1.0";

        private float _moneyDropMultiplier = 1f;

        private float _itemPriceMultiplier = 1f;

        public float GetMoneyDropMultiplier() => _moneyDropMultiplier;

        public float GetItemPriceMultiplier() => _itemPriceMultiplier;

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

        private void EnsureValidValues(bool setAllValues = false)
        {
            var lastDrop = GetMoneyDropMultiplier();
            var lastPrice = GetItemPriceMultiplier();

            var newDrop = ToRange(SafeToFloat(MoneyDropMultiplierText, 1f), 0.01f, 10_000f, 1f);
            var newPrice = ToRange(SafeToFloat(ItemPriceMultiplierText, 1f), 0.01f, 10_000f, 1f);

            if (lastDrop != newDrop)
            {
                MoneyDropMultiplierText = newDrop.ToString();
                _moneyDropMultiplier = newDrop;
            }

            if (lastPrice != newPrice)
            {
                ItemPriceMultiplierText = newPrice.ToString();

                if (setAllValues)
                {
                    _itemPriceMultiplier = newPrice;
                }
            }
        }

        public override void OnChanged()
        {
            EnsureValidValues();
        }

        public override void OnLoaded()
        {
            EnsureValidValues(true);
        }

        public override bool NeedsReload(ModConfig pendingConfig)
        {
            try {
                var config = pendingConfig as EconomyScalerConfig;
                return _itemPriceMultiplier != SafeToFloat(config.ItemPriceMultiplierText, 1f);
            } catch {
                return false;
            }
        }
    }
}
