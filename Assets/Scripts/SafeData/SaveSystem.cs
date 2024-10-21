using Points;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
{
    public static void SavePoints(ShopPoint shop)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData.multy";
        FileStream stream = new FileStream(path, FileMode.Create);

		SavePoints data = new SavePoints(shop);

		formatter.Serialize(stream, data);
        stream.Close();
	}

    public static SavePoints LoadPoints()
    {
		string path = Application.persistentDataPath + "/saveData.multy";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePoints data = binaryFormatter.Deserialize(stream) as SavePoints;
            stream .Close();

            return data;
		}
        Debug.LogError($"Not find {path}");
        return null;
	}
}
