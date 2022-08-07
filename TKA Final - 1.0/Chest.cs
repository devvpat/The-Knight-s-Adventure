using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Item
{
    [SerializeField]
    private GameObject wallToBeBroken;
    [SerializeField]
    private float moveWallX;
    [SerializeField]
    private float moveWallY;
    [SerializeField]
    private Sprite openTexture;

    //clicking on a chest when player is near it causes it to open and break (move) a wall
    private void OnMouseDown()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 1.4f)
        {
            spriteRend.sprite = openTexture;
            Vector3 moveToPos = new Vector3(wallToBeBroken.transform.position.x + moveWallX, wallToBeBroken.transform.position.y + moveWallY, wallToBeBroken.transform.position.z);
            StartCoroutine(SpecialTileBehavior.MoveWall(wallToBeBroken, moveToPos, 2.5f));
        }
    }
}
