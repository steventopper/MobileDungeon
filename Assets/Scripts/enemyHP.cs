using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
    Put this script on all monsters created
    The player will call the Hit method to remove hp
    you can use getHP() to see your hp from controller
    script and use getHit() and hitRecieved() to see if your
    monster recently got hit.
*/
public class enemyHP : MonoBehaviour
{
    [SerializeField]
    public float health = 100f;
    private bool dead = false;
    private bool hit = false;
    public GameObject coin;
    public float dropMoney;
    public float timeBeforeDeath=1.0f;
    public GameObject entityManager;
    public string currScene;

    private void Start() {
        entityManager = GameObject.FindGameObjectWithTag("EntityManager");
        currScene = SceneManager.GetActiveScene().name;
    }
    public float getHP()
    {
        return health;
    }
    public void setHP(float hp)
    {
        health = hp;
    }
    public void hitReceived()
    {
        hit = false;
    }
    public bool getHit()
    {
        return hit;
    }
    public void Hit(float dmg)
    {
        hit = true;
        health -= dmg;
    }
    //When health = drop items, gold and keep it from playing death animation
    void Update() {
        if(health <= 0 && dead == false){
            dead = true;
            //entityManager.GetComponent<EntitySetter>().enemies.Remove(transform.gameObject);
            //entityManager.GetComponent<EntityTracker>().enemies.Remove(transform.gameObject);
            //PlayerPrefs.DeleteKey(currScene+transform.gameObject.name+"x");
            //PlayerPrefs.DeleteKey(currScene+transform.gameObject.name+"y");
            //PlayerPrefs.DeleteKey(currScene+transform.gameObject.name+"hp");
            //Destroy(transform.gameObject);
            Drops();
        }    
    }
    void Drops(){
        for (int i = 0; i < dropMoney/coin.GetComponentInChildren<coinController>().goldValue; i++)
        {
            Object.Instantiate(coin,transform.position,transform.rotation);
            //Instantiate(coin);
        }
        
    }
}