using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TentionController))]
public class FireController : MonoBehaviour
{
    [SerializeField] private ProjectilePattern current_projectile_pattern;
    private Projectile current_projectile;
    [SerializeField] private Projectile projectile_prefab;
    private TentionController current_tention_controller;

    [SerializeField] private float projectile_speed_multiplier = 1f;

    [SerializeField] private Transform gunpoint;
    void Start()
    {
        current_tention_controller = GetComponent<TentionController>();
        current_tention_controller.OnReleased += Fire;
    }
    private void OnEnable()
    {
        if (current_tention_controller == null)
            return;
        current_tention_controller.OnReleased += Fire;
    }
    private void OnDisable()
    {
        current_tention_controller.OnReleased -= Fire;
    }

    private void Fire(Vector2 dir)
    {
        current_projectile = Instantiate(projectile_prefab, gunpoint.position, Quaternion.Euler(0,0,MathHelper.CalculateAngleFromVector(dir)+90), null);
        current_projectile.Init(current_projectile_pattern);
        current_projectile.AdjustImpulse(dir * projectile_speed_multiplier);
    }
}
