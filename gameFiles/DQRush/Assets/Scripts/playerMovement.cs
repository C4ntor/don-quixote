using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;
    private float horMovement;
    private float verMovement;
    public float playerSpeed;
    private int comboHits;
    private float timer;
    private bool playerIsAlive;
    private Text counter;
    public int lifeCounter;
    [SerializeField] private float timerSet;
    AudioSource[] asources;
    Animator animator;



    void Start()
    {
        animator = GetComponent<Animator>();
        asources = GetComponents<AudioSource>();
        counter = GameObject.Find("counter").GetComponent<Text>();
        timer = 0;
        lifeCounter = 0;
        comboHits = 0;
    }

    void Move() 
    {
        Vector3 playerInput = new Vector3(horMovement, 0, verMovement);
        //normalize the vector s.t. diagonal movement is fixed (not moving faster in diagonal)
        playerInput.Normalize();
        if (playerInput != Vector3.zero)
        {
            playerSpeed = walkSpeed;
            //we update the animation that it must play
            animator.SetBool("walking", true);
            if (Input.GetKey("left shift"))
            {
                animator.SetBool("running", true);
                playerSpeed = runSpeed;
            }
            else
            {
                animator.SetBool("running", false);
                playerSpeed = walkSpeed;
            }

            //it moves the player in the world, considering the player speed and adjusting it by the frame rate (s.t. it is frame independent)
            transform.Translate(playerInput * playerSpeed * Time.deltaTime, Space.World);
            Quaternion rotationTo = Quaternion.LookRotation(playerInput, Vector3.up);
            //applies a smooth rotation from current player rotation angle to the one denoted by the new direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTo, rotationSpeed * Time.deltaTime);
        }
        else
        {
           
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
            playerSpeed = 0.5f;
            Attack();

        }
        

    }

    void Attack() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            asources[0].Play();
            switch (comboHits)
            {
                case 0:
                    FirstHit();
                    break;
                case 1:
                    SecondHit();
                    break;
                case 2:
                    FinalHit();
                    break;
            }
        }
       

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            comboHits = 0;
            animator.SetBool("attack3", false);
            animator.SetBool("attack2", false);
            animator.SetBool("attack1", false);
        }


    }

    void FirstHit()
    {
        animator.SetBool("attack1", true);
        comboHits++;
        timer = timerSet;
    }

    void SecondHit()
    {
        animator.SetBool("attack2", true);
        comboHits++;
        timer = timerSet;
    }

    void FinalHit()
    {
        animator.SetBool("attack3", true);
        comboHits = 0;
        timer = 0;
    }


    IEnumerator Die()
    {
        animator.Play("dead");
        yield return new WaitForSeconds(5);
        GetComponent<mainMenu>().loadMenu();
        lifeCounter = 0;
    }

        void Update()
        {
            lifeCounter ++;
            playerIsAlive = gameObject.GetComponent<healthController>().isAlive;
            //get pressed keys (wasd, or arrows) 
            horMovement = Input.GetAxisRaw("Horizontal");
            verMovement = Input.GetAxisRaw("Vertical");
            if (playerIsAlive) {
                Move();
                counter.text = ((int) lifeCounter/60).ToString() + " S";
            } else StartCoroutine(Die());
    }


   

}
