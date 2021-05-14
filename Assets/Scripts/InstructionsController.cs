using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(goToMain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void goToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
