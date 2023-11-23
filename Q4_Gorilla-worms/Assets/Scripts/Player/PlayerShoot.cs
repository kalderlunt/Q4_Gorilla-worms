using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    [Range(-10, 10)] public float WindForce;
    [Range(-10, 10)] public float Gravity;


     
    void Shoot()
    {
        Instantiate(_projectile, this.transform);
    }


    private void FixedUpdate()
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
    }
    /*_transform = bullet.transform;
        _transformTarget = _target.transform;

        var _targetPos.Vector3 = _transformTarget.position;
        float */
}
