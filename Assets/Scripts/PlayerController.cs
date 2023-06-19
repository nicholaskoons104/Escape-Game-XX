using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    //Object Movement
    public float speed = 10f;

    //jump modifiers
    public float gravityModifier;
    public float jumpHeight = 5f;

    public bool isOnGround = true;
    public bool jumpCheck;

    public int jumpCount;



    //sprint modifier
    public float sprint;




    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        JumpMechanics();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
        }
    }


        public void JumpMechanics()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            Debug.Log("Jumping");
            isOnGround = false;
            jumpCount += 1;

        }
    }

    public void PlayerMovement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.right * speed * horizontalInput);
        playerRb.AddForce(Vector3.forward * speed * forwardInput);
    }
}
