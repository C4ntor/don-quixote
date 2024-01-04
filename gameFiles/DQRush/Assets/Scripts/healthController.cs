using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthController : MonoBehaviour
{
    public Image healthbar;
    [SerializeField] private Text counter;
    private int timer;
    public bool isAlive = true;
    public float health = 100f;
    [SerializeField] private bool isPlayer = false;
    AudioSource[] asources;

    public void updateHealthBar() {
        healthbar.fillAmount = health/100f;
    }

    private void Awake()
    {
        asources = GetComponents<AudioSource>(); 
    }

   

    public void takeDamage(float damage)
    {
        health = health -damage;
        if (isPlayer) updateHealthBar();
        if (health <= 0)
        {
            health = 0;
            isAlive = false;
        }

    }

    public void restoreHealth(float aid)
    {
        health = health + aid;
        if (isPlayer) {
            updateHealthBar();
            asources[1].Play();
        } 
        if (health >= 200f)
        {
            health = 200f;
        }

    }



}
