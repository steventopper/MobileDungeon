using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;
using TMPro;

public class ShopButtonManager : MonoBehaviour
{
    public GameObject[] shopButtons = new GameObject[3];

    public Sprite sword;
    public Sprite head;
    public Sprite chest;
    public Sprite feet;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateShop();
        if (!GameObject.Find("Game Engine").GetComponent<EngineController>().shopUnlocked)
        {
            GetComponent<TextMeshProUGUI>().text = "Current room does not have access to shop.";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemSelect()
    {
        foreach (GameObject b in shopButtons)
        {
            b.GetComponent<Button>().interactable = true;
        }
        foreach(GameObject b in GameObject.FindGameObjectsWithTag("Item Button"))
        {
            b.GetComponent<Button>().interactable = true;
        }
    }

    public void InstantiateShop()
    {
        Item[] shopItems = GameObject.Find("Game Engine").GetComponent<EngineController>().shopItems;
        for (int x = 0; x < 3; x++)
        {
            ShopButtonController controller = shopButtons[x].GetComponent<ShopButtonController>();
            controller.buttonManager = this;
            controller.item = shopItems[x];
            if (controller.item != null)
            {
                switch (controller.item.slot)
                {
                    case Item.Slot.Sword:
                        shopButtons[x].transform.GetChild(1).GetComponent<Image>().sprite = sword;
                        break;
                    case Item.Slot.Head:
                        shopButtons[x].transform.GetChild(1).GetComponent<Image>().sprite = head;
                        break;
                    case Item.Slot.Chest:
                        shopButtons[x].transform.GetChild(1).GetComponent<Image>().sprite = chest;
                        break;
                    case Item.Slot.Feet:
                        shopButtons[x].transform.GetChild(1).GetComponent<Image>().sprite = feet;
                        break;
                }
            }
            EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
            if (engine.shopItemBought[x] == true || !GameObject.Find("Game Engine").GetComponent<EngineController>().shopUnlocked)
            {
                shopButtons[x].SetActive(false);
            }
        }
    }
}
