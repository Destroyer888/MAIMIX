using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRulesManager : CustomSingleton<GameRulesManager>
{
    private UIManager UI_manager;
    public int game_points = 0;
    public delegate void GRDelegate(int i);
    public event GRDelegate OnGamePointsUpdated;
    private void Start()
    {

        UI_manager = UIManager.instance;
        UI_manager.OnGameRestarted += RestartGame;
    }
    private void OnEnable()
    {
        if (UI_manager == null) return;
        UI_manager.OnGameRestarted += RestartGame;
    }
    private void OnDisable()
    {
        UI_manager.OnGameRestarted -= RestartGame;
    }
    public void UpdateGamePoints(int count)
    {
        game_points += count;
        OnGamePointsUpdated?.Invoke(game_points);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
