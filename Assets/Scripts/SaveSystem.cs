using System.IO;
using UnityEngine;

public class SaveSystem
{
    public static void Save(string filename, string content)
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            PlayerPrefs.SetString(filename, content);
            PlayerPrefs.Save();
        }
        else
        {
            File.WriteAllText(Application.persistentDataPath + "/" + filename, content);
        }
    }

    public static string Load(string filename)
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return PlayerPrefs.GetString(filename);
        }
        else
        {
            return File.ReadAllText(Application.persistentDataPath + "/" + filename);
        }
    }

    public static bool Exists(string filename)
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return PlayerPrefs.HasKey(filename);
        }
        else
        {
            return File.Exists(Application.persistentDataPath + "/" + filename);
        }
    }
}
