using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthBar;
    public float health;
    public float startHealth;
    public GameObject player;
    private playerController pc;
    EngineController engine;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<playerController>();
        engine = GameObject.Find("Game Engine").GetComponent<EngineController>();
        pc.health = engine.currHealthPercentage * startHealth;
        health = engine.currHealthPercentage * startHealth;
    }
    private void Update() {
        health = pc.health;
        healthBar.fillAmount = health/startHealth;
        engine.currHealthPercentage = healthBar.fillAmount;
    }

    public void setStartHealth(float h)
    {
        startHealth = h;
    }
  /*
  public void onTakeDamage(float damage)
  {
    health = health - damage;
    healthBar.fillAmount = health / startHealth;
  }
  */
}
