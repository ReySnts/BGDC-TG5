using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed;
    public float RunSpeed;
    public float normalSpeed;
    public bool isRunning = false;



    public Rigidbody2D Rigibody;
    public Animator animator;

    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") ;
        movement.y = Input.GetAxisRaw("Vertical") ;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            Speed = RunSpeed;
            
        }
        else
        {
            isRunning = false;
            Speed = normalSpeed;
           
        }
        
        animator.SetFloat("Horizontal", Mathf.Abs(movement.x));
        animator.SetFloat("Vertical", Mathf.Abs(movement.y));
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }




    void FixedUpdate()
    {
        Rigibody.MovePosition(Rigibody.position + movement * Speed * Time.fixedDeltaTime);
        
    }
}
