using System;
using System.IO;
using UnityEngine;

namespace Core.UniversalStorage
{
    public static class Storage
    {
        private const string FileExtension = ".dat";

        public static void SavePrefs<T>(T data, string key = null, int? liveMinutes = null) =>
            Save(data, StorageTarget.PlayerPrefs, key, liveMinutes);
        
        public static void SavePersistent<T>(T data, string key = null, int? liveMinutes = null) =>
            Save(data, StorageTarget.PersistentData, key, liveMinutes);
        
        public static void SaveTemporary<T>(T data, string key = null, int? liveMinutes = null) =>
            Save(data, StorageTarget.TemporaryCache, key, liveMinutes);
        
        public static void Save<T>(T data, StorageTarget target, string key = null, int? liveMinutes = null)
        {
            string actualKey = GetKey<T>(key);
            var wrapper = new StoredWrapper<T>
            {
                Data = data,
                Timestamp = DateTime.UtcNow.Ticks,
                LiveMinutes = liveMinutes
            };

            string json = JsonUtility.ToJson(wrapper);

            switch (target)
            {
                case StorageTarget.PlayerPrefs:
                    PlayerPrefs.SetString(actualKey, json);
                    PlayerPrefs.Save();
                    break;
                case StorageTarget.PersistentData:
                case StorageTarget.TemporaryCache:
                    File.WriteAllText(GetFilePath(actualKey, target), json);
                    break;
            }
        }

        public static T LoadPrefs<T>(string key = null) =>
            Load<T>(StorageTarget.PlayerPrefs, key);
        public static T LoadPersistent<T>(string key = null) =>
            Load<T>(StorageTarget.PersistentData, key);
        public static T LoadTemporary<T>(string key = null) =>
            Load<T>(StorageTarget.TemporaryCache, key);
        
        public static T Load<T>(StorageTarget target, string key = null)
        {
            string actualKey = GetKey<T>(key);
            string json = null;

            switch (target)
            {
                case StorageTarget.PlayerPrefs:
                    if (!PlayerPrefs.HasKey(actualKey))
                        return default;
                    json = PlayerPrefs.GetString(actualKey);
                    break;
                case StorageTarget.PersistentData:
                case StorageTarget.TemporaryCache:
                    string path = GetFilePath(actualKey, target);
                    if (!File.Exists(path))
                        return default;
                    json = File.ReadAllText(path);
                    break;
            }

            if (string.IsNullOrEmpty(json))
                return default;

            try
            {
                var wrapper = JsonUtility.FromJson<StoredWrapper<T>>(json);

                if (wrapper.LiveMinutes.HasValue)
                {
                    var savedTime = new DateTime(wrapper.Timestamp, DateTimeKind.Utc);
                    if (DateTime.UtcNow > savedTime.AddMinutes(wrapper.LiveMinutes.Value))
                    {
                        Delete<T>(target, key);
                        return default;
                    }
                }

                return wrapper.Data;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[UniversalStorage] Load failed: {ex.Message}");
                return default;
            }
        }

        public static void Delete<T>(StorageTarget target, string key = null)
        {
            string actualKey = GetKey<T>(key);
            switch (target)
            {
                case StorageTarget.PlayerPrefs:
                    PlayerPrefs.DeleteKey(actualKey);
                    break;
                case StorageTarget.PersistentData:
                case StorageTarget.TemporaryCache:
                    string path = GetFilePath(actualKey, target);
                    if (File.Exists(path))
                        File.Delete(path);
                    break;
            }
        }

        public static void ClearPrefs() =>
            Clear(StorageTarget.PlayerPrefs);

        public static void ClearPersistent() =>
            Clear(StorageTarget.PersistentData);

        public static void ClearTemporary() =>
            Clear(StorageTarget.TemporaryCache);

        public static void ClearAll()
        {
            ClearPrefs();
            ClearPersistent();
            ClearTemporary();
        }

        public static void Clear(StorageTarget target)
        {
            switch (target)
            {
                case StorageTarget.PlayerPrefs:
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.Save();
                    break;
                case StorageTarget.PersistentData:
                case StorageTarget.TemporaryCache:

                    string basePath = target switch
                    {
                        StorageTarget.PersistentData => Application.persistentDataPath,
                        StorageTarget.TemporaryCache => Application.temporaryCachePath,
                        _ => throw new InvalidOperationException("PlayerPrefs не поддерживает папку Data")
                    };

                    string dataFolder = Path.Combine(basePath, "Data");

                    if (Directory.Exists(dataFolder))
                    {
                        try
                        {
                            Directory.Delete(dataFolder, true);
                            Debug.Log($"[UniversalStorage] Cleared {target} Data folder.");
                        }
                        catch (Exception ex)
                        {
                            Debug.LogWarning($"[UniversalStorage] Failed to clear {target} Data: {ex.Message}");
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
        }


        private static string GetKey<T>(string key) =>
            string.IsNullOrEmpty(key) ? typeof(T).FullName : key;

        private static string GetFilePath(string key, StorageTarget target)
        {
            string basePath = target switch
            {
                StorageTarget.PersistentData => Application.persistentDataPath,
                StorageTarget.TemporaryCache => Application.temporaryCachePath,
                _ => throw new InvalidOperationException("Invalid file storage target")
            };

            string dataFolder = Path.Combine(basePath, "Data");

            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            return Path.Combine(dataFolder, key + FileExtension);
        }
    }
}