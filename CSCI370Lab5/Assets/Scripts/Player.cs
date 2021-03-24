using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
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
            GameManager.Instance.fire.setCold();
        }
    }
}
