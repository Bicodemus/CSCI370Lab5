using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;
    public GameObject deathEffect;

    public int minCold = 0;
    public int currentCold;
    public ColdBar coldBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        currentCold = minCold;
        coldBar.SetMinCold(minCold);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);
        coldBar.SetCold(currentCold);

        if (currentHealth <= 0)
        {
            Die();
        }else if(currentCold == 100)
        {
            TakeDamage(10);
        }
    }


    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Fire"))
        {
            Debug.Log("work");
        }
    }

}
