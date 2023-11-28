using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private int _min_height;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.position.y < _min_height)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 15.0f);
        }
    }
}