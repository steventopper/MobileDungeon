using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource background;
    
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") && background.isPlaying == false)
        {
            background.Play();
        }
        if (GameObject.Find("Player") == false)
        {
            background.Pause();   
        }
    }
}
