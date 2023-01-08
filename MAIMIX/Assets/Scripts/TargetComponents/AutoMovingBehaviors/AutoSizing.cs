using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AutoSizing : AutoMoving
{
    [SerializeField] private float min_size = 1f, max_size = 2f, min_period = 1f, max_period = 2f;
    [SerializeField] private float size, period;
    private Vector2 init_size;
    private float current_timer = 0;


    void Start()
    {
        size = min_size + Random.Range(0, (max_size - min_size) * 10 + 1) / 10;
        init_size = transform.localScale;
    }
    private void OnEnable()
    {
        init_size = transform.localScale;
    }
    void Update()
    {
        if (current_timer <= period / 2)
        {
            transform.localScale = Vector2.Lerp(init_size, Vector3.one*size, current_timer / period / 2);
            current_timer += Time.deltaTime;
        }
        else if (current_timer < period)
        {
            transform.localScale = Vector2.Lerp(init_size, Vector3.one * size, (period - current_timer) / period / 2);
            current_timer += Time.deltaTime;
        }
        else
        {
            size = min_size +Random.Range(0, (max_size - min_size) * 10 + 1) / 10;
            current_timer = 0;
        }
    }
}