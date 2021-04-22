using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	private static string path = Application.persistentDataPath + "/Player.fun";

	public static void savePlayer( int level) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerProgress data = new PlayerProgress(level);
		formatter.Serialize(stream, data);
		stream.Close();
		Debug.Log("Saved player");
	}

	public static PlayerProgress loadPlayer() {
		// string path = Application.persistentDataPath + "/Player.fun";
		if(File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerProgress data = formatter.Deserialize(stream) as PlayerProgress;
			stream.Close();
			Debug.Log("load player");
			return data; 
			} else {
				
				return new PlayerProgress();
			}
		// return null;
	}
    
    public static void resetData() {

		if(File.Exists(path)) {
			File.Delete(path);
		}
		// AssetDatabase.Refresh();
    }
}
