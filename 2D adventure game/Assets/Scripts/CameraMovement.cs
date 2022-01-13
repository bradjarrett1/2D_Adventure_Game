using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // LateUpdate comes last after Update and FixedUpdate (set to the physics system tick- about every 30 secs)
    // We use LateUpdate bc update might make the camera update faster than the player
    void LateUpdate()
    {
        //if the position of the camera and it's target (the assigned player) are not equal then this willa activate
        if (transform.position != target.position)
        {
            //not sure how transform.position.z works- I believe it holds the camera's current z value
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            //Mathf.clamp allows me to restrict values- this is used to restrict the camera to the play zone
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            //linear interpilation- this moves the camera slower between current location and new player target location
            //using smoothing
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
