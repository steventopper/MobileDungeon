using System.Collections.Generic;
using UnityEngine;
using Items;

public class EngineController : MonoBehaviour
{
    public Item[] playerEquipment;
    public List<Item> playerBackpack;
    public float playerGold = 0f;
    public int sendPlayerBackToPause = 0;

    Layout[] maps = new Layout[3];
    Layout currLayout;
    public int floorNum = 0;
    public float currHealthPercentage;
    private static bool engineExists;
    public bool cheats;

    public Item[] shopItems;
    public bool shopUnlocked;
    public bool[] shopItemBought = { false, false, false };
    
    void Start()
    {
        shopUnlocked = false;
        shopItems = new Item[3];
        currHealthPercentage = 1f;
        cheats = false;
        if (!engineExists)
        {
            engineExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerEquipment = new Item[4];
        playerBackpack = new List<Item>();
        playerEquipment[0] = new Item(Item.Slot.Sword);
        playerEquipment[1] = new Item(Item.Slot.Head);
        playerEquipment[2] = new Item(Item.Slot.Chest);
        playerEquipment[3] = new Item(Item.Slot.Feet);
        //AddItemToBackpack(new Item(Item.Rarity.Legendary, Item.Slot.Chest, 0, 0, 0, 200, 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int OnCollideDoor(string dir)
    {
        if (!dir.Equals("Next") && currLayout.getDirection(dir).isShop)
        {
            OnEnterShop();
            shopUnlocked = true;
        }
        else
        {
            shopItems = new Item[3];
            shopUnlocked = false;
            shopItemBought[0] = false;
            shopItemBought[1] = false;
            shopItemBought[2] = false;
        }
        if (dir.Equals("Next"))
        {
            floorNum++;
            if (floorNum >= maps.Length)
            {
                return 13;
            }
            else
            {
                SceneTracker tracker = GameObject.Find("Game Engine").GetComponent<SceneTracker>();
                tracker.resetVals();
                currLayout = maps[floorNum];
                return currLayout.currRoom.sceneNum;
            }
        }
        return currLayout.goDirection(dir);
    }
    public void addGold(float newGold)
    {
        playerGold += newGold;
    }

    public int OnStart()
    {
        maps[0] = new Layout(1);
        maps[1] = new Layout(1);
        maps[2] = new Layout(1);
        currLayout = maps[0];
        return currLayout.getCurrent();
    }

    public int OnResume()
    {
        return currLayout.getCurrent();
    }

    public void AddItemToBackpack(Item item)
    {
        playerBackpack.Add(item);
    }

    public void resetVals()
    {
        sendPlayerBackToPause = 0;
        currHealthPercentage = 1f;
        playerGold = 0f;
        playerEquipment = new Item[4];
        playerBackpack = new List<Item>();
        playerEquipment[0] = new Item(Item.Slot.Sword);
        playerEquipment[1] = new Item(Item.Slot.Head);
        playerEquipment[2] = new Item(Item.Slot.Chest);
        playerEquipment[3] = new Item(Item.Slot.Feet);
    }

    public void OnToggleCheats(bool val)
    {
        cheats = val;
    }

    public void OnEnterShop()
    {
        shopItems[0] = new ItemController().generate();
        shopItems[1] = new ItemController().generate();
        shopItems[2] = new ItemController().generate();
    }
}

public class Layout
{
    public enum Direction { Right, Left, Up, Down };

    int shopScene = 8;
    int bossScene = 7;
    int startingScene = 6;
    int[] roomScenes = {9, 10, 11, 12, 15, 16, 17, 18, 19};
    int menuScene = 4;
    int gameOverScene = 13;

    public Room currRoom;
    public Layout(int layout)
    {
        switch (layout)
        {
            case 1:
                //Layout: start -> random -> random -> boss -> shop -> menu

                //Initialize rooms
                int int1 = 0;
                int int2 = 0;
                Room entrance = new Room(startingScene);
                while (true)
                {
                    
                    int1 = Random.Range(0, roomScenes.Length);
                    int2 = Random.Range(0, roomScenes.Length);
                    
                    if (int1 != int2)
                    {
                        break;
                    }
                }
                Room random1 = new Room(roomScenes[int1]);
                Room random2 = new Room(roomScenes[int2]);
                Room boss = new Room(bossScene);
                Room shop = new Room(shopScene, true);
                Room menu = new Room(menuScene);
                Room gameOver = new Room(gameOverScene);

                //Link rooms
                entrance.setExit(Direction.Right, random1);
                random1.setExit(Direction.Left, entrance);
                random1.setExit(Direction.Right, random2);
                random2.setExit(Direction.Left, random1);
                random2.setExit(Direction.Right, boss);
                boss.setExit(Direction.Left, random2);
                boss.setExit(Direction.Right, shop);
                shop.setExit(Direction.Left, boss);
                shop.setExit(Direction.Right, gameOver);

                //Set current room as entrance
                currRoom = entrance;
                break;
            case 2:

                break;
        }
    }
    public int goDirection(string dir)
    {
        Direction d = (Direction)System.Enum.Parse(typeof(Direction), dir);
        currRoom = currRoom.getExit(d);
        return currRoom.sceneNum;
    }
    public int getCurrent()
    {
        return currRoom.sceneNum;
    }

    public Room getDirection(string dir)
    {
        Direction d = (Direction)System.Enum.Parse(typeof(Direction), dir);
        return currRoom.getExit(d);
    }

    public class Room
    {
        public int sceneNum;
        Dictionary<Direction, Room> exits;

        public bool isShop;

        public Room(int s)
        {
            exits = new Dictionary<Direction, Room>();
            sceneNum = s;
            isShop = false;
        }
        public Room(int s, bool shop)
        {
            exits = new Dictionary<Direction, Room>();
            sceneNum = s;
            isShop = shop;
        }

        public void setExit(Direction d, Room r)
        {
            exits.Add(d, r);
        }

        public Room getExit(Direction d)
        {
            Room ret;
            if (exits.TryGetValue(d, out ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }
    }
}
