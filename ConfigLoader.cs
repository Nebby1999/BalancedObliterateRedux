using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedObliterateRedux
{
    internal class ConfigLoader
    {
        internal static ConfigEntry<float> CoinsPerStageBeatenConfig { get; set; }
        public static float CoinsPerStageBeaten => CoinsPerStageBeatenConfig.Value;
        internal static ConfigEntry<bool> StagePenaltyConfig { get; set; }
        public static bool StagePenalty => StagePenaltyConfig.Value;
        internal static ConfigEntry<float> DrizzleMultiplierConfig { get; set; }
        public static float DrizzleMultiplier => DrizzleMultiplierConfig.Value;
        internal static ConfigEntry<float> RainstormMultiplierConfig { get; set; }
        public static float RainstormMultiplier => RainstormMultiplierConfig.Value;
        internal static ConfigEntry<float> MonsoonMultiplierConfig { get; set; }
        public static float MonsoonMultiplier => MonsoonMultiplierConfig.Value;
        internal static ConfigEntry<float> TyphoonMultiplierConfig { get; set; }
        public static float TyphoonMultiplier => TyphoonMultiplierConfig.Value;
        internal static ConfigEntry<bool> EnableFallBackConfig { get; set; }
        public static bool EnableFallBack => EnableFallBackConfig.Value;

        public static void Init(ConfigFile config)
        {
            CoinsPerStageBeatenConfig = config.Bind<float>("1 - General Settings", "Coins Per Stage Beaten", 0.25f, "How many extra coins you get per stage beaten past Stage 8\nThis number is the base number of the mathematical equation which dictates how many extra coins you get when obliterating.");
            StagePenaltyConfig = config.Bind<bool>("1 - General Settings", "Coin Per Stage Starts Past Stage 5", true, "By default, the mod starts counting the Coins Per Stage Beaten once you first beat the boss of the Primordial Teleporter\nThis is to encourage players to go past stage 8 and obliterating later for bigger rewards.");

            DrizzleMultiplierConfig = config.Bind<float>("2 - Difficulty Multipliers", "Drizzle Multiplier", 1.0f, "The multiplier applied to the amount of stages beaten.");
            RainstormMultiplierConfig = config.Bind<float>("2 - Difficulty Multipliers", "Rainstorm Multiplier", 1.5f, "The multiplier applied to the amount of stages beaten.");
            MonsoonMultiplierConfig = config.Bind<float>("2 - Difficulty Multipliers", "Monsoon Multiplier", 2.0f, "The multiplier applied to the amount of stages beaten.");
            TyphoonMultiplierConfig = config.Bind<float>("2 - Difficulty Multipliers", "Typhoon Multiplier", 2.5f, "The multiplier applied to the amount of stages beaten.");
        }
    }
}
