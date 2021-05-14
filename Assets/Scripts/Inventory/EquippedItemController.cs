using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class EquippedItemController : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setItem(Item i)
    {
        item = i;
    }
    Item getItem()
    {
        return item;
    }
}
