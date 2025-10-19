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
    private Camera _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerControls = new InputSystem_Actions();
        _camera = Camera.main;
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
        Physics.gravity = Quaternion.Euler(0f, 0f, angle) * Physics.gravity;
        _camera.transform.Rotate(0, 0, angle);
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
            _movement = transform.forward * movementAmount;
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
            _rigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            // throw new NotImplementedException();
        }
    }
}