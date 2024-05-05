using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedboost_prova : MonoBehaviour
{
    [SerializeField] private Transform _cameraT;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 3f;

    [SerializeField] private float _gravity = -20f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpHeight = 3f;


    private CharacterController _characterController;
    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;

    private Vector3 _velocity;
    private bool _isGrounded;

    //RUN AND JUMP
    private float requiredDuration = 0.2f; // Durata richiesta in secondi
    private bool isKeyBeingPressed = false; // Flag per controllare se il tasto è premuto
    private float pressedTime = 0f; // Tempo trascorso mentre il tasto è premuto

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        //Ground Check
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        GatherInput();
        NewOrientation();
        Movement();

        //JUMPING AND RUN WITH THE SPACE   
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _speed = 8f;
            isKeyBeingPressed = true;
            pressedTime = 0f; // resetta il tempo ad ogni premuta
        }
        else if(Input.GetKeyUp(KeyCode.Space) && _isGrounded) 
        {
            isKeyBeingPressed = false;
            _speed = 5f;
            if (pressedTime < requiredDuration )
            {  
                Jump();
            } 
        }
        if(isKeyBeingPressed) 
            {
                pressedTime += Time.deltaTime;
            }

        //FALLING
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }


    private void GatherInput()
    {
        //GET Input
        _inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);
    }
    private void NewOrientation()
    {
        //Compute direction According to Camera Orientation
        _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
        _targetDirection.y = 0f;

        //Rotate Object
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    private void Movement() 
    {
        //Move object along forward
        _characterController.Move(transform.forward * _inputSpeed * _speed * Time.deltaTime);
    }
    private void Run()
    {
        _speed = 10f;
    } 
    private void Jump()
    {
        _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
    }
}
