using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Require a CharacterController component on the object.
// If no CharacterController component has been attached, it will
// be automatically added.
[RequireComponent(typeof(CharacterController))]

// Place object in a given component bin.
[AddComponentMenu("Control Script/FPS Input")]
    
public class FPSInput : MonoBehaviour
{
    [SerializeField] GameBoss gameBoss;
    
    // [Header("Jump Parameters")]
    // public bool canJump = true;
    // private bool shouldJump => Input.GetKeyDown(jumpKey) && _controller.isGrounded;
    // [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    // [SerializeField] private float jumpForce = 2.0f;

    [SerializeField] private Rigidbody rb;
    [Header("Movement Attributes")]
    [Range(1.0f, 10.0f)]
    [SerializeField] float _speed = 6.0f;
    // [SerializeField] float _gravity = -9.8f;

    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    private void Update()
    {
        // Moving the transform via the Translate method won't engage
        // collision detection.

        // transform.Translate(
        //    Input.GetAxis("Horizontal") * _speed * Time.deltaTime,
        //    0,
        //    Input.GetAxis("Vertical") * _speed * Time.deltaTime);

        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        
        Vector3 movement = new(deltaX, 0, deltaZ);
        
        // Clamp diagonal movement
        movement = Vector3.ClampMagnitude(movement, _speed);
        
        // Apply gravity after X and Z have been clamped
            
        // if (canJump)
        // {
        //     if (shouldJump)
        //     {
        //         movement.y = 0;
        //         movement.y = Mathf.Sqrt(jumpForce * -2.0f * _gravity);
        //     }
        // }
        // movement.y += _gravity;
        
        movement *= Time.deltaTime;
        
        // Convert movement vector to rotation settings of player
        movement = transform.TransformDirection(movement);
        
        Vector3 finalMove = (movement * _speed) + (movement.y * Vector3.up);

        if (gameBoss.canPlay)
        {
            _controller.Move(movement);
        }
    }
    
}
