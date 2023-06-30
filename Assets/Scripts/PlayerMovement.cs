using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMulitplier;

    public bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool isOnGround;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody playerRb;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;


    // Start is called before the first frame update
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;

        readyToJump = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        isOnGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);


        SpeedControl();
        MyInput();

        if (isOnGround)
        {
            playerRb.drag = groundDrag;
        }
        else
        {
            playerRb.drag = 0;
        }

       

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        //jump check

        if (Input.GetKey(jumpKey) && readyToJump && isOnGround)
        {
            Debug.Log("IsJumping");

            readyToJump = false;

            Jump();

            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isOnGround)
        {
            playerRb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        }else if (!isOnGround)
        {
            playerRb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMulitplier, ForceMode.Force);

        }
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        //limit velocity if needed

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerRb.velocity = new Vector3(limitedVel.x, playerRb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset velocity y
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void JumpReset()
    {
        readyToJump = true;
    }

}
