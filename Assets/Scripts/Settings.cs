using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Button exitButton;
    public Slider volume;
    public AudioMixer aud;

    private float sliderVal;
    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("MasterVol",0.75f);
        exitButton.onClick.AddListener(exitSettings);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(volume.value == 0)
        {
            sliderVal = -80.0f;
        } else if(volume.value == 1) {
            sliderVal = Mathf.Log10(0.99f)*20;
        } else {
            sliderVal = Mathf.Log10(volume.value)*20;
        }
        aud.SetFloat("MasterVol",sliderVal);
        PlayerPrefs.SetFloat("MasterVol",volume.value);
    }

    public static void exitSettings(){
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        if (engine.sendPlayerBackToPause == 1)
        {
            SceneManager.LoadScene("PauseScreen");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }
    public void Setlevel()
    {
        
    }
}
