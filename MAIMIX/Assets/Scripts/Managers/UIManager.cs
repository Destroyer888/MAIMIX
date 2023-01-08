using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomSingleton<UIManager>
{
    public delegate void UIDelegate();
    public delegate void UIDelegateToggle(bool i);
    public event UIDelegate OnGameRestarted, OnGoingToMenu;
    public event UIDelegateToggle OnSoundsToggled, OnMusicToggled;

    
    public void RestartGame()
    {
        OnGameRestarted?.Invoke();
    }
    public void ToggleSounds(bool i)
    {
        OnSoundsToggled?.Invoke(i);
    }
    public void ToggleMusic(bool i)
    {
        OnMusicToggled?.Invoke(i);
    }
}
