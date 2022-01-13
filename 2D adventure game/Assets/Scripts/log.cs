using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//having this inheritance like from Enemy is a principle characteristic of object oriented programming
public class log : Enemy
{
    //by having log inherit from Enemy instead of monobehavior it will gather all variables, methods, and info
    //from Enemy plus the Monobevhaiors that Enemy posses

    /*****************The purpose of this script is to make the log move when the player is close
                        and to stop when it's close to the player. We do this by grabbing the Rigidbody2D
                        and assigning the object with the tag "Player" to target. After this we will be running the
                        update method that run the check distance. The check distance method is checking for
                        target and the object w/ this script attached (log) being within the chaseRadius and
                        being more than the attackRadius. If either of these is true then the movement/methood stops.
                        We are moving the log through moveTowards that is being assigned to a variable/field named temp.
                        We will use the .MovePosition with temp as the input to move the RigidBoody2D on the log.
                        It is important that we move the Rigidbody bc this is on dynamic body type. It not matter as
                        much better with the log alternating between kinematic and dynamic, bc kinematic is moved by script.
                        Now, we must focus on moving the log through the physics systems for less issues.*************
     */

    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        //going to find the game object with tag "Player" then grab the transform properties- scale, rotation, position
        //this will be assigned to the target variable
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is being changed to FixedUpdate in order to align this "physics" method with the physics call every
    // 30 secs. Normal Updates happen with the frame rate (usually higher than 30 times per sec)
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        //Vector3.Distance will use the variables of where to go to vs. where you are currently
        //we are comparing this to chaseRadius that is input in the inspector
        //if it equal to or less than chaseRadius then we will initiate this if statement
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState!= EnemyState.stagger)
            {
                //we will assign the transform position to this object with the script that is calculated below
                //we use Vector3.MoveTowards to move something from the object position to our target position,
                //we will use moveSpeed * Time.deltaTime to set the amount of speed per frame
                //this last variable is called max distance delta (amount of time since the last frame)
                Vector3 temp = transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

}
