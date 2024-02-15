using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform teleportationLookAt;

    public GameObject thirdPersonCam;
    public GameObject TeleportCam;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Teleportation
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Teleportation);
        if (Input.GetKeyUp(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);

        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        if (currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticarInput = Input.GetAxis("Vertical");
            Vector3 inputdir = orientation.forward * verticarInput + orientation.right * horizontalInput;

            if (inputdir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputdir.normalized, Time.deltaTime * rotationSpeed);

        }

        else if (currentStyle == CameraStyle.Teleportation)
        {
            Vector3 dirTeleportation = teleportationLookAt.position - new Vector3(transform.position.x, teleportationLookAt.position.y, transform.position.z);
            orientation.forward = dirTeleportation.normalized;

            playerObj.forward = dirTeleportation.normalized;
        }
    }

    private void SwitchCameraStyle (CameraStyle newStyle)
    {
        TeleportCam.SetActive(false);
        thirdPersonCam.SetActive(true);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Teleportation) TeleportCam.SetActive(true);

        currentStyle = newStyle;
    }
}
