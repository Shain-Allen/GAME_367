using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private Transform _cam;
    
    [SerializeField] private float _movementSpeed = 5f;
    private Vector3 _movementInput;

    [SerializeField] private float _shootDistance = 5f;
    
    // gravity variables
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _groundedGravity = -0.05f;
    
    // Jumping Variables
    public float InitialJumpVelocity { get; private set; }
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    public bool IsJumping = false;
    
    
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

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
        HandleRaycast();
        HandleGravity();
        HandleJump();
    }
    
    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            _movementInput.y += _groundedGravity;
        }
        else
        {
            _movementInput.y += _gravity * Time.deltaTime;
        }
    }
    
    private void HandleJump()
    {
        if (_characterController.isGrounded)
        {
            IsJumping = false;
            _movementInput.y = 0;
        }
        
        if (Input.GetAxis("Jump") == 0 || IsJumping) return;
        
        IsJumping = true;
        _movementInput.y += InitialJumpVelocity;
    }

    private void HandleMovement()
    {
        Vector3 rawMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        rawMovementInput = transform.TransformDirection(rawMovementInput) * _movementSpeed;
        
        _movementInput.x = rawMovementInput.x;
        _movementInput.z = rawMovementInput.z;
        
        _characterController.Move(_movementInput *  Time.deltaTime);
    }
    
    private void HandleCamera()
    {
        //Get mouse input
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        
        //rotate Player body for left right looking
        transform.Rotate(Vector3.up ,mouseInput.x);
        
        //rotate camera for up/down looking
        _cam.transform.Rotate(Vector3.right, mouseInput.y);
    }
    
    private void HandleRaycast()
    {
        if (!Input.GetButton("Fire1")) return;
        
        if (!Physics.Raycast(_cam.transform.position, transform.forward, out RaycastHit hit, _shootDistance)) return;
        
        if (!hit.transform.TryGetComponent(out IShootable hitObject)) return;
        
        hitObject.Shot();
    }
}