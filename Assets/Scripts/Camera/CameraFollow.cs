using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV; //Smooth the rotation movement.
    public float sensitivity = 5.0f, smoothing = 2.0f; //Mouse Sensitivity.

    GameObject character;

    //Third Person Camera
    /*public Transform lookAt;
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
 
    //Move the camera using Physics component
    void FixedUpdate()
    {
        //A place for the camera + the offset
        Vector3 targetCamPos = target.position + offset;

        //Lerp Smoothly move from one position to another
        //move from current position to target position with the smoothing level that set above( * delta time so that it wont do it 50 times per seconds)
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
    
    private void LateUpdate()
    {
       Vector3 direction = new Vector3(0, 0, -distance);
       Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
       camTransform.position = lookAt.position + rotation * direction;
       camTransform.LookAt(lookAt.position);
    } */

    //Code Changes to adapt first person shooter       
    void Start()
    {
        //The character is the object parent. which is the character is the camera parent
        character = this.transform.parent.gameObject;

        //camTransform = transform;
        //cam = Camera.main;
        //offset = transform.position - target.position;
    }

    //Update per frame using normal update method.
    private void Update()
    {
        Turning();
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        //currentX += Input.GetAxis("Mouse X");
        //currentY += Input.GetAxis("Mouse Y");

        //Dont want to make a y Axis to have a full Loop
        //currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MIN);
    }

    void Turning()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        //Using lerp to make the movement transition from 1 position to another smoothly.
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouseLook += smoothV;

        //MouseLook have a minus to perform the inver
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}