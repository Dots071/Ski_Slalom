using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private const string fileName = "Persistant.data";
    public static void SavePersistantData(GameManger gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, fileName);
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gameManager);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadPersistantData()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();
            return data;

        } else
        {
            Debug.Log("[SaveManager] Save file does not exist! " + path);
            return null;
        }
    }
}