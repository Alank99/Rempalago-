using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    public Vector2 sensitivity;
    public Vector2 movement;

    public float jumpForce;

    public bool grounded;

    public float maxSpeedX;

    public float airtimeControlReduction;

    private void Start() {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        grounded = true;
    }

    private void Update() {
        var cacheSens = grounded ? sensitivity : sensitivity * airtimeControlReduction;
        playerRB.AddForce(new Vector2(movement.x * cacheSens.x * Time.deltaTime, 
                                      movement.y * cacheSens.y * Time.deltaTime));

        if (playerRB.velocity.x >  maxSpeedX){
            playerRB.velocity = new Vector2(maxSpeedX, playerRB.velocity.y);
        }

        if (playerRB.velocity.x <  -maxSpeedX){
            playerRB.velocity = new Vector2(-maxSpeedX, playerRB.velocity.y);
        }
    }

    public void OnMove(InputValue value){
        movement = value.Get<Vector2>();
    }

    public void OnJump(){
        if (grounded)
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
    }
}
