using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : CustomSingleton<DataManager>
{
    public static GameData current_game_data;
    public delegate void GDDelegate(ref GameData data);
    public event GDDelegate OnGameSaved, OnGameLoaded;
    private static bool isSaved = false;
    private void Start()
    {
        StartCoroutine(LoadCoroutine());
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            StartCoroutine(SaveCoroutine());
    }
    private void OnApplicationQuit()
    {
        StartCoroutine(SaveCoroutine());
    }
    private IEnumerator LoadCoroutine()
    {
        current_game_data = LoadSerialisedData();
        yield return new WaitForSeconds(0.5f);
        OnGameLoaded?.Invoke(ref current_game_data);
        Debug.Log(current_game_data.points);
        yield break;
    }
    private IEnumerator SaveCoroutine() 
    {
        OnGameSaved?.Invoke(ref current_game_data);
        SerialiseData(current_game_data);
        while (!isSaved)
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        Debug.Log(current_game_data.points);
        yield break;
    }
    public static void SerialiseData(GameData data)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_data");
        }
        string file_path = Application.persistentDataPath + "/game_data/data.txt";
        BinaryFormatter binary_formatter = new BinaryFormatter();
        if(File.Exists(file_path))
            File.Delete(file_path);
        FileStream file_stream = File.Create(file_path);
        binary_formatter.Serialize(file_stream, data);
        file_stream.Close();
        isSaved = true;
    }
    public static GameData LoadSerialisedData()
    {
        string file_path = Application.persistentDataPath + "/game_data/data.txt";
        if (!File.Exists(file_path)) 
            return new GameData();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(file_path, FileMode.Open);
        GameData data = bf.Deserialize(fs) as GameData;
        fs.Close();
        return data;
    }
   
}
[System.Serializable]
public class GameData
{
    public int points, bullets_count;
    public int[] available_skins;
    public int[] available_themes;
    public int current_target_class;
    public GameData() {}
    public GameData(int pts, int bullets, int target_class, int[] skins, int[] themes)
    {
        points = pts;
        bullets_count = bullets;
        current_target_class = target_class;
        available_skins = skins;
        available_themes = themes;
    }
}
