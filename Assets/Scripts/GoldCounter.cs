using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldCounter : MonoBehaviour
{
    //public GameObject player;
    public GameObject engine;
    private Text goldCount;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        engine = GameObject.Find("Game Engine");
        goldCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        goldCount.text = engine.GetComponent<EngineController>().playerGold.ToString();//player.GetComponent<playerController>().gold.ToString();
    }
}
