using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackController : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    AudioSource impactSource;
   

    private void Start()
    {
        impactSource = GetComponent<AudioSource>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag=="enemySword" && other.gameObject.tag=="Player" && other.gameObject.GetComponent<healthController>().isAlive)
        {
            impactSource.Play();
           
            other.gameObject.GetComponent<healthController>().takeDamage(damage);
        }

        if (gameObject.tag == "playerSword" && other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<healthController>().isAlive)
        {
            other.gameObject.GetComponent<healthController>().takeDamage(damage);
        }



    }

    

}
