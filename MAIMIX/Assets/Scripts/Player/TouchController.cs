using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TouchController : MonoBehaviour
{

    [SerializeField] private Transform touch_init_position_gfx;
    private LineRenderer touch_direction_gfx;
    public static TouchController instance;

    private Touch current_touch;
    public delegate void TouchDelegate(Vector2 pos);
    public event TouchDelegate OnTouchStarted, OnTouchEnded, OnTouchPositionChanged;

    private Vector2 touch_direction, touch_init_position;
    private Camera current_camera;

    private void OnDisable()
    {
        touch_init_position_gfx.gameObject.SetActive(false);
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    private void Start()
    {
        current_camera = GetComponent<Camera>();
        touch_direction_gfx = touch_init_position_gfx.gameObject.GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        current_touch = Input.GetTouch(0);
        
        if (current_touch.phase == TouchPhase.Began)
        {
            touch_direction = Vector2.zero;
            touch_init_position = current_camera.ScreenToWorldPoint(current_touch.position);
            OnTouchStarted?.Invoke(touch_init_position);
            touch_init_position_gfx.position = touch_init_position;
            touch_direction_gfx.SetPosition(0, new Vector3(touch_init_position_gfx.position.x, touch_init_position_gfx.position.y, 0));
            touch_direction_gfx.SetPosition(1, new Vector3(touch_init_position_gfx.position.x, touch_init_position_gfx.position.y, 0));
            touch_init_position_gfx.gameObject.SetActive(true);
        }     
        if (current_touch.phase == TouchPhase.Moved)
        {
            Vector3 current_touch_pos = current_camera.ScreenToWorldPoint(current_touch.position);
            touch_direction = new Vector3(touch_init_position.x, touch_init_position.y, 0) - current_touch_pos;
            OnTouchPositionChanged?.Invoke(touch_direction);
            touch_direction_gfx.SetPosition(1, new Vector3(current_touch_pos.x, current_touch_pos.y, 0));
        }
        if (current_touch.phase == TouchPhase.Ended)
        {
            OnTouchEnded?.Invoke(current_camera.ScreenToWorldPoint(current_touch.position));
            touch_init_position_gfx.gameObject.SetActive(false);
        }
            
    }
}
