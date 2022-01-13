using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
        //grab camera and assign it to the cam field/variable
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //trigger an OnTriggerEnter2D method if "player" collides with this asset
    private void OnTriggerEnter2D(Collider2D other)
    {
        //there is a trigger on the asset with this script assigned
        if (other.CompareTag("Player"))
        {
            Debug.Log("player hit trigger");
            // adjustinig the cam min/max position when trigger is collided with
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            // allows you to move the player- I will move him a few spaces
            other.transform.position += playerChange;
            // if the asset has needText assigned then it will start the Coroutine
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    //runs in parallel to other methods and has a specified wait time for coroutines
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        //placeName will come from the string left on the Room Transfer object
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
