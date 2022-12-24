using UnityEngine;
using System.IO;

public class Main : MonoBehaviour
{
    private MainConfig _mainConfig;
    private GameState _gameState;

    private const string SaveFileName = "TestClickerSaveData.dat";

    private void Start()
    {
        _mainConfig = transform.GetComponent<MainConfig>();

        _gameState = Load();
        if (_gameState == null)
            _gameState = new GameState(_mainConfig);
        else
            _gameState.Init(_mainConfig);

        EventsHolder.EventGameStarted(_gameState);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }
 
    private void Save()
    {
        var path = Path.Combine(Application.persistentDataPath, SaveFileName);
        var json = JsonUtility.ToJson(_gameState, true);
        File.WriteAllText(path, json);
    }

    private GameState Load()
    {
        var path = Path.Combine(Application.persistentDataPath, SaveFileName);
        if (!File.Exists(path))
            return null;
        var json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameState>(json);
    }
}
