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


    //[Range(-10, 10)] public float WindForce;
    //[Range(-10, 10)] public float Gravity;

    
    private void Start()
    {
        _rbProjectile = transform.parent.GetComponent<Rigidbody2D>();
        _playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if (_shooting)
        {
            _currentMousePos = Camera.main.ScreenToWorldPoint(_playerMovement.GetMousePos());
            _velocity = (_startMousePos - _currentMousePos) * _launchForce;
            DrawTrajectory();
        }
    }

    public void ShootPerformed(InputAction.CallbackContext context)
    {   
        if (context.started)
        {
            _trajectoryTimeStepCount = 15;
            _startMousePos = Camera.main.ScreenToWorldPoint(_playerMovement.GetMousePos());
        }

        if (context.performed)
        {
            _shooting = true;
        }

        if (context.canceled)
        {
            FireProjectile();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[_trajectoryTimeStepCount];
        for (int i = 0; i < _trajectoryTimeStepCount; ++i)
        {
            float t = i * _trajectoryTimeStep;
            Vector3 pos = (Vector2)_spawnPoint.transform.position + _velocity * t * 0.5f * Physics2D.gravity * t * t;

            positions[i] = pos;
        }
        _lineRenderer.positionCount = _trajectoryTimeStepCount;
        _lineRenderer.SetPositions(positions);
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _spawnPoint.transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = _velocity;
        projectile.transform.parent = this.transform;

        ClearTrajectory();
    }
    private void ClearTrajectory()
    {
        _trajectoryTimeStepCount = 0;

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
