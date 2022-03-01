using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace DarkTideSpawners
{
    public static class Assets
    {
        private static GameObject _wolfSpawner = null!;
        private static GameObject _ghostSpawner = null!;
        private static GameObject _wraithSpawner = null!;
        private static GameObject _deathsquitoSpawner = null!;
        private static GameObject _tentarootSpawner = null!;
        private static GameObject _hatchlingSpawner = null!;

        private static bool _assetsLoaded;

        private static List<GameObject> _spawnerList = new();

        private static AssetBundle GetAssetBundleFromResources(string filename)
        {
            Assembly execAssembly = Assembly.GetExecutingAssembly();
            string resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));

            using Stream? stream = execAssembly.GetManifestResourceStream(resourceName);
            return AssetBundle.LoadFromStream(stream);
        }

        private static void InitAssets(GameObject spawnerGo, string containString)
        {
            if (!spawnerGo.gameObject.name.ToLower().Contains(containString.ToLower())) return;
            if (!spawnerGo.GetComponent<SpawnArea>()) return;
            SpawnArea? spawerSpawnArea = spawnerGo.GetComponent<SpawnArea>();
            switch (containString)
            {
                case "Wolf":
                    spawerSpawnArea.m_levelupChance = DTSpawners.LevelupChance.Value;
                    spawerSpawnArea.m_spawnIntervalSec = DTSpawners.SpawnIntervalSec.Value;
                    spawerSpawnArea.m_triggerDistance = DTSpawners.TriggerDistance.Value;
                    spawerSpawnArea.m_setPatrolSpawnPoint = DTSpawners.SetPatrolSpawnPoint.Value;
                    spawerSpawnArea.m_spawnRadius = DTSpawners.SpawnRadius.Value;
                    spawerSpawnArea.m_nearRadius = DTSpawners.NearRadius.Value;
                    spawerSpawnArea.m_farRadius = DTSpawners.FarRadius.Value;
                    spawerSpawnArea.m_maxNear = DTSpawners.MaxNear.Value;
                    spawerSpawnArea.m_maxTotal = DTSpawners.MaxTotal.Value;
                    spawerSpawnArea.m_onGroundOnly = DTSpawners.OnGroundOnly.Value;
                    PrefabLoad(spawerSpawnArea, containString);

                    break;
                case "Ghost":
                    spawerSpawnArea.m_levelupChance = DTSpawners.ghostLevelupChance.Value;
                    spawerSpawnArea.m_spawnIntervalSec = DTSpawners.ghostSpawnIntervalSec.Value;
                    spawerSpawnArea.m_triggerDistance = DTSpawners.ghostTriggerDistance.Value;
                    spawerSpawnArea.m_setPatrolSpawnPoint = DTSpawners.ghostSetPatrolSpawnPoint.Value;
                    spawerSpawnArea.m_spawnRadius = DTSpawners.ghostSpawnRadius.Value;
                    spawerSpawnArea.m_nearRadius = DTSpawners.ghostNearRadius.Value;
                    spawerSpawnArea.m_farRadius = DTSpawners.ghostFarRadius.Value;
                    spawerSpawnArea.m_maxNear = DTSpawners.ghostMaxNear.Value;
                    spawerSpawnArea.m_maxTotal = DTSpawners.ghostMaxTotal.Value;
                    spawerSpawnArea.m_onGroundOnly = DTSpawners.ghostOnGroundOnly.Value;
                    PrefabLoad(spawerSpawnArea, containString);
                    break;
                case "Wraith":

                    spawerSpawnArea.m_levelupChance = DTSpawners.wraithLevelupChance.Value;
                    spawerSpawnArea.m_spawnIntervalSec = DTSpawners.wraithSpawnIntervalSec.Value;
                    spawerSpawnArea.m_triggerDistance = DTSpawners.wraithTriggerDistance.Value;
                    spawerSpawnArea.m_setPatrolSpawnPoint = DTSpawners.wraithSetPatrolSpawnPoint.Value;
                    spawerSpawnArea.m_spawnRadius = DTSpawners.wraithSpawnRadius.Value;
                    spawerSpawnArea.m_nearRadius = DTSpawners.wraithNearRadius.Value;
                    spawerSpawnArea.m_farRadius = DTSpawners.wraithFarRadius.Value;
                    spawerSpawnArea.m_maxNear = DTSpawners.wraithMaxNear.Value;
                    spawerSpawnArea.m_maxTotal = DTSpawners.wraithMaxTotal.Value;
                    spawerSpawnArea.m_onGroundOnly = DTSpawners.wraithOnGroundOnly.Value;
                    PrefabLoad(spawerSpawnArea, containString);
                    break;
                case "Deathsquito":

                    spawerSpawnArea.m_levelupChance = DTSpawners.deathsquitoLevelupChance.Value;
                    spawerSpawnArea.m_spawnIntervalSec = DTSpawners.deathsquitoSpawnIntervalSec.Value;
                    spawerSpawnArea.m_triggerDistance = DTSpawners.deathsquitoTriggerDistance.Value;
                    spawerSpawnArea.m_setPatrolSpawnPoint = DTSpawners.deathsquitoSetPatrolSpawnPoint.Value;
                    spawerSpawnArea.m_spawnRadius = DTSpawners.deathsquitoSpawnRadius.Value;
                    spawerSpawnArea.m_nearRadius = DTSpawners.deathsquitoNearRadius.Value;
                    spawerSpawnArea.m_farRadius = DTSpawners.deathsquitoFarRadius.Value;
                    spawerSpawnArea.m_maxNear = DTSpawners.deathsquitoMaxNear.Value;
                    spawerSpawnArea.m_maxTotal = DTSpawners.deathsquitoMaxTotal.Value;
                    spawerSpawnArea.m_onGroundOnly = DTSpawners.deathsquitoOnGroundOnly.Value;
                    PrefabLoad(spawerSpawnArea, containString);
                    break;
                case "TentaRoot":

                    spawerSpawnArea.m_levelupChance = DTSpawners.tentarootLevelupChance.Value;
                    spawerSpawnArea.m_spawnIntervalSec = DTSpawners.tentarootSpawnIntervalSec.Value;
                    spawerSpawnArea.m_triggerDistance = DTSpawners.tentarootTriggerDistance.Value;
                    spawerSpawnArea.m_setPatrolSpawnPoint = DTSpawners.tentarootSetPatrolSpawnPoint.Value;
                    spawerSpawnArea.m_spawnRadius = DTSpawners.tentarootSpawnRadius.Value;
                    spawerSpawnArea.m_nearRadius = DTSpawners.tentarootNearRadius.Value;
                    spawerSpawnArea.m_farRadius = DTSpawners.tentarootFarRadius.Value;
                    spawerSpawnArea.m_maxNear = DTSpawners.tentarootMaxNear.Value;
                    spawerSpawnArea.m_maxTotal = DTSpawners.tentarootMaxTotal.Value;
                    spawerSpawnArea.m_onGroundOnly = DTSpawners.tentarootOnGroundOnly.Value;
                    PrefabLoad(spawerSpawnArea, containString);
                    break;
                case "Hatchling":

                    spawerSpawnArea.m_levelupChance = DTSpawners.hatchlingLevelupChance.Value;
                    spawerSpawnArea.m_spawnIntervalSec = DTSpawners.hatchlingSpawnIntervalSec.Value;
                    spawerSpawnArea.m_triggerDistance = DTSpawners.hatchlingTriggerDistance.Value;
                    spawerSpawnArea.m_setPatrolSpawnPoint = DTSpawners.hatchlingSetPatrolSpawnPoint.Value;
                    spawerSpawnArea.m_spawnRadius = DTSpawners.hatchlingSpawnRadius.Value;
                    spawerSpawnArea.m_nearRadius = DTSpawners.hatchlingNearRadius.Value;
                    spawerSpawnArea.m_farRadius = DTSpawners.hatchlingFarRadius.Value;
                    spawerSpawnArea.m_maxNear = DTSpawners.hatchlingMaxNear.Value;
                    spawerSpawnArea.m_maxTotal = DTSpawners.hatchlingMaxTotal.Value;
                    spawerSpawnArea.m_onGroundOnly = DTSpawners.hatchlingOnGroundOnly.Value;
                    PrefabLoad(spawerSpawnArea, containString);
                    break;
            }
        }

        private static void PrefabLoad(SpawnArea spawerSpawnArea, string znetPrefab)
        {
            try
            {
                foreach (SpawnArea.SpawnData prefab in spawerSpawnArea.m_prefabs)
                {
                    prefab.m_prefab = ZNetScene.instance.GetPrefab(znetPrefab);
                    switch (znetPrefab)
                    {
                        case "Wolf":
                            prefab.m_weight = DTSpawners.Weight.Value;
                            prefab.m_minLevel = DTSpawners.Minlevel.Value;
                            prefab.m_maxLevel = DTSpawners.Maxlevel.Value;
                            break;
                        case "Ghost":
                            prefab.m_weight = DTSpawners.ghostWeight.Value;
                            prefab.m_minLevel = DTSpawners.ghostMinlevel.Value;
                            prefab.m_maxLevel = DTSpawners.ghostMaxlevel.Value;
                            break;
                        case "Wraith":
                            prefab.m_weight = DTSpawners.wraithWeight.Value;
                            prefab.m_minLevel = DTSpawners.wraithMinlevel.Value;
                            prefab.m_maxLevel = DTSpawners.wraithMaxlevel.Value;
                            break;
                        case "Deathsquito":
                            prefab.m_weight = DTSpawners.deathsquitoWeight.Value;
                            prefab.m_minLevel = DTSpawners.deathsquitoMinlevel.Value;
                            prefab.m_maxLevel = DTSpawners.deathsquitoMaxlevel.Value;
                            break;
                        case "TentaRoot":
                            prefab.m_weight = DTSpawners.tentarootWeight.Value;
                            prefab.m_minLevel = DTSpawners.tentarootMinlevel.Value;
                            prefab.m_maxLevel = DTSpawners.tentarootMaxlevel.Value;
                            break;
                        case "Hatchling":
                            prefab.m_weight = DTSpawners.hatchlingWeight.Value;
                            prefab.m_minLevel = DTSpawners.hatchlingMinlevel.Value;
                            prefab.m_maxLevel = DTSpawners.hatchlingMaxlevel.Value;
                            break;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void LoadAssets()
        {
            AssetBundle assetBundle = GetAssetBundleFromResources("darktidespawners");
            _wolfSpawner = assetBundle.LoadAsset<GameObject>("WolfSpawner");
            _ghostSpawner = assetBundle.LoadAsset<GameObject>("GhostSpawner");
            _wraithSpawner = assetBundle.LoadAsset<GameObject>("WraithSpawner");
            _deathsquitoSpawner = assetBundle.LoadAsset<GameObject>("DeathsquitoSpawner");
            _deathsquitoSpawner = assetBundle.LoadAsset<GameObject>("TentaRootSpawner");
            _deathsquitoSpawner = assetBundle.LoadAsset<GameObject>("HatchlingSpawner");

            InitAll();


            assetBundle.Unload(false);
            _assetsLoaded = true;
        }

        private static void InitAll()
        {
            InitAssets(_wolfSpawner, "Wolf");
            InitAssets(_ghostSpawner, "Ghost");
            InitAssets(_wraithSpawner, "Wraith");
            InitAssets(_deathsquitoSpawner, "Deathsquito");
            InitAssets(_tentarootSpawner, "TentaRoot");
            InitAssets(_hatchlingSpawner, "Hatchling");
        }

        [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
        public static class DTSpawnersZNetSceneAwakePatch
        {
            public static bool Prefix(ZNetScene __instance)
            {
                if (!_assetsLoaded) LoadAssets();


                __instance.m_prefabs.Add(_wolfSpawner);
                __instance.m_prefabs.Add(_ghostSpawner);
                __instance.m_prefabs.Add(_wraithSpawner);
                __instance.m_prefabs.Add(_deathsquitoSpawner);
                __instance.m_prefabs.Add(_tentarootSpawner);
                __instance.m_prefabs.Add(_hatchlingSpawner);

                DTSpawners.DarkTideLogger.LogDebug("Loading the prefabs to ZNetScene");
                return true;
            }
        }

        [HarmonyPatch(typeof(SpawnArea), nameof(SpawnArea.UpdateSpawn))]
        public static class DTSpawnersSpawnAreaUpdateSpawnPatch
        {
            public static bool Prefix(SpawnArea __instance)
            {
                if (!_assetsLoaded) LoadAssets();

                try
                {
                    InitAll();
                }
                catch
                {
                    DTSpawners.DarkTideLogger.LogDebug("Attempt to UpdateSpawners for DTSpawners failed");
                }

                DTSpawners.DarkTideLogger.LogDebug("Attempt to UpdateSpawners for DTSpawners success!");
                return true;
            }
        }

        [HarmonyPatch(typeof(SpawnArea), nameof(SpawnArea.Awake))]
        public static class DTSpawnersSpawnAreaAwakePatch
        {
            public static bool Prefix(SpawnArea __instance)
            {
                if (!_assetsLoaded) LoadAssets();

                try
                {
                    InitAll();
                }
                catch
                {
                    DTSpawners.DarkTideLogger.LogDebug("Attempt to UpdateSpawners for DTSpawners on awake failed");
                }

                DTSpawners.DarkTideLogger.LogDebug("Attempt to UpdateSpawners for DTSpawners on awake success!");
                return true;
            }
        }
    }
}