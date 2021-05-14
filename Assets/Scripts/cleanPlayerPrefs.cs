using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit() {
        PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Awake()
    {
        //
        //PlayerPrefs.DeleteAll();
    }
}
