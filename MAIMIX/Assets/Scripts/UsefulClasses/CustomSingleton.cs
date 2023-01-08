using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    private void Awake()
    {
        if (instance == null)
            instance = GetComponent<T>();
        else
            Destroy(instance);
    }
}
