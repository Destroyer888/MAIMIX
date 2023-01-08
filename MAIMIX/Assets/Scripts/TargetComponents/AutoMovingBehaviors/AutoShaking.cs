using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShaking : AutoMoving
{
    [SerializeField] private float min_shake_distance = 1f, max_shake_distance = 2f, min_shake_period = 1f, max_shake_period = 2f;
    [SerializeField]private float shake_distance, shake_period;
    private Vector2 current_direction, init_position;
    private float current_timer = 0;


    void Start()
    {
        shake_distance = min_shake_distance + Random.Range(0, (max_shake_distance-min_shake_distance) * 10 + 1) / 10;
        shake_period = min_shake_period + Random.Range(0, (max_shake_period - min_shake_period)* 10 + 1) / 10;
        init_position = transform.position;
        current_direction = Random.insideUnitCircle;
        current_direction = new Vector3(current_direction.x/Mathf.Sqrt(current_direction.x*current_direction.x + current_direction.y*current_direction.y) , 
                                        current_direction.y / Mathf.Sqrt(current_direction.x * current_direction.x + current_direction.y * current_direction.y), 
                                        0);
        current_direction *= shake_distance;
        
    }
    private void OnEnable()
    {
        init_position = transform.position;
    }
    void Update()
    {
        if (current_timer <= shake_period/2)
        {
            transform.position = Vector2.Lerp(init_position, init_position + new Vector2(current_direction.x, current_direction.y), current_timer/shake_period/2);
            current_timer += Time.deltaTime;
        }
        else if(current_timer < shake_period)
        {
            transform.position = Vector2.Lerp(init_position, init_position + new Vector2(current_direction.x, current_direction.y), (shake_period - current_timer)/shake_period/2);
            current_timer += Time.deltaTime;
        }
        else
        {
            current_direction = Random.insideUnitCircle;
            current_direction = new Vector3(current_direction.x / Mathf.Sqrt(current_direction.x * current_direction.x + current_direction.y * current_direction.y),
                                            current_direction.y / Mathf.Sqrt(current_direction.x * current_direction.x + current_direction.y * current_direction.y),
                                            0);
            current_direction *= shake_distance;
            current_timer = 0;
        }
    }
}
