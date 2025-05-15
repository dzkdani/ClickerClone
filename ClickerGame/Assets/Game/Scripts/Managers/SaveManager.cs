using UnityEngine;

public static class SaveManager
{
    public static void LocalSaveInt(string key, int value) => PlayerPrefs.SetInt(key, value);
    public static void LocalSaveFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);

    public static int LocalLoadInt(string key, int defaultVal = 0) => PlayerPrefs.GetInt(key, defaultVal);
    public static float LocalLoadFloat(string key, float defaultVal = 0f) => PlayerPrefs.GetFloat(key, defaultVal);

    public static void localSave() => PlayerPrefs.Save();
}
