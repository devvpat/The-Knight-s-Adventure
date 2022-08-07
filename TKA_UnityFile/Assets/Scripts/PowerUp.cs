using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : Item
{
    [SerializeField]
    protected float statModVal;
    [SerializeField]
    protected float modTime;

    protected Animator playerAnim;
    protected BoxCollider2D boxCol2D;

    protected override void Start()
    {
        base.Start();
        playerAnim = player.GetComponent<Animator>();
        boxCol2D = this.GetComponent<BoxCollider2D>();
    }

    //method that does something over a certain amount of time, overriden on a per object type basis
    protected abstract IEnumerator ModifyPlayerStats(float time);

    //colliding with a player starts the special effect of the power up
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Start();
            spriteRend.enabled = false;
            boxCol2D.enabled = false;
            StartCoroutine(ModifyPlayerStats(modTime));
        }
    }
}
