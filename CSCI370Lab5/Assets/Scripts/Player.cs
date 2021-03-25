using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private bool nearFire;
    private float tempChange = 100;
    public float warmSpeed = 1f;
    public float coolSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (nearFire)
        {
            tempChange -= (1f * Time.deltaTime) * warmSpeed ;
        } else
        {
            tempChange += (1f * Time.deltaTime) * coolSpeed;
        }
        if (tempChange < 10)
        {
            tempChange = 10;
        }
        if (tempChange > 100)
        {
            tempChange = 100;
        }
        GameManager.Instance.setCold(tempChange);
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("zombie"))
    //    {
    //        TakeDamage(1);
    //        Debug.Log("munch");
    //    }
    //}

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("zombie"))
        {
            GameManager.Instance.TakeDamage(1);
            Debug.Log("munch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            nearFire = true;
            /*GameManager.Instance.fire.setCold();*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            nearFire = false;
            /*GameManager.Instance.fire.setCold();*/
        }
    }
}
