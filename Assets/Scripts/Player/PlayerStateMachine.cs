using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : MonoBehaviour  
{
    [Header("Player references")]
    private InputActions _inputActions;
    private CharacterController _characterController;
    private PlayerGun _playerGun;
    
    [Header("Player movement Variables")]
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    [SerializeField] private float _fallSpeedMultiplyer = 2f;
    private float InitialJumpVelocity { get; set; }
    private bool IsJumping = false;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;
    
    [Header("Player Camera variables")]
    [SerializeField] private Transform _cam;
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;
    private float _rotY = 0;
    
    // gravity variables
    private float _gravity = -9.8f;
    private float _groundedGravity = -0.05f;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerGun = GetComponentInChildren<PlayerGun>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
        SetupJumpVariable();
    }
    
    // setup jump variables to allow for jump height and time to apex.
    private void SetupJumpVariable()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        InitialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    private void Update()
    {
        HandleMovement();
        HandleCamera();
        HandleGravity();
        HandleJump();

        if(!_playerGun) return;
        _playerGun.Shoot();
        _playerGun.CheckForInteracables();
    }
    
    private void HandleGravity()
    {
        float previousYVelocity = _currentMovement.y;
        
        bool isFalling = !_characterController.isGrounded && _currentMovement.y <= 0f;
        
        if (_characterController.isGrounded)
        {
            _currentMovement.y = _groundedGravity;
            _appliedMovement.y = _groundedGravity;
        }
        else if (isFalling)
        {
            _currentMovement.y += _gravity * _fallSpeedMultiplyer * Time.deltaTime;
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * 0.5f;
        }
        else
        {
            _currentMovement.y += _gravity * Time.deltaTime;
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
        }
    }
    
    private void HandleJump()
    {
        if (_characterController.isGrounded)
        {
            IsJumping = false;
            _currentMovement.y = 0;
        }
        
        if (Input.GetAxis("Jump") == 0 || IsJumping) return;
        
        IsJumping = true;
        _currentMovement.y += InitialJumpVelocity;
        _appliedMovement.y += InitialJumpVelocity;
    }

    private void HandleMovement()
    {
        Vector3 rawMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        rawMovementInput = transform.TransformDirection(rawMovementInput) * _movementSpeed;
        
        _currentMovement.x = rawMovementInput.x;
        _currentMovement.z = rawMovementInput.z;

        _appliedMovement.x = _currentMovement.x;
        _appliedMovement.z = _currentMovement.z;
        
        _characterController.Move(_appliedMovement *  Time.deltaTime);
    }
    
    private void HandleCamera()
    {
        //Get mouse input
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

        _rotY += mouseInput.y;
        _rotY = Mathf.Clamp(_rotY, minVerticalAngle, maxVerticalAngle);
        
        //rotate Player body for left right looking
        transform.Rotate(Vector3.up ,mouseInput.x);
        
        //rotate camera for up/down looking
        _cam.transform.localRotation = Quaternion.Euler(_rotY, 0f, 0f);
    }
    
    
}