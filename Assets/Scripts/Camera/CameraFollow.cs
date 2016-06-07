using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    //Third Person Camera
    public Transform lookAt;
    public Transform camTransform;
    private Camera cam;
    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private const float Y_ANGLE_MIN = 5.0f;
    private const float Y_ANGLE_MAX = 30.0f;

    //Target for camera to follow
    public Transform target;

    //Little bit of lag to create smoothing effect allow the player to see
    public float smoothing = 5f;

    //Private variable, store the offset of the camera from the player
    Vector3 offset;

    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        //offset = transform.position - target.position;
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

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        //Dont want to make a y Axis to have a full Loop
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MIN);
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * direction;
        camTransform.LookAt(lookAt.position);
    }
}