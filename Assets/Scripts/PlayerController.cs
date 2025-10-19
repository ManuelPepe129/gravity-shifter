using System;
using System.Collections;
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
        StartCoroutine(PlayerCameraRotation(angle));
    }

    private IEnumerator PlayerCameraRotation(float angle)
    {
        _rigidbody.useGravity = false;
        _rigidbody.linearVelocity = Vector3.zero;
        _playerControls.Disable();
        float duration = 2.0f;
        float alpha = 0.0f;
        Quaternion startPlayerRotation = _rigidbody.rotation;
        Quaternion startCameraRotation = _camera.transform.rotation;
        while (alpha < 1.0f)
        {
            float currentAngle = Mathf.LerpAngle(0, angle, alpha);
            _rigidbody.rotation = Quaternion.Euler(0, 0, currentAngle) * startPlayerRotation;
            _camera.transform.rotation = Quaternion.Euler(0, 0, currentAngle) * startCameraRotation;
            alpha += Time.deltaTime / duration;
            yield return null;
        }

        _rigidbody.rotation = Quaternion.Euler(0, 0, angle) * startPlayerRotation;
        _camera.transform.rotation = Quaternion.Euler(0, 0, angle) * startCameraRotation;


        _rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        var collisionDirection = other.impulse.normalized;
        float dot = -Vector3.Dot(Physics.gravity.normalized, collisionDirection);
        if (dot > 0.5f)
        {
            _playerControls.Enable();
            _isJumping = false;
        }
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
            _isJumping = true;
        }
    }
}