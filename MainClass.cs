using BepInEx;
using RoR2;
using Starstorm2.Cores;
using System;
using System.Runtime.CompilerServices;

namespace BalancedObliterateRedux
{
    [BepInDependency("com.TeamMoonstorm.Starstorm2", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.Nebby1999.BalancedObliterateRedux", "BalancedObliterateRedux", "1.1.0")]
    public class MainClass : BaseUnityPlugin
    {
        internal float stageReward;
        internal int stagePenalty;
        internal static bool hasSS2 = false;
        public void Awake()
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.TeamMoonstorm.Starstorm2"))
            {
                hasSS2 = true;
                Logger.LogMessage("Starstorm 2 Found!");
            }
            ConfigLoader.Init(Config);
            if (ConfigLoader.StagePenalty)
            {
                stagePenalty = 5;
            }
            if (ConfigLoader.CalculationMethods == "Redux2.0")
            {
                Run.onServerGameOver += Redux2Method;
            }
            else if (ConfigLoader.CalculationMethods == "Redux")
            {
                Run.onServerGameOver += ReduxMethod;
            }
            else
            {
                Logger.LogWarning(ConfigLoader.CalculationMethodsConfig.Definition + " is not valid! balanced obliterate redux will not do changes.");
            }
        }

        private void Redux2Method(Run run, GameEndingDef gameEndingDef)
        {
            float difficultyMultiplier = DifficultyCatalog.GetDifficultyDef(run.selectedDifficulty).scalingValue;
            Logger.LogMessage("Stages Cleared: " + run.stageClearCount);
            Logger.LogMessage("Loops Cleared: " + run.loopClearCount);
            Logger.LogMessage("This run's Game Ending: " + gameEndingDef.cachedName);
            Logger.LogMessage("----------REWARDS----------");
            stageReward = ConfigLoader.CoinsPerStageBeaten * ((run.stageClearCount - stagePenalty) * run.loopClearCount);
            Logger.LogMessage("reward for Stages Completed & Loops Completed: " + stageReward);
            Logger.LogMessage("Difficulty Multiplier: " + difficultyMultiplier);
            if (gameEndingDef == RoR2Content.GameEndings.StandardLoss)
            {
                Logger.LogMessage("Game Ending is Standard Loss, Aborting...");
                return;
            }
            else if(gameEndingDef == RoR2Content.GameEndings.ObliterationEnding)
            {
                Logger.LogMessage("Game Ending is Obliteration, proceeding to calculate rewards...");
                var LunarCoinAward = (stageReward * difficultyMultiplier);
                Logger.LogMessage("Extra Lunar Coins to Give: " + LunarCoinAward);
                if(LunarCoinAward > 0)
                {
                    uint totalCoins = Convert.ToUInt32(Math.Ceiling(LunarCoinAward));
                    for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                    {
                        NetworkUser networkUser = NetworkUser.readOnlyInstancesList[i];
                        if(networkUser && networkUser.isParticipating)
                        {
                            Logger.LogMessage("Awarding " + totalCoins + " to network user " + networkUser.GetNetworkPlayerName().nameOverride);
                            networkUser.AwardLunarCoins(totalCoins);
                        }
                    }
                }
                else
                {
                    Logger.LogMessage("Extra Lunar Coins to Give is less than 0, Aborting extra coins...");
                }
            }
            else
            {
                Logger.LogMessage("Game ending is not obliteration, Aborting...");
                return;
            }
        }
        private void ReduxMethod(Run run, GameEndingDef gameEndingDef)
        {
            float difficultyMultiplier = GetDifficultyMultiplier(run.selectedDifficulty, hasSS2);
            Logger.LogMessage("Stages Cleared: " + run.stageClearCount);
            Logger.LogMessage("Loops Cleared: " + run.loopClearCount);
            Logger.LogMessage("This run's Game Ending: " + gameEndingDef);
            Logger.LogMessage("----------REWARDS----------");
            stageReward = ConfigLoader.CoinsPerStageBeaten * ((run.stageClearCount - stagePenalty) * run.loopClearCount);
            Logger.LogMessage("Reward for Stages Completed & Loops Completed: " + stageReward);
            Logger.LogMessage("Difficulty Multiplier: " + difficultyMultiplier);
            if (gameEndingDef == RoR2Content.GameEndings.StandardLoss)
            {
                Logger.LogMessage("Game Ending is Standard Loss, Aborting...");
                return;
            }
            else if (gameEndingDef == RoR2Content.GameEndings.ObliterationEnding)
            {
                Logger.LogMessage("Game Ending is Obliteration, proceeding to calculate rewards...");
                var LunarCoinAward = (stageReward * difficultyMultiplier);
                Logger.LogMessage("Extra Lunar Coins to give: " + LunarCoinAward);
                if (LunarCoinAward > 0)
                {
                    uint totalCoins = Convert.ToUInt32(Math.Ceiling(LunarCoinAward));
                    for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                    {
                        NetworkUser networkUser = NetworkUser.readOnlyInstancesList[i];
                        if (networkUser && networkUser.isParticipating)
                        {
                            Logger.LogMessage("Awarding " + totalCoins + " to network user " + networkUser.GetNetworkPlayerName().nameOverride);
                            networkUser.AwardLunarCoins(totalCoins);
                        }
                    }
                }
                else
                {
                    Logger.LogMessage("Extra Lunar Coins to Give is less than 0, Aborting extra coins...");
                }
            }
            else
            {
                Logger.LogMessage("Game ending is not obliteration, Aborting...");
                return;
            }
        }

        public void OnDestroy()
        {
            if (ConfigLoader.CalculationMethods == "Redux2.0")
            {
                Run.onServerGameOver -= Redux2Method;
            }
            else if (ConfigLoader.CalculationMethods == "Redux")
            {
                Run.onServerGameOver -= ReduxMethod;
            }
        }
        private float GetDifficultyMultiplier(DifficultyIndex difficultyIndex, bool hasStarstorm2)
        {
            if(hasStarstorm2)
            {
                var multiplier = GetStarstormDificulty(difficultyIndex);
                return multiplier;
            }
            else
            {
                if (difficultyIndex == DifficultyIndex.Easy)
                {
                    return ConfigLoader.DrizzleMultiplier;
                }
                else if (difficultyIndex == DifficultyIndex.Normal)
                {
                    return ConfigLoader.RainstormMultiplier;
                }
                else if (difficultyIndex == DifficultyIndex.Hard)
                {
                    return ConfigLoader.MonsoonMultiplier;
                }
            }
            return ConfigLoader.RainstormMultiplier;
        }
        private float GetStarstormDificulty(DifficultyIndex difficultyIndex)
        {
            if (difficultyIndex == DifficultyIndex.Easy)
            {
                return ConfigLoader.DrizzleMultiplier;
            }
            else if (difficultyIndex == DifficultyIndex.Normal)
            {
                return ConfigLoader.RainstormMultiplier;
            }
            else if (difficultyIndex == DifficultyIndex.Hard)
            {
                return ConfigLoader.MonsoonMultiplier;
            }
            else if (difficultyIndex == TyphoonCore.diffIdxTyphoon)
            {
                return ConfigLoader.TyphoonMultiplier;
            }
            return ConfigLoader.RainstormMultiplier;
        }
    }
}