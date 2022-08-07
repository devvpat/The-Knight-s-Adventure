using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportZonesHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private float waitToRestartOnPlayerDeath;

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player enters a trigger, depending on the trigger's object, do something
        if (collision.gameObject.tag == "Player")
        {
            switch (this.gameObject.tag)
            {
                case "KillZone":
                    StartCoroutine(KillPlayer());
                    break;
                case "EndLevel":
                    GameStateManager.Victory();
                    GameStateManager.QuitToTitle();
                    break;
            }
        }
    }

    //freezes the player's position on death and plays the player death sound, then after waiting for a short amount of time, restarts the current level
    private IEnumerator KillPlayer()
    {
        player.FreezePlayerPos(true);
        player.OnPlayerDeath();
        yield return new WaitForSeconds(waitToRestartOnPlayerDeath);
        GameStateManager.RestartCurrentLevel();
    }
}
