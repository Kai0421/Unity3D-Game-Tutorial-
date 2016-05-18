using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    //Target for camera to follow
    public Transform target;

    //Little bit of lag to create smoothing effect allow the player to see
    public float smoothing = 5f;

    //Private variable, store the offset of the camera from the player
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    //Move the camera using Physics component
    void FixedUpdate()
    {
        //A place for the camera + the offset
        Vector3 targetCamPos = target.position + offset;

        //Lerp Smoothly move from one position to another
        //move from current position to target position with the smoothing level that set above( * delta time so that it wont do it 50 times per seconds)
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
