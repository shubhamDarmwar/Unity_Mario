using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	private static string path = Application.persistentDataPath + "/Player.fun";
	private static int unlockedLevel = 1;
	private static int oldScore = 0;

	public static void savePlayer(int level, int score) {
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(path, FileMode.Create);
		int newUnlockedLevel = unlockedLevel;
		if(level > unlockedLevel) {
			newUnlockedLevel = level;
			unlockedLevel = level;
		}

		int newScore = oldScore;
		if(score > oldScore) {
			newScore = score;
		}

		PlayerProgress data = new PlayerProgress(newUnlockedLevel, newScore);
		formatter.Serialize(stream, data);
		stream.Close();
		// Debug.Log("Saved player");
	}

	public static PlayerProgress loadPlayer() {
		// string path = Application.persistentDataPath + "/Player.fun";
		if(File.Exists(path)) {
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerProgress data = formatter.Deserialize(stream) as PlayerProgress;
			stream.Close();
			// Debug.Log("load player");

			unlockedLevel = data.level;
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
