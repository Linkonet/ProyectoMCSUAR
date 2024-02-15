using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] public float fmoveSpeed;
    [SerializeField] public float fJumpForce;

    float horizontalInput;
    float verticalInput;

    Rigidbody _rb;

    public Transform orientation;

    private Vector3 direction;
    private Vector3 moveDir;
    //public Transform cam;


    public float fsmoothrotationSpeed;
    public float fturnsmoothtime = 0.1f;



    public SlowTime time;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        MyInput();

        //UpdateRotation();

    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (time.timeslowed)
        {
            _rb.velocity = _rb.velocity / time.slowMotionFactor;
            _rb.angularVelocity = _rb.angularVelocity / time.slowMotionFactor;
        }


    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    }

    /*private void UpdateRotation()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float targetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref fsmoothrotationSpeed, fturnsmoothtime);
            transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }*/

    private void MovePlayer()
    {
        Vector3 velocity = Vector3.zero;

        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (horizontalInput != 0f || verticalInput !=0) 
        {  
            velocity = moveDir * fmoveSpeed;

        }

        velocity.y = _rb.velocity.y;

        _rb.velocity = velocity;
    }


}