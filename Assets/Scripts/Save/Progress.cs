using UnityEngine;
public static class Progress
{
    public static void SaveProgress(string savefile, int progress)
    {
        SaveSystem.Save(savefile, JsonUtility.ToJson(progress));
    }

    public static int LoadProgress(string savefile)
    {
        if(!SaveSystem.Exists(savefile))
        {
            SaveProgress(savefile, 0);
            Debug.Log("No savefile created. Creating one with score 0");
            return 0;
        }
        else return JsonUtility.FromJson<int>(SaveSystem.Load(savefile));
    }

    public static bool Exists(string savefile)
    {
        return SaveSystem.Exists(savefile);
    }
}