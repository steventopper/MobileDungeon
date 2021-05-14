using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinController : MonoBehaviour
{
    public float goldValue = 5.0f;
    public GameObject engine;
    public GameObject soundObject;
    //public AudioSource pickupAudio;
    //private CircleCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        engine = GameObject.Find("Game Engine");
        //col = GetComponentInParent<CircleCollider2D>();
        //pickupAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<playerController>().addGold(goldValue);
            engine.GetComponent<EngineController>().addGold(goldValue);
            GameObject.Instantiate<GameObject>(soundObject);
            //pickupAudio.Play();
            Object.Destroy(transform.parent.gameObject);
        }
        
    }
}
