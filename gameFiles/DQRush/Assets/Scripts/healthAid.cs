using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class healthAid : MonoBehaviour
{
    public float aid;
    AudioSource sound;

    private void Awake()
    {
        Destroy(gameObject,10);
    }

    private void Update()
    {
        transform.Rotate(0, 0, 200 * Time.deltaTime);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            
            other.gameObject.GetComponent<healthController>().restoreHealth(aid);
            Destroy(gameObject);
        }
        

       
    }

}
