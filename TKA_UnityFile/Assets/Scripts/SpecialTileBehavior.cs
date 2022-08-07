using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTileBehavior : MonoBehaviour
{
    //moves the wall from it's current position to a specified end position over a specificed duration, also plays a sound while the wall moves
    public static IEnumerator MoveWall(GameObject wall, Vector3 endPos, float totalTime)
    {
        AudioSource sound = wall.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.loop = true;
            sound.Play();
        }
        Vector3 startPos = wall.transform.position;
        float elapsedTime = 0;
        while (elapsedTime < totalTime)
        {
            wall.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        wall.transform.position = endPos;
        if (sound != null)
        {
            sound.loop = false;
            sound.Stop();
        }
    }
}
