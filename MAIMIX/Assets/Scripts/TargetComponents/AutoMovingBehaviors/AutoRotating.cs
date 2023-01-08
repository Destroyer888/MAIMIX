using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotating : AutoMoving
{
    [SerializeField] float rotating_speed = 100f;
    public int rand;
    private void Start()
    {
        if(Random.Range(0, 2)==1)
        {
            rand = -1;
        }
        else
        {
            rand = 1;
        }
        rand*=Random.Range(1, 4);
    }
    void Update()
    {
        transform.Rotate(new Vector3(0,0,rotating_speed * Time.deltaTime*rand));    
    }
}
