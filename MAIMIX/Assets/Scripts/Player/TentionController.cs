using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentionController : MonoBehaviour
{
    private Transform graphics_for_tention; 
    private TouchController current_touch_controller;
    private Vector2 current_tention_dir;
    private float min_tention = 1, max_tention = 2;
    public delegate void OnTentionReleased(Vector2 dir);
    public event OnTentionReleased OnReleased;
    void Start()
    {
        graphics_for_tention = GetComponentInChildren<SpriteRenderer>().transform;
        current_touch_controller = TouchController.instance;
        current_touch_controller.OnTouchPositionChanged += UpdateTention;
        current_touch_controller.OnTouchEnded += ResetTention;
    }
    private void OnEnable()
    {
        if (current_touch_controller == null)
            return;
        current_touch_controller.OnTouchPositionChanged += UpdateTention;
        current_touch_controller.OnTouchEnded += ResetTention;
    }
    private void OnDisable()
    {
        current_touch_controller.OnTouchPositionChanged -= UpdateTention;
        current_touch_controller.OnTouchEnded -= ResetTention;
    }

    public void UpdateTention(Vector2 dir)
    {
        current_tention_dir = dir;
    }
    public void ResetTention(Vector2 pos)
    {
        OnReleased?.Invoke(Vector2.ClampMagnitude(current_tention_dir, max_tention));
        current_tention_dir = Vector2.zero;
    }
    void Update()
    {
        float deformed_length = Mathf.Clamp(current_tention_dir.magnitude + min_tention, min_tention, max_tention);
        graphics_for_tention.localScale = new Vector3(0.5f, deformed_length, 1);
        graphics_for_tention.localPosition = new Vector3(-1 - deformed_length/2 + min_tention, 0, 0);
    }
}
