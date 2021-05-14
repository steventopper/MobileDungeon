using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Setup() {
        gameObject.SetActive(true);
    }

    public void RestartButton() {
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        engine.resetVals();
        SceneTracker tracker = GameObject.Find("Game Engine").GetComponent<SceneTracker>();
        tracker.resetVals();
        //PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(engine.OnStart());
    }

    public void ExitButton() {
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        engine.resetVals();
        SceneTracker tracker = GameObject.Find("Game Engine").GetComponent<SceneTracker>();
        tracker.resetVals();
        //PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
}
