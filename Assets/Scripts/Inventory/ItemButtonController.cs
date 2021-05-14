using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;

public class ItemButtonController : MonoBehaviour
{
    public Item item;
    public GameObject activeText;
    public ItemButtonManager buttonManager;
    public GameObject descriptionBox;
    // Start is called before the first frame update
    void Start()
    {
        descriptionBox = GameObject.Find("Description Box");
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

    public void OnClick()
    {
        buttonManager.OnItemSelect();
        this.GetComponent<Button>().interactable = false;
        descriptionBox.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        descriptionBox.transform.GetChild(0).GetComponent<Text>().text = item.ToString();
        GameObject.Find("Equip Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Equip Button").GetComponentInChildren<Text>().text = "Equip Selected";
    }
    
    public void setTextActive(bool b)
    {
        activeText.SetActive(b);
    }

    public bool getTextActive()
    {
        return activeText.activeSelf;
    }
}
