using System.Runtime.InteropServices;
using UnityEngine;
public class Progress
{
    public static void SaveProgress(string savefile, int progress)
    {
        SaveSystem.Save(savefile, JsonUtility.ToJson(new ProgressInt(progress)));
    }

    public static int LoadProgress(string savefile)
    {
        if(!SaveSystem.Exists(savefile))
        {
            SaveProgress(savefile, 0);
            Debug.Log("No savefile created. Creating one with score 0");
            return 0;
        }
        else return JsonUtility.FromJson<ProgressInt>(SaveSystem.Load(savefile)).integer;
    }

    public static bool Exists(string savefile)
    {
        return SaveSystem.Exists(savefile);
    }

    [System.Serializable]
    class ProgressInt
    {
        public int integer;
        public ProgressInt(int i)
        {
            integer = i;
        }
    }
}