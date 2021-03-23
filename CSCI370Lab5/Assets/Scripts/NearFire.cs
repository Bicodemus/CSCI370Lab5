using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearFire : MonoBehaviour
{

    private bool byFire = false;
    public float waitTime = 3f;
    public ColdBar coldbar;

    private SphereCollider sphere;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TEST");
        sphere = gameObject.GetComponent<SphereCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (byFire)
        {
            StartCoroutine(LowerCold(waitTime));
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yes");
        }
    }

    /* private void OnCollisionStay(Collision collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
             Debug.Log("Yes");
         }
     }*/



    IEnumerator LowerCold(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        coldbar.SetCold(-1);
    }
}
