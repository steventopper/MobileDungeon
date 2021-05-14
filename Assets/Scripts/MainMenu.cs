using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button instructions;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(goToSettings);
        instructions.onClick.AddListener(goToInstructions);
    }

    public static void StartGame()
    {
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        SceneManager.LoadScene(engine.OnStart());
        Debug.Log("You moved to a new scene");
    }
    public static void goToSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public static void goToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
