using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float leftXLim = -0.5f;
    [SerializeField]
    private float rightXLim = 25.5f;
    [SerializeField]
    private float upYLim = 3;
    [SerializeField]
    private float downYLim = 0;
    [SerializeField]
    private float stopCameraLimitsPos;
    [SerializeField]
    private float durationMoveCamToPlayer;

    private bool limitedPos;
    private bool follow;
    private bool movedCamera;

    private void Start()
    {
        limitedPos = true;
        follow = true;
        movedCamera = false;
    }

    //once the player reaches the point where the camera's position isn't limited, the camera moves to the player's position and then is free from position restrictions
    private void Update()
    {
        if (playerTransform.position.x > stopCameraLimitsPos)
        {
            limitedPos = false;
            if (!movedCamera)
            {
                follow = false;
                movedCamera = !movedCamera;
                StartCoroutine(MoveCameraToPlayer());
            }
        }
    }

    //moves the camera to the player's location over a specificed duration
    private IEnumerator MoveCameraToPlayer()
    {
        float timeElapsed = 0;
        while (timeElapsed < durationMoveCamToPlayer)
        {
            float newX = Mathf.Lerp(transform.position.x, playerTransform.position.x, timeElapsed / durationMoveCamToPlayer);
            float newY = Mathf.Lerp(transform.position.y, playerTransform.position.y, timeElapsed / durationMoveCamToPlayer);
            transform.position = new Vector3(newX, newY, transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        follow = true;
    }

    private void LateUpdate()
    {
        //camera follows player horizontally and vertically
        if (follow)
        {
            FollowPlayerX(limitedPos);
            FollowPlayerY(limitedPos);
        }
    }

    //camera moves horizontally if player's X coordinate changes and limits camera's X pos if needed
    private void FollowPlayerX(bool limit)
    {
        if (limit)
        {
            if (transform.position.x != playerTransform.position.x && playerTransform.position.x > leftXLim && playerTransform.position.x < rightXLim)
            {
                var playerX = playerTransform.transform.position.x;
                transform.position = new Vector3(playerX, transform.position.y, transform.position.z);
            }
        }
        else
        {
            var playerX = playerTransform.transform.position.x;
            transform.position = new Vector3(playerX, transform.position.y, transform.position.z);
        }
    }

    //camera moves vertically if player's Y coordinate changes and limits camera's Y pos if needed
    private void FollowPlayerY(bool limit)
    {
        if (limit)
        {
            if (transform.position.y != playerTransform.position.y && playerTransform.position.y > downYLim && playerTransform.position.y < upYLim)
            {
                var playerY = playerTransform.transform.position.y;
                transform.position = new Vector3(transform.position.x, playerY, transform.position.z);
            }
        }
        else
        {
            var playerY = playerTransform.transform.position.y;
            transform.position = new Vector3(transform.position.x, playerY, transform.position.z);
        }
    }
}
