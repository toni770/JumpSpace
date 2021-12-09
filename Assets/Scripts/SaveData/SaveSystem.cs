using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveData(DataManager data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = DataPath();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData(data);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadData()
    {
        string path = DataPath();
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = (GameData)formatter.Deserialize(stream);

            stream.Close();
            return gameData;
        }
        else
        {
           // Debug.LogError("Save File Not found in " + path);
            return null;
        }
    }

    private static string DataPath()
    {
        return Application.persistentDataPath + "/SpaceDump.save";
    }
}
