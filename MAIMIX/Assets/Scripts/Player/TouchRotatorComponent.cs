using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotatorComponent : MonoBehaviour
{
    private Transform current_transform;
    private TouchController current_touch_controller;
    private Vector2 current_direction;
    void Start()
    {
        current_touch_controller = TouchController.instance;
        current_transform = transform;
        current_touch_controller.OnTouchPositionChanged += UpdateDirection;
    }
    private void OnEnable()
    {
        if (current_touch_controller == null)
            return;
        current_touch_controller.OnTouchPositionChanged += UpdateDirection;
    }
    private void OnDisable()
    {
        current_touch_controller.OnTouchPositionChanged -= UpdateDirection;
    }

    public void UpdateDirection(Vector2 dir)
    {
        current_direction = dir;
    }
  
    void Update()
    {
        float current_angle = MathHelper.CalculateAngleFromVector(current_direction);
        current_transform.rotation = Quaternion.Euler(0, 0, current_angle);
    }
}
