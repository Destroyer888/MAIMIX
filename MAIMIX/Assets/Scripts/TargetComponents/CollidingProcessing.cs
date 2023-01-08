using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingProcessing : MonoBehaviour
{
    public delegate void OnCollisitionDelegate(Vector2 dir);
    public event OnCollisitionDelegate OnCollided;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Projectile proj))
        {
            OnCollided?.Invoke(proj.GetVelocity());
            Destroy(proj.gameObject);
        }
    }
}
