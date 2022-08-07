using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private float moveWallY;
    [SerializeField]
    private float moveDuration;

    private bool check = true;
    private Vector3 endPos;

    private void Start()
    {
        endPos = new Vector3(wall.transform.position.x, wall.transform.position.y + moveWallY, wall.transform.position.z);
    }

    //blocks the player from returning to spawn (by raising a wall) once a trigger is hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check && collision.tag == "Player")
        {
            check = !check;
            StartCoroutine(SpecialTileBehavior.MoveWall(wall, endPos, moveDuration));
        }
    }

}
