using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PullManager : CustomSingleton<PullManager>
{
    private Queue<TargetController> targets_pull = new Queue<TargetController>();
    private Queue<Projectile> projectiles_pull = new Queue<Projectile>();
    [SerializeField] private int projectiles_pull_capacity = 7, targets_pull_capacity = 5;
    public void AddToPull<T>(T obj) where T: MonoBehaviour
    {

        switch (obj)
        {
            case TargetController:
                if (obj.TryGetComponent(out TargetController target) && targets_pull.Count < targets_pull_capacity)
                {
                    targets_pull.Enqueue(target);
                }
                break;
            case Projectile:
                if(obj.TryGetComponent(out Projectile proj) && projectiles_pull.Count < projectiles_pull_capacity)
                    projectiles_pull.Enqueue(proj);
                break;
        }
    }
    public T TryGetFromPull<T>(T obj) where T : MonoBehaviour
    {
        switch (obj)
        {
            case TargetController:
                if (targets_pull.Count == 0 || targets_pull.Count < targets_pull_capacity) return null;
                return targets_pull.Dequeue() as T;
            case Projectile:
                if (projectiles_pull.Count == 0 || projectiles_pull.Count < projectiles_pull_capacity) return null;
                return projectiles_pull.Dequeue() as T;
            default:
                return null;
        }
    }
    public void ClearPull<T>(T obj) where T : MonoBehaviour
    {
        int a;
        switch (obj)
        {
            case TargetController:
                a = targets_pull.Count;
                for (int i = 0; i < a; i++)
                {
                    Destroy(targets_pull.Dequeue().gameObject, i);
                }
                break;
            case Projectile:
                a = projectiles_pull.Count;
                for (int i = 0; i < a; i++)
                    Destroy(projectiles_pull.Dequeue(), i);
                break;
        }
    }
}
