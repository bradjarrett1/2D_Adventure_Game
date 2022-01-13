using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Simplifed: This script will knock the enemy back using calculations and an AddForce. The effect will turn off
 * after a short amount of time by using the coroutine knockCo, this sets the veocity too zero, stopping the enemy
 * (log in this case) from flying off screen.
 */
public class Knockback : MonoBehaviour 
{
    public float thrust; //the amount of force that the character knocks the enemy back
    public float knockTime;

    public Rigidbody2D enemy;

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        //this script is assigned to hitboxes that are only on when attack state/animation is happening
        //when the hitboxes are on if there is a collision with an object that is tagged breakable then the
        //if statement below will process
        if (other.CompareTag("breakable"))
        {
            //as currently written it appears that the pot will be follow the smash method everytime if breakable is assigned
            //to an object that is triggered by hitboxes- the pot will break/smash following the method and coroutine and animation
            other.GetComponent<Pot>().Smash();
        }
        //check to see if the collision is with an object tagged enemy
        if (other.gameObject.CompareTag("enemy"))
        {
            enemy = other.GetComponent<Rigidbody2D>();
            if(enemy != null) //I believe this means if there is an enemy assigned? also checking for death of enemy
            {
                enemy.GetComponent<Enemy>().currentState = Enemy.EnemyState.stagger;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust; // normalizing sets it to absolute zero or one
                enemy.AddForce(difference, ForceMode2D.Impulse); //this moves an object
                StartCoroutine(KnockCo(enemy)); //this runs a parallel task with a timer
            }
        }
    }

    //this is going to create a time frame until the enemy's physics to reset so that it cannot keep moving by being hit
    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        //this is checking for the disapperance or death of the enemy
        if(enemy != null)
        {
            // going to set the amount of time that physics is changed on the enemy
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero; // after the knockTime the velocity(movement?) will be set to
            enemy.GetComponent<Enemy>().currentState = Enemy.EnemyState.idle;
            //*********The code in the tutorial uses = EnemyState.idle instead of Enemy.EnemyState.idle but idk why.
            //********* let's see if this works
        }
    }
}
