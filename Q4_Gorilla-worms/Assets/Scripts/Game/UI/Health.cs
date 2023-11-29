using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static Health instance; 
    public float healthPlayer = 75.0f;
    public float healthAI = 75.0f;
    public float maxHealth = 100.0f;

    public Image healthBarValueImage;
    public TextMeshProUGUI healthText;

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
        healthPlayer = maxHealth;
        healthAI = maxHealth;
    }

    void Update()
    {
        healthBarValueImage.fillAmount = healthPlayer / maxHealth;
        
        healthText.text = "♥ Player : " + healthPlayer.ToString();

        if (healthPlayer >= maxHealth)
        {
            healthPlayer = maxHealth;
        }
        
        if (healthAI >= maxHealth)
        {
            healthAI = maxHealth;
        }

        if (healthPlayer <= 0 || healthAI <= 0)
        {
            SceneManager.LoadScene("Scenes/Menu");
        }
    }

    public void DamageHit(int damageAmount)
    {
        healthPlayer -= damageAmount;
    }

    public void HealDeal(int healAmount)
    {
        healthPlayer += healAmount;
    }
}
