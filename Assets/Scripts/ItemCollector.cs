using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
public class ItemCollector : MonoBehaviour
{
    public ItemController theItemController;
    // Start is called before the first frame update
    void Start()
    {
        theItemController = transform.parent.gameObject.GetComponent<ItemController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            
            Item newItem = theItemController.getItem();
            GameObject.Find("Game Engine").GetComponent<EngineController>().playerBackpack.Add(newItem);
            GameObject.Find("PlayerUICanvas").GetComponent<ItemPickUp>().ChangeItemText(newItem.rarity + " " + newItem.slot + " acquired");
            Object.Destroy(transform.parent.gameObject);
        }
        
    }
}
