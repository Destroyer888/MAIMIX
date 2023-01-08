using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private Sprite sprite_on, sprite_off;
    [SerializeField] private Image target_gfx;
    public void ChangeSprite(bool i)
    {
        if (i)
            target_gfx.sprite = sprite_on;
        else
            target_gfx.sprite = sprite_off;
    }
}
