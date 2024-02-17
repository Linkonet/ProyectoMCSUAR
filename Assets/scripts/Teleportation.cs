using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    //Camera Variables
    public Camera mainCamera;
    public Transform cam;

    //Player Variables
     public Rigidbody rb;
    public Movement move;

    //Distance and Coordinates Variables
    public float maxTeleportDistance = 20f;
    public Vector3 selectedCoordinate = Vector3.zero; // Store the selected coordinates]

    //Variables to check Teleport ability
    public bool isSelecting = false;
    private float cooldownTeleport;

    //Variables to Store Raycast
    private Ray ray;
    private RaycastHit hit;

    //Variable for Coroutine
    private bool teleport;
    private bool finished;


    /** FIRST FUNCTION TO BE CALLED WHEN PRESSED PLAY**/
    void Start()
    {
        //Set cooldown to 10
        cooldownTeleport = 10f;

        //Set teleport max Distance to 20
        maxTeleportDistance = 20f;

        finished = false;

        rb = move.GetComponent<Rigidbody>();
    }


    /** FUNCTION THAT IS CALLED EVERYFRAME**/
    void Update()
    {
        //Checking if key is pressed down and cooldown is above or equal 10
        if (Input.GetKeyDown(KeyCode.Alpha1) /*&& cooldownTeleport >= 10f*/ ) // Start selection on left mouse click
        {
            //giving variable a true value
            isSelecting = true;
            //making cooldown drop to 0
            cooldownTeleport = 0f;
            finished = false;
        }

        //Checking if key is being unpressed
        if (Input.GetKeyUp(KeyCode.Alpha1)) // End selection on mouse release
        {
            //giving variable a false value
            isSelecting = false;
            finished = true;

        }

        if (finished && cooldownTeleport <= 10f)
        {
            cooldownTeleport += Time.deltaTime;
        }

        

        //Calls StartTeleport Function
        StartTeleport(maxTeleportDistance);

    }

    /** BEGGINING OF TELEPORT MECHANICS**/
    private void StartTeleport (float maxDistance)
    {
        //Checking in variable is true
        if (isSelecting)
        {
            //Giving variable the values of the Input of the mouse
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                selectedCoordinate = hit.point;
                teleport = true;
                // Do something with the selected coordinate, e.g., visualize it
                Debug.Log("Selected Coordinate: " + selectedCoordinate);
            }
            else
            {
                Debug.Log("point too far");
                teleport = false;
            }
        }

        if (teleport && Input.GetKeyUp(KeyCode.Alpha1)) 
        {
            teleportation();
        }

        Debug.Log("teleport " + teleport);

    }

    void teleportation()
    {
       
        // Directly use selectedCoordinate for teleportation
        rb.transform.position = new Vector3(selectedCoordinate.x, selectedCoordinate.y, selectedCoordinate.z);

        // Reset variables
        isSelecting = false;
        selectedCoordinate = Vector3.zero;
        teleport = false;
    }

    

   
}
