using System.Collections;
using System.Collections.Generic;
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
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        //I can only get this compenent using the following line of code
        //becuase the rigidbody2d is attached to the player and this script
        //is also attached to the player.
        playerRigidBody = GetComponent<Rigidbody2D>();
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

    private void movePlayerLateral()
    {
        //Value returned will be 0, 1, or -1 depending on what button is pressed
        //no button press: 0
        //right arrow key or d: 1
        //left arrow key or a: -1
        //"Horizontal" is defined in the input section of project settings
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
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
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
    }

}
