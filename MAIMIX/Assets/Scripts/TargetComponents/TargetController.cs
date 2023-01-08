using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CollidingProcessing), typeof(Rigidbody2D))]
public class TargetController : MonoBehaviour
{
    private PullManager pull_manager;
    private CollidingProcessing coll_processor;
    private Rigidbody2D body;
    private bool isDestroyed = false;
    private void Awake()
    {   
        body = GetComponent<Rigidbody2D>();
        coll_processor = GetComponent<CollidingProcessing>();
        coll_processor.OnCollided += DestroyTarget;
    }
    private void Start()
    {
        pull_manager = PullManager.instance;
    }
    private void OnEnable()
    {
        if (coll_processor == null) return;
        coll_processor.OnCollided += DestroyTarget;
    }
    private void OnDisable()
    {
        coll_processor.OnCollided -= DestroyTarget;
    }

    private void DestroyTarget(Vector2 dir)
    {
        if (isDestroyed) return;
        pull_manager.AddToPull(this);
        AutoMoving[] movables = gameObject.GetComponents<AutoMoving>();
        for(int i = 0; i < movables.Length; i++)
        {
            movables[i].enabled = false;
        }
        body.isKinematic = false;
        body.velocity = Vector2.zero;
        body.AddForce(dir, ForceMode2D.Impulse);
        GameRulesManager.instance.UpdateGamePoints(1);
        TargetsSpawnManager.instance.SpawnTarget();
        isDestroyed = true;
        gameObject.SetActive(false);
    }
    public void ResetTarget()
    {
        body.isKinematic = true;
        body.velocity = Vector2.zero;
        isDestroyed = false;
        AutoMoving[] movables = gameObject.GetComponents<AutoMoving>();
        for (int i = 0; i < movables.Length; i++)
        {
            movables[i].enabled = true;
        }
    }
}
