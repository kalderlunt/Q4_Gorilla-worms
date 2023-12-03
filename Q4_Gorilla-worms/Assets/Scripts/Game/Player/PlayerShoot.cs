using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class LineHelp : MonoBehaviour
{
    [SerializeField] Transform _projectilesPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] LineRenderer _lineRenderer;

    [SerializeField] float _launchForce = 1.5f;
    [SerializeField] float _trajectoryTimestep = 0.05f;
    [SerializeField] int _trajectoryStepCount = 15;

    Vector2 _velocity, _startMousePos, _currentMousePos;

    public Animator animator;
    private AnimatorStateInfo currentStateInfo;

    private void Start()
    {
        animator.Play("Idle");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            _currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _velocity = (_startMousePos - _currentMousePos) * _launchForce;

            DrawTrajectory();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FireProjectile();
            ClearTrajectory();
        }
    }

    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[_trajectoryStepCount];
        for (int i = 0; i < _trajectoryStepCount; i++)
        {
            float t = i * _trajectoryTimestep;
            Vector3 pos = (Vector2)_spawnPoint.position + _velocity * t + 0.5f * Physics2D.gravity * t * t;
            positions[i] = pos;
        }

        _lineRenderer.positionCount = _trajectoryStepCount;
        _lineRenderer.SetPositions(positions);
    }

    private void FireProjectile()
    {
        Transform projectile = Instantiate(_projectilesPrefab, _spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = _velocity;
        projectile.transform.parent = this.transform; 
        
        animator.Play("Attack");

        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        StartCoroutine(ResetAnimation("Attack", "Idle", currentStateInfo.length));
    }

    IEnumerator ResetAnimation(string conditionName, string executionName, float time)
    {
        yield return new WaitForSeconds(time); // Délai avant la réinitialisation de l'animation

        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName(conditionName))
        {
            animator.Play(executionName);
        }
    }

    void ClearTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }
}