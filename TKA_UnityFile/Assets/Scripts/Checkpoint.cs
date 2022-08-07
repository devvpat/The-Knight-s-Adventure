using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Sprite raisedFlagTexture;
    [SerializeField]
    private int m_Id;
    //turning on the animator moves the flag up, so this variable helps maintain the intended position
    [SerializeField]
    private float animYMove;

    private Animator anim;

    public int ID { get; set; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        ID = m_Id;
    }

    //sets a checkpoint on collision with player if the checkpoint is not active (determined by if the animator is enabled)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !anim.enabled)
        { 
            PlayerPrefs.SetInt("Checkpoint", ID);
            GetComponent<SpriteRenderer>().sprite = raisedFlagTexture;
            transform.position = new Vector3(transform.position.x, transform.position.y + animYMove, transform.position.z);
            anim.enabled = true;
            GameStateManager.SaveGame();
        }
    }
}
