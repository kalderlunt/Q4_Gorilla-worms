using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private int _minHeightLimit = -1;
    [SerializeField] private int _damageAmount = 20;
    [SerializeField] private float _timeToDestroy = 10.0f;
    [SerializeField] private int _numberMaxCollisionWithMap = 3;
    private int _numberCollisionWithMap = 0;

    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _explosionRadiusPrefab;
    [SerializeField] private AudioClip _hitSound;

    private GameManager _gameManager;
    private Rigidbody2D _rb;
    
    private Vector2 _wind;
    private float _timer = 0;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _explosionPrefab.GetComponent<Rigidbody2D>();
        _explosionRadiusPrefab.GetComponent<Rigidbody2D>();

        float r = UnityEngine.Random.Range(-7, 7);
        _wind = new Vector2(1, 0) * r;
    }

    void Update()
    {
        if (transform.position.y < _minHeightLimit)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (_timer >= _timeToDestroy)
            {
                Destroy(this.gameObject); 

                GameObject newexplosion = null;
                newexplosion = Instantiate(_explosionPrefab);
                newexplosion.transform.position = transform.position;

                _timer = 0;
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        _rb.velocity += _wind * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_explosionPrefab != null)
        {
            if (collision.gameObject.tag == "Map")
            {
                _numberCollisionWithMap++;
                if (_numberCollisionWithMap >= _numberMaxCollisionWithMap)
                {
                    NewExplosion();
                }
                return;
            }

            if (collision.gameObject.tag == "Player")
            {
                Health.DamageHitPlayer(_damageAmount);
                NewExplosion();
                return;
            }

            if (collision.gameObject.tag == "AI")
            {
                Health.DamageHitAI(_damageAmount);
                NewExplosion();
                return;
            }
            NewExplosion();
        }
        
        Destroy(this.gameObject);
    }

    private void NewExplosion()
    {
        SoundManager.PlaySound(_hitSound);

        Destroy(this.gameObject);
        GameObject newExplosion = null;
        newExplosion = Instantiate(_explosionPrefab);
        newExplosion.transform.position = transform.position;
        _explosionPrefab = null;
        
        GameObject newExplosionRadius = null;
        newExplosionRadius = Instantiate(_explosionRadiusPrefab);
        newExplosionRadius.transform.position = transform.position;
        _explosionRadiusPrefab = null;
    }
}