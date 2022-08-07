using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : PowerUp
{
    //over the interval of a given time, modify speed stat and double animaiton speed
    protected override IEnumerator ModifyPlayerStats(float time)
    {
        GameStateManager.SpeedMod *= statModVal;
        playerAnim.SetFloat("animSpeedMultiplier", 1.2f);
        yield return new WaitForSeconds(time);
        playerAnim.SetFloat("animSpeedMultiplier", 0.6f);
        GameStateManager.SpeedMod /= statModVal;
    }
}
