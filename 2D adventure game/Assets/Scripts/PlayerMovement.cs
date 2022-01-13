using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//creates states for the player to but in for different categories before interactions
//enum is like a boolean but with more states than booleans
public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //puts the player into the walk state (for interaction or to do walking down for hitbox reasons- covered)
        currentState = PlayerState.walk;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //settings the initial coordinates for the animator so the hitbox down will trigger on attack instead of all hitboxes
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        //initializes change as zero
        change = Vector3.zero;
        //captures the input from buttons for horizontal and vertical
        //using GetAxisRaw because we want to go straight from 0 to 1 or 0 to -1 immediately like an analog system for old 2D games
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
        { 
        UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        //sets animator boolean to attacking- this must trigger the attack animation depending on being true
        //and based on coordinates of the player for exact animation and hitbox seleection
        animator.SetBool("attacking", true);
        //puts player into attack state for whatever reason?
        currentState = PlayerState.attack;
        yield return null; // waits one frame- used for the time frame of the attack animation?
        //turns attacks off
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        // checks to see if the input (change) is anything besides zero- if so it will act
           if (change != Vector3.zero)
        {
            //uses MoveCharacter method to normalize the vector input then move the rigidbody of player
            MoveCharacter();
            //sets the x and y coordinates on change for the movements animations in  animator
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            //sets the movement bool to true?
            animator.SetBool("Moving", true);
        } else 
        {
            //if movement or input for change is no longer true then movement in animator will be turned off
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize(); // moves the vector to a full 1.0 instead of a decimal btwn 0 and 1
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime // change is a vector3, this is multiplied
                                                                 // by a speed then Time.deltaTime to align with frame rates
        );
    }
}
