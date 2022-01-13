using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //grabs animator component and assigns it to anim
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {
        //the boolean for smash in animator will be set to true when this method is called
        anim.SetBool("smash", true);
        //starts the coroutin for breakCo- aka breakCoroutine
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        //assigns the time for this coroutine
        yield return new WaitForSeconds(.3f);
        //turns off the gameObject was being interacted with
        this.gameObject.SetActive(false);
    }
}
