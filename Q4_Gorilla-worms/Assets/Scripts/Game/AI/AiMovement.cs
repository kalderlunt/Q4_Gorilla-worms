using UnityEngine;

public class AiMovement : MonoBehaviour
{

    [SerializeField] private GameObject _feet;
    [SerializeField] private Rigidbody2D _rb;

    private DetectGround _detectGround;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _detectGround = _feet.GetComponent<DetectGround>();
    }
}
