using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectController : MonoBehaviour
{
    private float aliveTime = 0f;
    public float maxaliveTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime >= maxaliveTime)
        {
            Object.Destroy(transform.gameObject);
        }
    }
}
