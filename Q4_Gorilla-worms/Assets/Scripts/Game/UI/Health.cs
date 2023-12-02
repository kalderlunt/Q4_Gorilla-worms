using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Health instance; 

    public static float _healthPlayer = 0f;
    public static float _healthAi = 0f;

    public float _maxHealth = 100.0f;

    public Image _healthBarPlayerValueImage;
    public Image _healthBarAiValueImage;
    
    public TextMeshProUGUI _healthTextPlayer;
    public TextMeshProUGUI _healthTextAi;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }

    void Start()
    {
        _healthPlayer = _maxHealth;
        _healthAi = _maxHealth;
    }

    void Update()
    {
        _healthBarPlayerValueImage.fillAmount = Mathf.Lerp(_healthBarPlayerValueImage.fillAmount, _healthPlayer / _maxHealth, 0.1f);
        _healthBarAiValueImage.fillAmount = Mathf.Lerp(_healthBarAiValueImage.fillAmount, _healthAi / _maxHealth, 0.1f);
        
        _healthTextPlayer.text = "♥ Player : " + _healthPlayer.ToString();
        _healthTextAi.text = "♥ AI : " + _healthAi.ToString();

        if (_healthPlayer >= _maxHealth)
        {
            _healthPlayer = _maxHealth;
        }
        else if (_healthAi >= _maxHealth)
        {
            _healthAi = _maxHealth;
        }

        
        if (_healthPlayer >= 0 && _healthAi <= 0) // Player win
        {
            SceneManager.LoadScene("Scenes/FinVictoire");
        }
        if (_healthPlayer <= 0 && _healthAi >= 0) // AI win
        {
            SceneManager.LoadScene("Scenes/FinDefaite");
        }
    }

    public static void DamageHitPlayer(int damageAmount)
    {
        _healthPlayer -= damageAmount;
    }
    public static void DamageHitAI(int damageAmount)
    {
        _healthAi -= damageAmount;
    }

    public static void HealDeal(float targetHealth, int healAmount)
    {
        targetHealth += healAmount;
    }
}
