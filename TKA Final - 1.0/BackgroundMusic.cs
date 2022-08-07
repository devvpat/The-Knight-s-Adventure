using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private float startMusicAtXPos;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private float musicVolume;
    [SerializeField]
    private float musicPitch;

    //once the player reaches a specific point, turn on looping background music
    private void Update()
    {
        if (!music.isPlaying && playerTransform.position.x >= startMusicAtXPos)
        {
            music.volume = musicVolume;
            music.pitch = musicPitch;
            music.loop = true;
            music.Play();
        }
    }
}
