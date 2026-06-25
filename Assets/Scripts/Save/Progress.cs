using UnityEngine;
public class Progress
{
    public const string savefile = "progress";
    public void SaveProgress(int progress)
    {
        SaveSystem.Save(savefile, JsonUtility.ToJson(progress));
    }

    public int LoadProgress()
    {
        if(SaveSystem.Exists(savefile))
        {
            SaveProgress(0);
            Debug.Log("No savefile created. Creating one with progress 1");
            return 0;
        }
        return JsonUtility.FromJson<int>(SaveSystem.Load(savefile));
    }

    public bool Exists()
    {
        return SaveSystem.Exists(savefile);
    }
}