
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public PlayerScriptableObject playerData;
    [SerializeField] private GameObject _shield;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] StatusBar hpBar;

    //[Header("current stats")]
    [Range(2, 5)]
    [SerializeField] private float moveSpeed;
    float currentHealth;
    float currentDamage;

    [SerializeField] AudioSource powerUpClip;

    private int shieldCountHits;
    [SerializeField] private int shieldMaxHits;
    [SerializeField] private float rechargeTime;

    public float CurrentSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool ShieldActive { get; private set; }

    private void Awake()
    {


    }
    private void Start()
    {
        currentHealth = playerData.MaxHealth;
        playerData.Invulnerable = false;
    }
    public void SetDamage(float dmg)
    {
        currentDamage += dmg;
    }

    public void TakeDamage()
    {
        if (ShieldActive)
        {
            SafeTemporally();
        }
        else
        {
            AudioManager.instance.Play("DamagePJ");
            currentHealth -= currentDamage;
            hpBar.SetState(currentHealth, playerData.MaxHealth);
            if (currentHealth <= 0)
            {
                Kill();
            }

        }

    }
    public void SafeTemporally()
    {

        if (shieldCountHits >= shieldMaxHits)
        {
            shieldCountHits = 0;
            _shield.gameObject.SetActive(false);
            ShieldActive = false;
            StartCoroutine(RestoreShield());
        }
        else
        {
            shieldCountHits++;
        }

    }

    public void ShieldActivate(bool value) ///interfaz
    {
        if (!ShieldActive)
        {
            ShieldActive = value;
        }
        else
        {
            UpgradeShield();
        }

    }

    public void UpgradeSpeed()
    {
        if (CurrentSpeed < 5)
        {
            CurrentSpeed++;
        }

    }
    public void UpgradeShield()
    {
        shieldMaxHits++;
        rechargeTime--;
    }

    void Kill()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    IEnumerator RestoreShield()
    {
        yield return new WaitForSeconds(rechargeTime);
        ShieldActive = true;
        _shield.gameObject.SetActive(true);
        yield break;
    }
   

    public void ResetDamage()
    {
        throw new System.NotImplementedException();
    }
}
