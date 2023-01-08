using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(TextMeshProUGUI))]
public class PointsCounter : MonoBehaviour
{
    private TextMeshProUGUI TMP_UGUI;
    private GameRulesManager game_rules_manager;
    private void Start()
    {
        TMP_UGUI = GetComponent<TextMeshProUGUI>();
        game_rules_manager = GameRulesManager.instance;
        game_rules_manager.OnGamePointsUpdated += UpdatePoints;
    }
    private void OnEnable()
    {
        if (game_rules_manager == null) return;
        game_rules_manager.OnGamePointsUpdated += UpdatePoints;
    }
    private void OnDisable()
    {
        game_rules_manager.OnGamePointsUpdated -= UpdatePoints;
    }
    public void UpdatePoints(int points)
    {
        TMP_UGUI.text = "POINTS:" + points.ToString();
    }
}
