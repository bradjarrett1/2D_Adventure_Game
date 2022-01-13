using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }
}
