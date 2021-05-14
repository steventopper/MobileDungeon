using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    public Item item;
    public ShopButtonManager buttonManager;
    public GameObject descriptionBox;
    // Start is called before the first frame update
    void Start()
    {
        descriptionBox = GameObject.Find("Description Box");
        GetComponentInChildren<Text>().text = item.cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInChildren<Text>().text == "0")
        {
            GetComponentInChildren<Text>().text = item.cost.ToString();
        }
    }

    public void OnClick()
    {
        buttonManager.OnItemSelect();
        this.GetComponent<Button>().interactable = false;
        descriptionBox.transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.UpperLeft;
        descriptionBox.transform.GetChild(0).GetComponent<Text>().text = item.ToString();
        if (GameObject.Find("Game Engine").GetComponent<EngineController>().playerGold >= item.cost) {
            GameObject.Find("Equip Button").GetComponent<Button>().interactable = true;
            GameObject.Find("Equip Button").GetComponentInChildren<Text>().text = "Buy Selected";
        }
        else
        {
            GameObject.Find("Equip Button").GetComponent<Button>().interactable = false;
            GameObject.Find("Equip Button").GetComponentInChildren<Text>().text = "Not Enough Gold";
        }
    }
}
