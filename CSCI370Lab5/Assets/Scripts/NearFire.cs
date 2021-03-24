using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearFire : MonoBehaviour
{

    public ColdBar coldbar;
    public float warmSpeed = 1f;
    public Player player;

    private float currCold;
    private bool warming = false;
    
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        manager.byFire(warming);
        if (currCold >= 0 && currCold <= 100) 
        {
            coldbar.SetCold(currCold);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currCold = coldbar.slider.value;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        warming = true;
        if (other.gameObject.CompareTag("Player"))
        {
            currCold -= 1 * Time.deltaTime * warmSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        warming = false;
        GameManager.Instance.setCold(coldbar.slider.value);
    }

    public void setCold()
    {
        currCold = GameManager.Instance.currentCold;
    }

}
