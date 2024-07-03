
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
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
    public void TakeDamage(float dmg)
    {
        if (ShieldActive)
        {
            SafeGuard();
        }
        else
        {
            if (currentHealth <= 0) Kill();
            AudioManager.instance.Play("Impact");
            currentDamage = dmg;
            if (playerData.Invulnerable)
                return;
            StartCoroutine(ApplyDamageOverTime());
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
    public void UnregisterDamage(float dmg)
    {
        AudioManager.instance.Stop("Impact");
        StopCoroutine(ApplyDamageOverTime());
        currentDamage -= dmg;
    }
    void Kill()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    IEnumerator ApplyDamageOverTime()
    {
        while (true)
        {
            currentHealth -= currentDamage;
            hpBar.SetState(currentHealth, playerData.MaxHealth);
            Debug.Log(currentHealth);
            if (currentHealth <= 0) Kill();
            playerData.Invulnerable = true;
            yield return new WaitForSeconds(playerData.InvulnerableTime);
        }
    

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
}
