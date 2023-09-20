using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //gravity: 5
    private Rigidbody2D playerRigidBody;
    //8
    public float movementSpeed;
    private float inputHorizontal;
    //12
    public float jumpForce;
    private int numJumps;
    private int maxNumJumps;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        //I can only get this compenent using the following line of code
        //becuase the rigidbody2d is attached to the player and this script
        //is also attached to the player.
        playerRigidBody = GetComponent<Rigidbody2D>();
        numJumps = 1;
        maxNumJumps = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");

        //Basic movement not ideal
        //moves an object but will ignore collisions
        //transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        movePlayerLateral();
        jump();
        
    }

    private void flipPlayer()
    {
        //If moving to the right
        if(inputHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
        else if(inputHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void movePlayerLateral()
    {
        //Value returned will be 0, 1, or -1 depending on what button is pressed
        //no button press: 0
        //right arrow key or d: 1
        //left arrow key or a: -1
        //"Horizontal" is defined in the input section of project settings
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);
        flipPlayer();
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);

            numJumps++;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {


        //Debug.Log(collision.ToString());
        if (collision.gameObject.CompareTag("OB"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            numJumps = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DoubleJump"))
        {
            //Give player double jump
            //delete object from screen
            maxNumJumps = 2;
            Destroy(collision.gameObject);
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        canJump = false;
    //    }

    //}

}
