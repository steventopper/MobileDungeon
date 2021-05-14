using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGoldDisplayer : MonoBehaviour
{
    private EngineController engine;
    // Start is called before the first frame update
    void Start()
    {
        engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = engine.playerGold.ToString();
    }
}
