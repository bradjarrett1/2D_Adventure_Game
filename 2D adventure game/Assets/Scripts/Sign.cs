using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    /*
     * What do we want to happen?
     * When the player is inside the area we want the dialog box to pop up
     * If player presses space bar or leaves the area then make dialog box disappears
     * We need access to UI, we need reference to dialog box, text, and string for text to change
     */

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks for two conditions- Input.GetKeyDown(KeyCode.Space) && playerInRange being true
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            //checks to see if dialogbox is already active
            if (dialogBox.activeInHierarchy)
            {
                //if diablogbox was being statements were true and dialogbox is already active then it is turned off
                dialogBox.SetActive(false);
            } else
            {
                //if dialogbox was not true/on then is set to true and dialogText is assigned from the string in inspector
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    //checks for player collision with the objects assigned box collider with is trigger on
    private void OnTriggerEnter2D(Collider2D other)
    {
        //checks to see if the object colliding with it is tagged with "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            //assigns true to playerInRange
            playerInRange = true;
        } 
    }

    //checks for player exiting the trigger for the assigned object above
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left range");
            //assigns false to playerInRange
            playerInRange = false;
            //turns off dialog box as playerInRange becomes false/player moves away
            dialogBox.SetActive(false);
        }
    }
}
