using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour
{
    public GameObject coin;
    public GameObject item;
    public float dropMoney = 50.0f;
    private Collider2D col;
    private SpriteRenderer sprite;
    public Sprite openedSprite;
    public float dropOffset = 5.0f;
    public int opened = 0;
    public int numOfItems = 1;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(opened == 1)
        {
            sprite.sprite = openedSprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && opened == 0){
            opened = 1;
            DropTreasure();
        }
    }
    void DropTreasure()
    {
         for (int i = 0; i < dropMoney/coin.GetComponentInChildren<coinController>().goldValue; i++)
        {
            Object.Instantiate(coin,transform.position+new Vector3(Random.Range(-dropOffset,dropOffset),Random.Range(-dropOffset,dropOffset),0.0f),transform.rotation);
            //Instantiate(coin);
        }
        for (int i = 0; i < numOfItems; i++)
        {
            Object.Instantiate(item,transform.position+new Vector3(Random.Range(-dropOffset,dropOffset),Random.Range(-dropOffset,dropOffset),0.0f),transform.rotation);
        }
        
    }
}
