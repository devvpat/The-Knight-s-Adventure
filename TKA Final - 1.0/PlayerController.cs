using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigbod2D;
    [SerializeField]
    private SpriteRenderer spriteRend;
    [SerializeField]
    private BoxCollider2D boxCol2D;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private AudioSource playerJumpSound;
    [SerializeField]
    private AudioSource playerDeathSound;

    //groundChecker's transform.pos will be the center of circle to check if the player is touching the ground
    [SerializeField]
    private Transform groundCheckerPos;

    [SerializeField]
    private LayerMask groundLayerMask;

    private bool PlayerIsGrounded;

    //wallCheckRay's transform.pos will be used a origin for a Raycast to see if the player is trying to run into a wall*
    //*players can "stick" to walls mid-air if they continuously run into them
    [SerializeField]
    private Transform wallCheckRayPos;

    private RaycastHit2D hit;

    //stores the player's collider's offsets, used when shifting offsets during sprite image flipping*
    //*the sprite's center includes space taken by sword but my collider doesn't want this so manual offset shifting is a way to circumvent this
    private float[] ColliderOffsets = new float[2];

    private bool hasFrozenPos;

    [SerializeField]
    private float gravity;

    //reference of all checkpoints so the player can teleport to appropriate checkpoint on spawn/respawn
    [SerializeField]
    private List<GameObject> checkpointsList;

    // Start is called before the first frame update
    void Start()
    {
        hasFrozenPos = false;
        rigbod2D.gravityScale = gravity;

        GameStateManager.SpeedMod = 1;
        GameStateManager.JumpMod = 1;

        ColliderOffsets[0] = boxCol2D.offset.x;
        ColliderOffsets[1] = boxCol2D.offset.y;

        PlayerIsGrounded = false;

        int curCP = PlayerPrefs.GetInt("Checkpoint");
        if (curCP > 0)
        {
            transform.position = checkpointsList[curCP - 1].transform.position;
            //in editor the below commented out code worked fine however in builds it never worked so the above is a "brute force" fix
            //it could likely be attributed to all the checkpoints not loading prior to FindObjectsOfType being called
                //Checkpoint[] cpList = FindObjectsOfType<Checkpoint>();
                //foreach (Checkpoint cp in cpList)
                //{
                //    if (cp.ID == curCP)
                //    {
                //        transform.position = cp.transform.position;
                //    }
                //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.IsPaused && !hasFrozenPos)
        {
            //jumps on "space" press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //creates a circle at player's feet to see if the player's feet overlap with the ground layer (used to check if player is grounded)*
                //*used to double check if the player is on the ground in case some unintended collision happens that falsely changes the player's grounded state
                if (PlayerIsGrounded && Physics2D.OverlapCircle(groundCheckerPos.position, 0.2f, groundLayerMask))
                {
                    PlayerIsGrounded = false;
                    playerAnim.SetBool("isGrounded", false);
                    rigbod2D.AddForce(new Vector2(0, GameStateManager.JumpMod * 500));
                    playerJumpSound.Play();
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                //if A is pressed and there is no wall immediately to the left of the player, move left
                hit = Physics2D.Raycast(wallCheckRayPos.position, Vector2.left, 0.28f, groundLayerMask);
                if (hit.collider == null || hit.collider.gameObject.layer != 31)
                {
                    boxCol2D.offset = new Vector2(ColliderOffsets[0] + .21f, ColliderOffsets[1]);
                    spriteRend.flipX = true;
                    transform.position += new Vector3(-4, 0, 0) * GameStateManager.SpeedMod * Time.deltaTime;
                }                
            }
            if (Input.GetKey(KeyCode.D))
            {
                //if D is pressed and there is no wall immediately to the right of the player, move right
                hit = Physics2D.Raycast(wallCheckRayPos.position, Vector2.right, 0.28f, groundLayerMask);
                if (hit.collider == null || hit.collider.gameObject.layer != 31)
                {
                    boxCol2D.offset = new Vector2(ColliderOffsets[0], ColliderOffsets[1]);
                    spriteRend.flipX = false;
                    transform.position += new Vector3(4, 0, 0) * GameStateManager.SpeedMod * Time.deltaTime;
                }
            }
        }
    }

    //sets the player to grounded if they enter a collision with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            playerAnim.SetBool("isGrounded", true);
            PlayerIsGrounded = true;
        }
    }

    //sets the player to not grounded if they exit a collision with the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 31)
        {
            playerAnim.SetBool("isGrounded", false);
            PlayerIsGrounded = false;
        }
    }

    //plays death sound when player dies
    public void OnPlayerDeath()
    {
        playerDeathSound.Play();
    }

    public void FreezePlayerPos(bool freeze)
    {
        rigbod2D.gravityScale = 0;
        hasFrozenPos = freeze;
    }
}
