using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public Button resume;
    public Button inventory;
    public Button mainMenu;
    public Button settings;

    void Start()
    {
      resume.onClick.AddListener(ResumeGame);
      inventory.onClick.AddListener(Inventory);
      mainMenu.onClick.AddListener(GoToMainMenu);
      settings.onClick.AddListener(GoToSettings);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          if(isPaused)
          {
            ResumeGame();
          }
          else
          {
            PauseGame();
          }
        }
    }

    public static void PauseGame()
    {
      SceneManager.LoadScene("PauseScreen");
      //Time.timeScale = 0f;
      isPaused = true;
    }

    public void ResumeGame()
    {
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        SceneManager.LoadScene(engine.OnResume());
        //Time.timeScale = 1f;
      isPaused = false;
    }

    public void GoToMainMenu()
    {
      //Time.timeScale = 1f;
      EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
      SceneManager.LoadScene("MainMenu");
      engine.sendPlayerBackToPause = 0;
    }

    public void Inventory()
    {
      //Time.timeScale = 0f;
      SceneManager.LoadScene("Inventory");
    }

    public void GoToSettings()
    {
      EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
      engine.sendPlayerBackToPause = 1;
      SceneManager.LoadScene("Settings");
    }
}
