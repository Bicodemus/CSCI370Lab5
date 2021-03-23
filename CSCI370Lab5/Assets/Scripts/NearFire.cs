using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearFire : MonoBehaviour
{

    public ColdBar coldbar;
    [Range(1, 2)]
    public float warmSpeed = 1f;
    public GameObject player;

    private float currCold;
    private bool warming = false;

    // Start is called before the first frame update
    void Start()
    {
        currCold = coldbar.slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (currCold > 0 && currCold <= 100) 
        {
            coldbar.SetCold(currCold);
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
    }

}
