using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float damage;
    public float timeUntilDeletion;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilDeletion = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilDeletion -= Time.deltaTime;
        if (timeUntilDeletion <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<playerController>().Hit(damage);
        }
        GameObject.Destroy(this.gameObject);
    }
}
