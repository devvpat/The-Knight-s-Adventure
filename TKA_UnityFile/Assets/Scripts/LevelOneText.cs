using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this class displays different messages depending on the player's x pos (and y pos for the final message)

public class LevelOneText : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Text sceneText;
    [SerializeField]
    private float showAppleInfoTextPosX;
    [SerializeField]
    private float showMelonInfoTextPosX;
    [SerializeField]
    private float showChestInfoTextPosX;
    [SerializeField]
    private float showEndTutorialTextPosX;
    [SerializeField]
    private float showNoTextPosX;
    [SerializeField]
    private float showFinalTextPosX;
    [SerializeField]
    private float showFinalTextPosY;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > showFinalTextPosX && playerTransform.position.y > showFinalTextPosY)
        {
            ShowFinalText();
        }
        else if (playerTransform.position.x > showNoTextPosX)
        {
            ShowNoText();
        }
        else if (playerTransform.position.x > showEndTutorialTextPosX)
        {
            ShowEndTutorialText();
        }
        else if (playerTransform.position.x > showChestInfoTextPosX)
        {
            ShowFinalInfoText();
        }
        else if (playerTransform.position.x > showMelonInfoTextPosX)
        {
            ShowMelonInfoText();
        }
        else if (playerTransform.position.x > showAppleInfoTextPosX)
        {
            ShowAppleInfoText();
        }
        else
        {
            ShowControlsInfoText();
        }
    }

    private void ShowAppleInfoText()
    {
        sceneText.text = "Touch an APPLE to temporarily\nincrease your SPEED\n\nPress 'R' to restart current level*\n*Costs 1 life to restart...";
    }

    private void ShowMelonInfoText()
    {
        sceneText.text = "Touch a flag to set\na new checkpoint\n\nTouch a MELON to temporarily\nincrease your JUMP HEIGHT";
    }

    private void ShowFinalInfoText()
    {
        sceneText.text = "Left-click a chest when next to it\nto alter the map in someway\n\nPress 'M' to return to the\nmain menu (saves progress)";
    }

    private void ShowControlsInfoText()
    {
        sceneText.text = "Hold 'A' to move left\nHold 'D' to move right\nPress 'Space' to jump";
    }

    private void ShowEndTutorialText()
    {
        sceneText.text = "The tutorial is now over\n\nEnjoy this short platformer\nwhere you venture to\nfind a hidden treasure!";
    }

    private void ShowNoText()
    {
        sceneText.text = "";
    }

    private void ShowFinalText()
    {
        sceneText.text = "You found the treasure! But it looks\nlike someone beat you to it...\nIt seems like another adventure is\nneeded to find this missing teasure...\n\nWalk to the banner to finish this adventure\nand save your current time";
    }
}
