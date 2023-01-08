using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class of Targets", order = 52)]
public class TargetsClass : ScriptableObject
{
    public List<TargetController> targets_in_class;
    public int targets_class_count = 3;
}

