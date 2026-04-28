using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]


public class Mover : MonoBehaviour
{

    public float speed;
    public float runmultiplier;
    public float gravity = -9.81f;
    public float jumpHeight;
    public float RotationSpeed;

    private CharacterController character;
    private Vector3 velocity;
    private bool isRunning = false;
    private bool isGrounded;
    private PlayerInputActions inputaction;
    private Vector2 moveInput;
    private float rotateInput;

    private void Awake(){
        character = GetComponent<CharacterController>();
        inputaction = new PlayerInputActions();
        inputaction.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputaction.Player.Move.canceled += ctx => moveInput =Vector2.zero;
        inputaction.Player.Run.performed += ctx => isRunning = true;
        inputaction.Player.Run.canceled += ctx => isRunning = false;
        inputaction.Player.Jump.performed += ctx => jump();
        inputaction.Player.Rotate.performed += ctx => rotateInput = ctx.ReadValue<float>();
        inputaction.Player.Rotate.canceled += ctx => rotateInput = 0;
        




    }

    void OnEnable(){
        inputaction.Enable();


    }
    void OnDisable(){
        inputaction.Disable();


    }



    void jump(){

        if(isGrounded){
            velocity.y= Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = character.isGrounded;
        if(isGrounded && velocity.y<0){
            velocity.y = -2;

        }

        Vector3 move = new Vector3(moveInput.x,0,moveInput.y);
        move=transform.TransformDirection(move);
        float currentSpeed = isRunning ? speed*runmultiplier : speed;
        character.Move(move*currentSpeed*Time.deltaTime);
        float rotation=rotateInput*RotationSpeed*Time.deltaTime;
        transform.Rotate(0,rotation,0);
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity*Time.deltaTime);
    }
}
