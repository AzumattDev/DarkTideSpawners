using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ServerSync;
using UnityEngine;

namespace DarkTideSpawners
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class DTSpawners : BaseUnityPlugin
    {
        internal const string ModName = "DarkTideSpawners";
        internal const string ModVersion = "2.0.0";
        internal const string ModGuid = "azumatt.DarkTideSpawners";
        private static string _configFileName = ModGuid + ".cfg";
        private static string _configFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + _configFileName;

        public static readonly ManualLogSource DarkTideLogger = BepInEx.Logging.Logger.CreateLogSource(ModName);


        private static readonly ConfigSync ConfigSync = new(ModGuid)
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };


        private void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Harmony harmony = new(ModGuid);


            _serverConfigLocked = Config("General", "Force Server Config", true, "Force Server Config");
            _ = ConfigSync.AddLockingConfigEntry(_serverConfigLocked);

            InitSpawnerConfigs();
            Assets.LoadAssets();
            harmony.PatchAll(assembly);
            SetupWatcher();
        }

        private void OnDestroy()
        {
            base.Config.Save();
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, _configFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(_configFileFullPath)) return;
            try
            {
                DarkTideLogger.LogDebug("ReadConfigValues called");
                base.Config.Reload();
            }
            catch
            {
                DarkTideLogger.LogError($"There was an issue loading your {_configFileName}");
                DarkTideLogger.LogError("Please check your config entries for spelling and format!");
            }
        }


        #region ConfigArea

        private static ConfigEntry<bool>? _serverConfigLocked;

        /* Wolf */
        internal static ConfigEntry<int>? Weight;
        internal static ConfigEntry<int>? Minlevel;
        internal static ConfigEntry<int>? Maxlevel;
        internal static ConfigEntry<float>? LevelupChance;
        internal static ConfigEntry<float>? SpawnIntervalSec;
        internal static ConfigEntry<float>? TriggerDistance;
        internal static ConfigEntry<bool>? SetPatrolSpawnPoint;
        internal static ConfigEntry<float>? SpawnRadius;
        internal static ConfigEntry<float>? NearRadius;
        internal static ConfigEntry<float>? FarRadius;
        internal static ConfigEntry<int>? MaxNear;
        internal static ConfigEntry<int>? MaxTotal;
        internal static ConfigEntry<bool>? OnGroundOnly;

        /* Ghost */
        internal static ConfigEntry<int>? ghostWeight;
        internal static ConfigEntry<int>? ghostMinlevel;
        internal static ConfigEntry<int>? ghostMaxlevel;
        internal static ConfigEntry<float>? ghostLevelupChance;
        internal static ConfigEntry<float>? ghostSpawnIntervalSec;
        internal static ConfigEntry<float>? ghostTriggerDistance;
        internal static ConfigEntry<bool>? ghostSetPatrolSpawnPoint;
        internal static ConfigEntry<float>? ghostSpawnRadius;
        internal static ConfigEntry<float>? ghostNearRadius;
        internal static ConfigEntry<float>? ghostFarRadius;
        internal static ConfigEntry<int>? ghostMaxNear;
        internal static ConfigEntry<int>? ghostMaxTotal;
        internal static ConfigEntry<bool>? ghostOnGroundOnly;

        /* Wraith */
        internal static ConfigEntry<int>? wraithWeight;
        internal static ConfigEntry<int>? wraithMinlevel;
        internal static ConfigEntry<int>? wraithMaxlevel;
        internal static ConfigEntry<float>? wraithLevelupChance;
        internal static ConfigEntry<float>? wraithSpawnIntervalSec;
        internal static ConfigEntry<float>? wraithTriggerDistance;
        internal static ConfigEntry<bool>? wraithSetPatrolSpawnPoint;
        internal static ConfigEntry<float>? wraithSpawnRadius;
        internal static ConfigEntry<float>? wraithNearRadius;
        internal static ConfigEntry<float>? wraithFarRadius;
        internal static ConfigEntry<int>? wraithMaxNear;
        internal static ConfigEntry<int>? wraithMaxTotal;
        internal static ConfigEntry<bool>? wraithOnGroundOnly;

        /* Deathsquito */
        internal static ConfigEntry<int>? deathsquitoWeight;
        internal static ConfigEntry<int>? deathsquitoMinlevel;
        internal static ConfigEntry<int>? deathsquitoMaxlevel;
        internal static ConfigEntry<float>? deathsquitoLevelupChance;
        internal static ConfigEntry<float>? deathsquitoSpawnIntervalSec;
        internal static ConfigEntry<float>? deathsquitoTriggerDistance;
        internal static ConfigEntry<bool>? deathsquitoSetPatrolSpawnPoint;
        internal static ConfigEntry<float>? deathsquitoSpawnRadius;
        internal static ConfigEntry<float>? deathsquitoNearRadius;
        internal static ConfigEntry<float>? deathsquitoFarRadius;
        internal static ConfigEntry<int>? deathsquitoMaxNear;
        internal static ConfigEntry<int>? deathsquitoMaxTotal;

        internal static ConfigEntry<bool>? deathsquitoOnGroundOnly;

        /* TentaRoot */
        internal static ConfigEntry<int>? tentarootWeight;
        internal static ConfigEntry<int>? tentarootMinlevel;
        internal static ConfigEntry<int>? tentarootMaxlevel;
        internal static ConfigEntry<float>? tentarootLevelupChance;
        internal static ConfigEntry<float>? tentarootSpawnIntervalSec;
        internal static ConfigEntry<float>? tentarootTriggerDistance;
        internal static ConfigEntry<bool>? tentarootSetPatrolSpawnPoint;
        internal static ConfigEntry<float>? tentarootSpawnRadius;
        internal static ConfigEntry<float>? tentarootNearRadius;
        internal static ConfigEntry<float>? tentarootFarRadius;
        internal static ConfigEntry<int>? tentarootMaxNear;
        internal static ConfigEntry<int>? tentarootMaxTotal;

        internal static ConfigEntry<bool>? tentarootOnGroundOnly;

        /* Hatchling */
        internal static ConfigEntry<int>? hatchlingWeight;
        internal static ConfigEntry<int>? hatchlingMinlevel;
        internal static ConfigEntry<int>? hatchlingMaxlevel;
        internal static ConfigEntry<float>? hatchlingLevelupChance;
        internal static ConfigEntry<float>? hatchlingSpawnIntervalSec;
        internal static ConfigEntry<float>? hatchlingTriggerDistance;
        internal static ConfigEntry<bool>? hatchlingSetPatrolSpawnPoint;
        internal static ConfigEntry<float>? hatchlingSpawnRadius;
        internal static ConfigEntry<float>? hatchlingNearRadius;
        internal static ConfigEntry<float>? hatchlingFarRadius;
        internal static ConfigEntry<int>? hatchlingMaxNear;
        internal static ConfigEntry<int>? hatchlingMaxTotal;
        internal static ConfigEntry<bool>? hatchlingOnGroundOnly;


        private ConfigEntry<T> Config<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription extendedDescription =
                new(
                    description.Description +
                    (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                    description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = base.Config.Bind(group, name, value, extendedDescription);
            //var configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> Config<T>(string group, string name, T value, string description,
            bool synchronizedSetting = true)
        {
            return Config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        }

        private class ConfigurationManagerAttributes
        {
            public bool? Browsable = false;
        }


        private void InitSpawnerConfigs()
        {
            /* Wolf */
            Weight = Config("WolfSpawner", "Weight", 1, "");
            Minlevel = Config("WolfSpawner", "Min Level", 1, "Minimum level of the prefab spawned");
            Maxlevel = Config("WolfSpawner", "Max Level", 3, "Maximum possible level of the prefab spawned");
            LevelupChance = Config("WolfSpawner", "LevelUp Chance", 15f,
                "Percentage chance for the spawned prefab to level up. (Min & Max level have effect on this)");
            SpawnIntervalSec = Config("WolfSpawner", "Spawn Interval Sec", 5f, "Interval between each spawn");
            TriggerDistance = Config("WolfSpawner", "Trigger Distance", 20f, "");
            SetPatrolSpawnPoint = Config("WolfSpawner", "Patrol Spawn Point", true, "Force Server Config");
            SpawnRadius = Config("WolfSpawner", "Spawn Radius", 2.28f, "Force Server Config");
            NearRadius = Config("WolfSpawner", "Near Radius", 20f, "Force Server Config");
            FarRadius = Config("WolfSpawner", "Far Radius", 1000f, "Force Server Config");
            MaxNear = Config("WolfSpawner", "Max Near", 2, "Force Server Config");
            MaxTotal = Config("WolfSpawner", "Max Total", 100, "Force Server Config");
            OnGroundOnly = Config("WolfSpawner", "Ground Only", false, "");
            /* Ghost */
            ghostWeight = Config("GhostSpawner", "Weight", 1, "");
            ghostMinlevel = Config("GhostSpawner", "Min Level", 1, "Minimum level of the prefab spawned");
            ghostMaxlevel = Config("GhostSpawner", "Max Level", 3, "Maximum possible level of the prefab spawned");
            ghostLevelupChance = Config("GhostSpawner", "LevelUp Chance", 15f,
                "Percentage chance for the spawned prefab to level up. (Min & Max level have effect on this)");
            ghostSpawnIntervalSec = Config("GhostSpawner", "Spawn Interval Sec", 5f, "Interval between each spawn");
            ghostTriggerDistance = Config("GhostSpawner", "Trigger Distance", 20f, "");
            ghostSetPatrolSpawnPoint = Config("GhostSpawner", "Patrol Spawn Point", true, "Force Server Config");
            ghostSpawnRadius = Config("GhostSpawner", "Spawn Radius", 2.28f, "Force Server Config");
            ghostNearRadius = Config("GhostSpawner", "Near Radius", 20f, "Force Server Config");
            ghostFarRadius = Config("GhostSpawner", "Far Radius", 1000f, "Force Server Config");
            ghostMaxNear = Config("GhostSpawner", "Max Near", 2, "Force Server Config");
            ghostMaxTotal = Config("GhostSpawner", "Max Total", 100, "Force Server Config");
            ghostOnGroundOnly = Config("GhostSpawner", "Ground Only", false, "");
            /* Wraith */
            wraithWeight = Config("WraithSpawner", "Weight", 1, "");
            wraithMinlevel = Config("WraithSpawner", "Min Level", 1, "Minimum level of the prefab spawned");
            wraithMaxlevel = Config("WraithSpawner", "Max Level", 3, "Maximum possible level of the prefab spawned");
            wraithLevelupChance = Config("WraithSpawner", "LevelUp Chance", 15f,
                "Percentage chance for the spawned prefab to level up. (Min & Max level have effect on this)");
            wraithSpawnIntervalSec = Config("WraithSpawner", "Spawn Interval Sec", 5f, "Interval between each spawn");
            wraithTriggerDistance = Config("WraithSpawner", "Trigger Distance", 20f, "");
            wraithSetPatrolSpawnPoint = Config("WraithSpawner", "Patrol Spawn Point", true, "Force Server Config");
            wraithSpawnRadius = Config("WraithSpawner", "Spawn Radius", 2.28f, "Force Server Config");
            wraithNearRadius = Config("WraithSpawner", "Near Radius", 20f, "Force Server Config");
            wraithFarRadius = Config("WraithSpawner", "Far Radius", 1000f, "Force Server Config");
            wraithMaxNear = Config("WraithSpawner", "Max Near", 2, "Force Server Config");
            wraithMaxTotal = Config("WraithSpawner", "Max Total", 100, "Force Server Config");
            wraithOnGroundOnly = Config("WraithSpawner", "Ground Only", false, "");
            /* Deathsquito */
            deathsquitoWeight = Config("DeathsquitoSpawner", "Weight", 1, "");
            deathsquitoMinlevel = Config("DeathsquitoSpawner", "Min Level", 1, "Minimum level of the prefab spawned");
            deathsquitoMaxlevel = Config("DeathsquitoSpawner", "Max Level", 3,
                "Maximum possible level of the prefab spawned");
            deathsquitoLevelupChance = Config("DeathsquitoSpawner", "LevelUp Chance", 15f,
                "Percentage chance for the spawned prefab to level up. (Min & Max level have effect on this)");
            deathsquitoSpawnIntervalSec =
                Config("DeathsquitoSpawner", "Spawn Interval Sec", 5f, "Interval between each spawn");
            deathsquitoTriggerDistance = Config("DeathsquitoSpawner", "Trigger Distance", 20f, "");
            deathsquitoSetPatrolSpawnPoint =
                Config("DeathsquitoSpawner", "Patrol Spawn Point", true, "Force Server Config");
            deathsquitoSpawnRadius = Config("DeathsquitoSpawner", "Spawn Radius", 2.28f, "Force Server Config");
            deathsquitoNearRadius = Config("DeathsquitoSpawner", "Near Radius", 20f, "Force Server Config");
            deathsquitoFarRadius = Config("DeathsquitoSpawner", "Far Radius", 1000f, "Force Server Config");
            deathsquitoMaxNear = Config("DeathsquitoSpawner", "Max Near", 2, "Force Server Config");
            deathsquitoMaxTotal = Config("DeathsquitoSpawner", "Max Total", 100, "Force Server Config");
            deathsquitoOnGroundOnly = Config("DeathsquitoSpawner", "Ground Only", false, "");

            /* TentaRoot */
            tentarootWeight = Config("TentaRootSpawner", "Weight", 1, "");
            tentarootMinlevel = Config("TentaRootSpawner", "Min Level", 1, "Minimum level of the prefab spawned");
            tentarootMaxlevel = Config("TentaRootSpawner", "Max Level", 3,
                "Maximum possible level of the prefab spawned");
            tentarootLevelupChance = Config("TentaRootSpawner", "LevelUp Chance", 15f,
                "Percentage chance for the spawned prefab to level up. (Min & Max level have effect on this)");
            tentarootSpawnIntervalSec =
                Config("TentaRootSpawner", "Spawn Interval Sec", 5f, "Interval between each spawn");
            tentarootTriggerDistance = Config("TentaRootSpawner", "Trigger Distance", 20f, "");
            tentarootSetPatrolSpawnPoint =
                Config("TentaRootSpawner", "Patrol Spawn Point", true, "Force Server Config");
            tentarootSpawnRadius = Config("TentaRootSpawner", "Spawn Radius", 2.28f, "Force Server Config");
            tentarootNearRadius = Config("TentaRootSpawner", "Near Radius", 20f, "Force Server Config");
            tentarootFarRadius = Config("TentaRootSpawner", "Far Radius", 1000f, "Force Server Config");
            tentarootMaxNear = Config("TentaRootSpawner", "Max Near", 2, "Force Server Config");
            tentarootMaxTotal = Config("TentaRootSpawner", "Max Total", 100, "Force Server Config");
            tentarootOnGroundOnly = Config("TentaRootSpawner", "Ground Only", false, "");

            /* Hatchling */
            hatchlingWeight = Config("HatchlingSpawner", "Weight", 1, "");
            hatchlingMinlevel = Config("HatchlingSpawner", "Min Level", 1, "Minimum level of the prefab spawned");
            hatchlingMaxlevel = Config("HatchlingSpawner", "Max Level", 3,
                "Maximum possible level of the prefab spawned");
            hatchlingLevelupChance = Config("HatchlingSpawner", "LevelUp Chance", 15f,
                "Percentage chance for the spawned prefab to level up. (Min & Max level have effect on this)");
            hatchlingSpawnIntervalSec =
                Config("HatchlingSpawner", "Spawn Interval Sec", 5f, "Interval between each spawn");
            hatchlingTriggerDistance = Config("HatchlingSpawner", "Trigger Distance", 20f, "");
            hatchlingSetPatrolSpawnPoint =
                Config("HatchlingSpawner", "Patrol Spawn Point", true, "Force Server Config");
            hatchlingSpawnRadius = Config("HatchlingSpawner", "Spawn Radius", 2.28f, "Force Server Config");
            hatchlingNearRadius = Config("HatchlingSpawner", "Near Radius", 20f, "Force Server Config");
            hatchlingFarRadius = Config("HatchlingSpawner", "Far Radius", 1000f, "Force Server Config");
            hatchlingMaxNear = Config("HatchlingSpawner", "Max Near", 2, "Force Server Config");
            hatchlingMaxTotal = Config("HatchlingSpawner", "Max Total", 100, "Force Server Config");
            hatchlingOnGroundOnly = Config("HatchlingSpawner", "Ground Only", false, "");
        }

        #endregion
    }
}