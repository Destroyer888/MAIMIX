using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Vector2 velocity_last_frame;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Init(ProjectilePattern pattern)
    {
        spriteRenderer.sprite = pattern.graphics;
    }
    public void AdjustImpulse(Vector2 impulse)
    {
        rigidbody.velocity = impulse;
    }
    private void LateUpdate()
    {
        velocity_last_frame = rigidbody.velocity;
    }
    public Vector2 GetVelocity()
    {
        return velocity_last_frame;
    }

}
