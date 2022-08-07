using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;

    protected SpriteRenderer spriteRend;

    protected virtual void Start()
    {
        spriteRend = this.GetComponent<SpriteRenderer>();
    }
}
