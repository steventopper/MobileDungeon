using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemController : MonoBehaviour
{
    public Item.Rarity rarity;
    public Item.Slot slot;
    public float bonusSpeed;
    public float bonusJump;
    public float bonusAttack;
    public float bonusHealth;
    public float cost;

    public Collider2D col;
    public SpriteRenderer spriterender;
    public Sprite boots;
    public Sprite chestplate;
    public Sprite helmet;
    public Sprite sword;
    // Start is called before the first frame update
    void Awake() {
        spriterender = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        col = GetComponent<Collider2D>();
        generate();
        setSprite();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player"){
            
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), col);
        }
    }
    public void setStats(float spd, float jmp, float atk, float hth, float cst)
    {
        bonusSpeed = spd;
        bonusJump = jmp;
        bonusAttack = atk;
        bonusHealth = hth;
        cost = cst;
    }

    public void setStats(Item item)
    {
        bonusSpeed = item.bonusSpeed;
        bonusJump = item.bonusJump;
        bonusAttack = item.bonusAttack;
        bonusHealth = item.bonusHealth;
        cost = item.cost;
    }
    public Item getItem()
    {
        return new Item(rarity,slot,bonusSpeed,bonusJump,bonusAttack,bonusHealth,cost);
    }
    private void setSprite()
    {
        switch (slot)
        {
            case Item.Slot.Chest:
                spriterender.sprite = chestplate;
                //Debug.Log("CHEST");
                break;
            case Item.Slot.Feet:
                spriterender.sprite = boots;
                //Debug.Log("Feet");
                break;
            case Item.Slot.Head:
                spriterender.sprite = helmet;
                //Debug.Log("Helmet");
                break;
            case Item.Slot.Sword:
                spriterender.sprite = sword;
                //Debug.Log("Sword");
                break;
            default:
                //Debug.Log("Error in setSprite");
                break;
        }
    }

    public Item generate()
    {
        int difficulty = 1 + GameObject.Find("Game Engine").GetComponent<EngineController>().floorNum;
        slot = (Item.Slot)Random.Range(0, 4);
        rarity = (Item.Rarity)Random.Range(0, 5);
        float rarityImprovement = 1 + ((int)rarity * 0.25f);
        switch (slot)
        {
            case Item.Slot.Head:
                bonusHealth = bellRandom(0, 10 * rarityImprovement * difficulty);
                bonusAttack = bellRandom(0, 5 * rarityImprovement * difficulty);
                break;
            case Item.Slot.Chest:
                bonusHealth = bellRandom(0, 20 * rarityImprovement * difficulty);
                break;
            case Item.Slot.Feet:
                bonusSpeed = bellRandom(0, 1 * rarityImprovement * difficulty);
                bonusJump = bellRandom(0, 1 * rarityImprovement * difficulty);
                break;
            case Item.Slot.Sword:
                bonusAttack = bellRandom(0, 10 * rarityImprovement * difficulty);
                break;
        }
        cost = bonusSpeed * 10 + bonusJump * 15 + bonusAttack * 2 + bonusHealth;

        return this.getItem();
    }

    private int bellRandom(float min, float max)
    {
        return (int)(Random.Range(min, (min + max)/2) + Random.Range((min + max)/2, max));
    }

}
namespace Items
{
    public class Item
    {
        public enum Rarity { Common, Uncommon, Rare, Epic, Legendary };
        public Rarity rarity;
        public enum Slot { Sword, Head, Chest, Feet };
        public Slot slot;
        public float bonusSpeed;
        public float bonusJump;
        public float bonusAttack;
        public float bonusHealth;
        public float cost;

        public Item(Rarity r, Slot s, float spd, float jmp, float atk, float hth, float cst)
        {
            rarity = r;
            slot = s;
            bonusSpeed = spd;
            bonusJump = jmp;
            bonusAttack = atk;
            bonusHealth = hth;
            cost = cst;
        }
        public Item(Slot s)
        {
            rarity = Rarity.Common;
            slot = s;
            bonusSpeed = 0;
            bonusJump = 0;
            bonusAttack = 0;
            bonusHealth = 0;
            cost = 0;

        }
        public Item(ItemController item)
        {
            rarity = item.rarity;
            slot = item.slot;
            bonusSpeed = item.bonusSpeed;
            bonusJump = item.bonusJump;
            bonusAttack = item.bonusAttack;
            bonusHealth = item.bonusHealth;
            cost = item.cost;
        }

        public void setStats(ItemController item)
        {
            bonusSpeed = item.bonusSpeed;
            bonusJump = item.bonusJump;
            bonusAttack = item.bonusAttack;
            bonusHealth = item.bonusHealth;
            cost = item.cost;
        }

        public override string ToString()
        {
            string ret = rarity.ToString() + " Item\nSlot: " + slot.ToString() + "\n";
            if (bonusAttack > 0)
            {
                ret += "\nAttack: " + bonusAttack;
            }
            if (bonusHealth > 0)
            {
                ret += "\nHealth: " + bonusHealth;
            }
            if (bonusSpeed > 0)
            {
                ret += "\nSpeed: " + bonusSpeed;
            }
            if (bonusJump > 0)
            {
                ret += "\nJump: " + bonusJump;
            }
            return ret;
        }
    }
}