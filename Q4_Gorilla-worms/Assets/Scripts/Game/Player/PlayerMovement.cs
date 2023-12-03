using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private GameObject _feet;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _speed;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _jumpForce;

    private Vector2 _moveVector;
    private Vector2 _direction;

    private Vector2 _mousePos;

    private DetectGround _detectGround;

    public Animator animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _detectGround = _feet.GetComponent<DetectGround>();
    }

    private void FixedUpdate()
    {
        if (_moveVector != Vector2.zero)
        {
            _direction += _moveVector;
        }

        _direction *= _deceleration;
        _rb.velocity += new Vector2(_speed * Time.deltaTime * _direction.x - _rb.velocity.x, 0);
    }

    private void Update()
    {
        if (_moveVector == Vector2.zero)
            animator.Play("Idle");
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (_detectGround.OnGround() && context.phase == InputActionPhase.Performed)
        {
            _rb.velocity += Vector2.up * _jumpForce;
        }
    }


    public void onMovementPerformed(InputAction.CallbackContext context)
    {
        animator.Play("Walk");
        _moveVector = context.ReadValue<Vector2>();
    }

    public void onMovementCancel(InputAction.CallbackContext context)
    {
        _moveVector = Vector2.zero;
    }

    public void MousePos(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
        if (Camera.main != null)
            _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
    }

    public Vector2 GetMousePos()
    {
        return _mousePos;
    }
}
