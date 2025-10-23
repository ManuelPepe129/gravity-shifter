using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float jumpSpeed = 10.0f;
    [SerializeField] private bool _facingRight = false;

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
        _isJumping = false;
    }

    /// <summary>
    /// Change of gravity of the entire scene
    /// </summary>
    /// <param name="rotationValue"></param>
    private void RotateGravity(float rotationValue)
    {
        float angle = rotationValue > 0 ? 90f : -90f;
        //float angle = 90f * rotationValue;
        Physics.gravity = Quaternion.Euler(0f, 0f, angle) * Physics.gravity;
        StartCoroutine(PlayerCameraRotation(angle));
    }

    /// <summary>
    /// Camera and player rotation according to gravity
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private IEnumerator PlayerCameraRotation(float angle)
    {
        // The player can't move while rotating the camera
        _rigidbody.useGravity = false;
        _rigidbody.linearVelocity = Vector3.zero;
        _playerControls.Disable();

        _camera.GetComponent<Animator>().Play("Camera Gravity Shift");

        //yield return new WaitForSeconds(.2f);
        float duration = 1.5f;
        float alpha = 0.0f;
        Quaternion startPlayerRotation = _rigidbody.rotation;
        //Quaternion startCameraRotation = _camera.transform.rotation;
        while (alpha < 1.0f)
        {
            float currentAngle = Mathf.LerpAngle(0, angle, alpha);
            _rigidbody.rotation = Quaternion.Euler(0, 0, currentAngle) * startPlayerRotation;
            //_camera.transform.rotation = Quaternion.Euler(0, 0, currentAngle) * startCameraRotation;
            alpha += Time.deltaTime / duration;
            yield return null;
        }

        // Final rotations
        _rigidbody.rotation = Quaternion.Euler(0, 0, angle) * startPlayerRotation;
        //_camera.transform.rotation = Quaternion.Euler(0, 0, angle) * startCameraRotation;

        _rigidbody.useGravity = true;
    }

    /// <summary>
    /// Management of collision with floor: the player controls
    /// are not enabled until the player touch the ground
    /// N.b. Do not consider collision during the jump
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        var collisionDirection = other.contacts[0].normal;
        float dot = -Vector3.Dot(Physics.gravity.normalized, collisionDirection);

        // If the gravity and the movement vector are parallels
        // (player going down and touching the ground): enable
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
        //if (!_isJumping)
        //{
            float movementAmount = _playerControls.Player.Move.ReadValue<float>();

            // Change player direction
            if (!_facingRight && movementAmount > 0 || _facingRight && movementAmount < 0)
            {
                _rigidbody.rotation *= new Quaternion(0, movementAmount, 0, 0);
                _facingRight = !_facingRight;
            }

            // Assign movement direction
            _movement = transform.forward * Mathf.Abs(movementAmount);
        //}
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