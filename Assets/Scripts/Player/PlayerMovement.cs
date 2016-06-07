using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player moving speed
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f; 

    void Awake()
    {
        //The Invisible quad on the UI floor in Level 1 scene
        floorMask = LayerMask.GetMask("Floor");

        //Load Components from the UI
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    //Function thats within unity that will call on every physics updates
    //Unity Runs on number of updates, rendering runs on normal updates
    void FixedUpdate()
    {
        //Keys on keyboard
        float h = Input.GetAxisRaw("Horizontal"); // Interpolated
        float v = Input.GetAxisRaw("Vertical"); // Interpolated
        float j = Input.GetAxisRaw("Jump"); 
       
        Move(h, v, j);
        Turning();
        Walking(h, v);
    }

    void Move(float h, float v, float j)
    {
        //0f cus there is not need for vertical component/movement which is on the y axis
        movement.Set(h, j, v);

        //If it move in x,z axis this is ok but id it move diagonally you get the advantage due to 
        //Pythagorious theorem, this diagonal longer this means that it moves faster then the initial unit of 1
        //using this to normalize it 
        //Delta time is the time between each update call
        movement = movement.normalized * speed * Time.deltaTime;

        //Move the player position
        //Using this current position + the position that it has moved to
        playerRigidbody.MovePosition(transform.position + movement);

    }


    //This is based on the mouse input
    void Turning()
    {
        //Invisible ray that points from the camera, the point underneath the mouse,
        //Camera has to find if the point underneath the mouse is a floor point.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Get the information back
        RaycastHit floorHit;

        //cast the ray invisible line
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            //Quaternion is a way of storing the rotation 
            // Cannot use vector3 to store the rotation so Quaternion is used
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //apply the new rotation to variable newRotation
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Walking(float h, float v)
    {
        //this is jsut saying that, did the player press the horizontal or the vertical axis
        //if the player pressed either of those, then the player is walking else not.
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
