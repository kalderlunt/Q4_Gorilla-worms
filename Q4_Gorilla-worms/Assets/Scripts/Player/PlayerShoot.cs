using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _launchForce = 1.5f;
    [SerializeField] private float _trajectoryTimeStep = 0.05f;
    [SerializeField] private int _trajectoryTimeStepCount = 15;

    private Vector2 _velocity;
    private Vector2 _startMousePos;
    private Vector2 _currentMousePos;

    private Rigidbody2D _rbProjectile;
    private bool _shooting;


    private PlayerMovement _playerMovement;


    [Range(-10, 10)] public float WindForce;
    [Range(-10, 10)] public float Gravity;


    private void Start()
    {
        _rbProjectile = transform.parent.GetComponent<Rigidbody2D>();
        _playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    public void ShootPerformed(InputAction.CallbackContext context)
    {
        _shooting = context.performed;

        if (context.started)
        {
            _startMousePos = Camera.main.ScreenToWorldPoint(_playerMovement.GetMousePos());
        }

        if (context.performed)
        {
            _currentMousePos = Camera.main.ScreenToWorldPoint(_playerMovement.GetMousePos());
            _velocity = (_startMousePos - _currentMousePos) * _launchForce;
            DrawTrajectory();
        }

        if (context.canceled)
        {
            FireProjectile();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] _positions = new Vector3[_trajectoryTimeStepCount];
        for (int i = 0; i < _trajectoryTimeStepCount; ++i)
        {
            float t = i * _trajectoryTimeStep;
            Vector3 pos = (Vector2)_spawnPoint.transform.position + _velocity * t * 0.5f * Physics2D.gravity * t * t;

            _positions[i] = pos;
        }

        _lineRenderer.SetPositions(_positions);
    }

    private void FireProjectile()
    {
        GameObject pr = Instantiate(_projectilePrefab, _spawnPoint.transform.position, Quaternion.identity);

        pr.GetComponent<Rigidbody2D>().velocity = _velocity;
    }

    /*private void FixedUpdate()
    {
        Vector3 v = Vector3.left + Vector3.up;
        Vector3 pCur = transform.position;

        for (int i = 0; i < 1000; ++i)
        {
            if (pCur.y < 0.0f)
                break;

            v += (WindForce * Vector3.right + Gravity * Vector3.down) * Time.fixedDeltaTime;
            Vector3 pNext = pCur + v * Time.fixedDeltaTime;

            Debug.DrawLine(pNext, pCur);

            pCur = pNext;
        }
    }*/
    /*_transform = bullet.transform;
        _transformTarget = _target.transform;

        var _targetPos.Vector3 = _transformTarget.position;
        float */
}
