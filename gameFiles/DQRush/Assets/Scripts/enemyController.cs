using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private float movSpeed;
    public float distanceToPlayer;
    Transform player;
    Animator animator;
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
        player = GameObject.Find("Player").transform;
       
    }

    private void Move()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer >= 1.2f)
        {
            animator.SetBool("attacking", false);
            animator.SetBool("walking", true);
            movSpeed = 3f;
            //chase player 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotSpeed * Time.deltaTime);
            transform.position = transform.position + transform.forward * movSpeed * Time.deltaTime;
        }
        else
        {
            movSpeed = 0.5f;
            animator.SetBool("attacking", true);

        }

    }


    // Update is called once per frame
    void Update()
    {
        isAlive = GetComponent<healthController>().isAlive;
        if (isAlive) Move();
        else
        {
            animator.Play("dead");
            Destroy(gameObject, 10);
        }

        
        
    }
}
