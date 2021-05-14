using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSetter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameEngine;
    void Start()
    {
        gameEngine = GameObject.Find("Game Engine");
        gameEngine.GetComponent<SceneTracker>().setScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
