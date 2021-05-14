using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatToggleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        GetComponent<Toggle>().isOn = engine.cheats;
        GetComponent<Toggle>().onValueChanged.AddListener(engine.OnToggleCheats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
