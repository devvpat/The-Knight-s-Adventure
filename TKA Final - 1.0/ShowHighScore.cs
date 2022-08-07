using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;

    void Start()
    {
        textComponent = this.GetComponent<Text>();
    }
    
    //displays the high score on the main menu in seconds with two decimal places (max = 99999 seconds)
    void Update()
    {
        if (textComponent != null)
        {
            float time = Mathf.Round(PlayerPrefs.GetFloat("HighScore") * 100) / 100;
            if (time > 99999) time = 99999;
            textComponent.text = "Fastest Completion Time (seconds): " + time;
        }
    }
}
