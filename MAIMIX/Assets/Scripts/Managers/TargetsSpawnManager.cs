using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsSpawnManager : CustomSingleton<TargetsSpawnManager>
{
    public List<TargetsClass> Classes = new List<TargetsClass>();
    [SerializeField] private TargetController current_target;
    private PullManager pull_manager;
    [SerializeField] private int current_class = 0, targets_class_remaining;
    private Vector2 move_vector;
    public delegate void TSManager(float t);
    public event TSManager OnTargetSpawned;
    private Vector2 last_position = Vector2.zero;
    private void Start()
    {
        move_vector = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height + 1f));
        pull_manager = PullManager.instance;
        targets_class_remaining = Classes[current_class].targets_class_count;
        SpawnTarget();
    }
    public void SpawnTarget()
    {
        targets_class_remaining--;
        if (targets_class_remaining+1 <= 0)
        {
            SwitchClass();
        }
        last_position += move_vector;
        current_target = pull_manager.TryGetFromPull(current_target);
        if(current_target == null)
            current_target = Instantiate(Classes[current_class].targets_in_class[Random.Range(0, Classes[current_class].targets_in_class.Count)],
                                        last_position, 
                                        Quaternion.identity, 
                                        null);
        current_target.gameObject.SetActive(true);
        current_target.transform.position = last_position; 
        current_target.ResetTarget();

        
        OnTargetSpawned?.Invoke(last_position.y);
    }
    public void SwitchClass()
    {
        if (current_class == Classes.Count - 1)
            return;
        pull_manager.ClearPull(current_target);
        current_class++;
    }
}
