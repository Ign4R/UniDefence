
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public PlayerScriptableObject playerData;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] StatusBar hpBar;

    //[Header("current stats")]
    [Range(2,5)]
    [SerializeField] private float moveSpeed;
    float currentHealth;
    float currentDamage;

    [SerializeField] AudioSource powerUpClip;
 
    private int shieldCountHits;
    private int shieldMaxHits;
    private float rechargeTime;

    public float CurrentSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool ShieldActive { get; set ; }
  
    private void Awake()
    {

       
    }
    private void Start()
    {
        currentHealth = playerData.MaxHealth;
        shieldMaxHits = playerData.ShieldMaxHits;
        playerData.Invulnerable = false;
    }
    public void SetDamage(float dmg)
    {
        currentDamage += dmg;
    }

    public void TakeDamage()
    {
        AudioManager.instance.Play("DamagePJ");
        currentHealth -= currentDamage;
        hpBar.SetState(currentHealth, playerData.MaxHealth);
        if (currentHealth <= 0)
        {
            Kill();
        }
        if (ShieldActive)
        {
            SafeGuard();
        }
    }
    public void SafeGuard()
    {
        shieldCountHits++;
        if (shieldCountHits >= shieldMaxHits)
        {
            ShieldActive = false;
            StartCoroutine(RestoreShield());
        }
    }

    public void ShieldActivate(bool value) ///interfaz
    {
        ShieldActive = value;
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
        AudioManager.instance.Play("GameOver",true);
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    IEnumerator RestoreShield()
    {
        yield return new WaitForSeconds(rechargeTime);
        ShieldActive = true;
        yield break;
    }
    IEnumerator BlinkRountine() //blink parpadeos
    {
        int t = 10;
        while (t > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(t * playerData.BlinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(t * playerData.BlinkRate);
            t--;
        }
    }

    public void ResetDamage()
    {
        throw new System.NotImplementedException();
    }
}
