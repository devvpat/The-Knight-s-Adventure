using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoText : MonoBehaviour
{
    private int livesLeft;
    private Text infoText;
    private float roundedTime;

    private void Start()
    {
        livesLeft = GameStateManager.LivesRemaining;
        infoText = this.GetComponent<Text>();
    }

    //this class is used to display lives remaining and current run time in the top right while playing the level
    void Update()
    {
        //rounds time displayed to two decimal places
        roundedTime = Mathf.Round(GameStateManager.timePlayed * 100)/100;
        GameStateManager.timePlayed += Time.deltaTime;
        livesLeft = GameStateManager.LivesRemaining;
        if (infoText != null)
        {
            //displays lives left and time in current run in the top right
            infoText.text = "Lives remaining: " + livesLeft + "\nTime: " + roundedTime;
        }
    }
}
