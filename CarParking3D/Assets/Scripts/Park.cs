using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    public Route route;

    [SerializeField] SpriteRenderer spriteRenderer;
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
