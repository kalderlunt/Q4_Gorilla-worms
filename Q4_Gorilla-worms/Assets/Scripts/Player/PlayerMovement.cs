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
    
    //private CustomInput _input = null;


    private DetectGround _detectGround;




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


    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (_detectGround.OnGround() && context.phase == InputActionPhase.Performed)
        {
            print("wth");
            _rb.velocity += Vector2.up * _jumpForce;
        }
            print(_rb.velocity);
    }


    public void onMovementPerformed(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }
    
    public void onMovementCancel(InputAction.CallbackContext context)
    {
        _moveVector = Vector2.zero;
    }



    public Vector2 getMousePos()
    {
        return Vector2.zero;
        //return _mousePos;
    }
}
