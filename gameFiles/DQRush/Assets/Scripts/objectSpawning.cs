using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawning : MonoBehaviour
{
    //this script can be used to spawn enemy or health aid for player
    private float timer;
    private float lastSpawn;
    private float lim;
   
    [SerializeField] GameObject item;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset = new Vector3(15, 0, 15);
    // Update is called once per frame

    private void Start()
    {
        lim = 15f;
        lastSpawn = timer;
    }


    void Spawn()
    {
        Vector3 pos = target.transform.position;
        Instantiate(item, pos + offset, transform.rotation);
    }

    void Update()
    {
        timer = (int)(target.GetComponent<playerMovement>().lifeCounter)/60;

        if (timer == 60f && timer <= 120f) lim = 12f;

        if (timer >= 120f && timer <= 180f) lim = 10f;
        if (timer >= 180f && timer <= 240f) lim = 8f;
        if (timer >= 240f) lim = 4f;

        if (timer - lastSpawn >= lim)
        {
            Spawn();
            lastSpawn = timer;
        }




    }

}

