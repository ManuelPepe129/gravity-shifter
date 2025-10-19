using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float jumpSpeed = 10.0f;

    private bool _isJumping = false;
    private Vector3 _movement = Vector3.zero;

    private Rigidbody _rigidbody;
    private InputSystem_Actions _playerControls;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new InputSystem_Actions();
    }

    private void Start()
    {
        // Callbacks setup
        _playerControls.Player.Jump.performed += _ => Jump();
        _playerControls.Player.Gravity.performed += context => RotateGravity(context.ReadValue<float>());
    }

    private void RotateGravity(float rotationValue)
    {
        float angle = 90f * rotationValue;
        // TODO: Update gravity
        Debug.Log("Rotate Gravity by " + angle);
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if (!_isJumping)
        {
            float movementAmount = _playerControls.Player.Move.ReadValue<float>();
            _movement = Vector3.right * movementAmount;
        }
    }


    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Jump()
    {
        if (!_isJumping)
        {
            throw new NotImplementedException();
        }
    }
    
    
}