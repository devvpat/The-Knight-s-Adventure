using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : PowerUp
{
    //over the interval of a given time, modify jump height stat and double animaiton speed
    protected override IEnumerator ModifyPlayerStats(float time)
    {
        GameStateManager.JumpMod *= statModVal;
        playerAnim.SetFloat("animSpeedMultiplier", 1.2f);
        yield return new WaitForSeconds(time);
        playerAnim.SetFloat("animSpeedMultiplier", 0.6f);
        GameStateManager.JumpMod /= statModVal;
    }
}
