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
    public float currentCold;
    public ColdBar coldBar;
    public bool isCold;

    public float warmSpeed = 2.5f;
    private float change = 0f;


    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        currentCold = coldBar.slider.value;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        coldBar.SetMinCold(minCold);

    }

    // Update is called once per frame
    void Update()
    {
        isCold = !manager.isWarm();


        if (Input.GetKeyDown("space"))
        {
            TakeDamage(10);
        }

        if (isCold)
        {

            if (currentCold >= 0 && currentCold <= 100)
            {
                change += 1 * Time.deltaTime * warmSpeed;
                coldBar.SetCold(currentCold + change);
            }

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
        }
        else if (currentCold == 100)
        {
            if (!isCold)
            {
                Cold();
                isCold = true;
            }
            TakeDamage(10);
        }
        else
        {
            if (isCold)
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
