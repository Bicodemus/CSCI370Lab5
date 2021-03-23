using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearFire : MonoBehaviour
{

    private bool byFire = false;
    public float waitTime = 3f;
    public ColdBar coldbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (byFire)
        {
            StartCoroutine(LowerCold(waitTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
        GameObject player = collision.gameObject;
        if (collision.collider.GetType() == typeof(BoxCollider))
        {
            if (player.CompareTag("Player"))
            {
                byFire = true;
            }
            Debug.Log("Enter");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        GameObject player = collision.gameObject;
        if (collision.collider.GetType() == typeof(BoxCollider))
        {
            if (player.CompareTag("Player"))
            {
                byFire = false;
            }
            Debug.Log("Exit");
        }
    }

    IEnumerator LowerCold(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
<<<<<<< Updated upstream
        coldbar.SetCold(+1);
=======
        coldbar.SetCold(-1);
>>>>>>> Stashed changes
    }
}
