using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;
    public GameObject deathEffect;
    public GameObject coldImage;

    public int minCold = 0;
    public int currentCold;
    public ColdBar coldBar;
    public bool isCold = false;

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
            TakeDamage(10);
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
            if (!isCold)
            {
                Cold();
                isCold = true;
            }
            TakeDamage(10);
        } else
		{
            if(isCold)
			{
                NotCold();
                isCold = false;
			}
		}
    }
    // Enables or disables a chilled border
    IEnumerator ColdColorLerp(Color endValue, float duration)
    {
        float time = 0;
        Image sprite = coldImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
    }
    public void Cold()
    {
        StartCoroutine(ColdColorLerp(new Color(1, 1, 1, 1), 2));
    }
    public void NotCold()
    {
        StartCoroutine(ColdColorLerp(new Color(0, 0, 0, 0), 2));
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
