using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
            SceneTracker tracker = GameObject.Find("Game Engine").GetComponent<SceneTracker>();

            SceneManager.LoadScene(engine.OnCollideDoor(gameObject.tag));
            //tracker.setScene(tracker.scenes[engine.OnCollideDoor(gameObject.tag)]);

            //Debug.Log("You moved to a new scene");
        }
    }
}
