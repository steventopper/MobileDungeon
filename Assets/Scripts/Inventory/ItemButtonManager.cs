using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Items;

public class ItemButtonManager : MonoBehaviour
{
    GameObject[] itemButtons;
    public GameObject backpack;
    public GameObject backpackContents;
    public GameObject buttonPrefab;

    public Sprite sword;
    public Sprite head;
    public Sprite chest;
    public Sprite feet;
    // Start is called before the first frame update
    void Start()
    {
        /**addItemToBackpack(new Item(Item.Rarity.Uncommon, Item.Slot.Sword, 0, 0, 15, 0, 10));
        addItemToBackpack(new Item(Item.Rarity.Common, Item.Slot.Sword, 0, 0, 10, 0, 10));
        addItemToBackpack(new Item(Item.Rarity.Rare, Item.Slot.Sword, 0, 0, 20, 0, 10));
        addItemToBackpack(new Item(Item.Rarity.Uncommon, Item.Slot.Feet, 1, 3, 0, 0, 10));
        addItemToBackpack(new Item(Item.Rarity.Legendary, Item.Slot.Chest, 0, 0, 50, 0, 10));
        addItemToBackpack(new Item(Item.Rarity.Rare, Item.Slot.Chest, 0, 0, 0, 25, 10));**/
        GameObject.Find("Equip Button").GetComponent<Button>().interactable = false;
        instantiateBackpack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemSelect()
    {
        foreach (GameObject b in itemButtons)
        {
            b.GetComponent<Button>().interactable = true;
        }
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Shop Item Button"))
        {
            b.GetComponent<Button>().interactable = true;
        }
    }

    public void OnEquipButton()
    {
        if (GameObject.Find("Equip Button").GetComponentInChildren<Text>().text == "Buy Selected")
        {
            GameObject[] itemButtons = GameObject.FindGameObjectsWithTag("Shop Item Button");
            for (int x = 0; x < 3; x++)
            {
                if (itemButtons[x].GetComponent<Button>().interactable == false)
                {
                    EngineController engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
                    int val = 0;
                    int.TryParse(itemButtons[x].gameObject.name, out val);
                    engine.shopItemBought[val] = true;
                    ShopButtonController temp = itemButtons[x].GetComponent<ShopButtonController>();
                    engine.playerGold -= temp.item.cost;
                    itemButtons[x].SetActive(false);
                    engine.AddItemToBackpack(temp.item);
                    instantiateBackpack();
                    break;
                }
            }
        }
        else
        {
            Item selectedItem = null;
            Item[] equippedItems = GameObject.Find("Game Engine").GetComponent<EngineController>().playerEquipment;
            foreach (GameObject b in itemButtons)
            {
                if (!b.GetComponent<Button>().interactable)
                {
                    selectedItem = b.GetComponent<ItemButtonController>().item;
                    int slotNum = 0;
                    switch (b.GetComponent<ItemButtonController>().item.slot)
                    {
                        case Item.Slot.Sword: //slot 0
                            slotNum = 0;
                            break;
                        case Item.Slot.Head: //slot 1
                            slotNum = 1;
                            break;
                        case Item.Slot.Chest: //slot 2
                            slotNum = 2;
                            break;
                        case Item.Slot.Feet: //slot 3
                            slotNum = 3;
                            break;
                    }
                    if (b.GetComponent<ItemButtonController>().getTextActive())
                    {
                        equippedItems[slotNum] = new Item(equippedItems[slotNum].slot);
                        b.GetComponent<ItemButtonController>().setTextActive(false);
                    }
                    else
                    {
                        equippedItems[slotNum] = selectedItem;
                        b.GetComponent<ItemButtonController>().setTextActive(true);
                    }
                }
            }
            if (selectedItem == null)
            {
                return;
            }
            foreach (GameObject b in itemButtons)
            {
                if (b.GetComponent<Button>().interactable && b.GetComponent<ItemButtonController>().item.slot == selectedItem.slot)
                {
                    b.GetComponent<ItemButtonController>().setTextActive(false);
                }
            }
        }
    }

    /**public void addItemToBackpack(Item item)
    {
        backpackItems.Add(item);
    }**/

    public void instantiateBackpack()
    {
        int x = 0;
        int y = 0;
        List<Item> backpackItems = GameObject.Find("Game Engine").GetComponent<EngineController>().playerBackpack;
        Item[] equippedItems = GameObject.Find("Game Engine").GetComponent<EngineController>().playerEquipment;
        foreach (Item item in backpackItems)
        {
            GameObject tempButton = Instantiate(buttonPrefab, new Vector3(backpack.transform.position.x + (Screen.width / 15) + (Screen.width / 10) * x, backpack.transform.position.y - (Screen.height / 10) - (Screen.height / 5) * y), backpackContents.transform.rotation, backpack.transform);
            tempButton.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            //Debug.Log(tempButton.transform.localPosition);
            tempButton.GetComponent<ItemButtonController>().buttonManager = this;
            tempButton.GetComponent<ItemButtonController>().item = item;
            tempButton.GetComponent<ItemButtonController>().setTextActive(false);
            x++;
            if (x >= 4)
            {
                x = 0;
                y++;
            }
            foreach (Item item2 in equippedItems)
            {
                if (item == item2)
                {
                    tempButton.GetComponent<ItemButtonController>().setTextActive(true);
                }
            }
            switch (item.slot)
            {
                case Item.Slot.Sword:
                    tempButton.GetComponent<Image>().sprite = sword;
                    break;
                case Item.Slot.Head:
                    tempButton.GetComponent<Image>().sprite = head;
                    break;
                case Item.Slot.Chest:
                    tempButton.GetComponent<Image>().sprite = chest;
                    break;
                case Item.Slot.Feet:
                    tempButton.GetComponent<Image>().sprite = feet;
                    break;
            }
        }
        itemButtons = GameObject.FindGameObjectsWithTag("Item Button");
    }
}
