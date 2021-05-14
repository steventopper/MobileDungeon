using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CircleCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player"){
            
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), col);
        }
    }
}
