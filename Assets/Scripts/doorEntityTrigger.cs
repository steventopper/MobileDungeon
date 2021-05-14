using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorEntityTrigger : MonoBehaviour
{
    //private GameObject entityManager;
    private GameObject sceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        //entityManager = GameObject.FindGameObjectWithTag("EntityManager");
        sceneManager = GameObject.Find("Game Engine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            
            //entityManager.GetComponent<EntityTracker>().updateEntityTracker();
            sceneManager.GetComponent<SceneTracker>().updateScene(SceneManager.GetActiveScene().name);
            //sceneManager.GetComponent<SceneTracker>().setScene(SceneManager.GetActiveScene().name);
            
        }
    }
}
