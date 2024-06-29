
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] StatusBar hpBar;

    //[Header("current stats")]
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    [SerializeField] AudioSource powerUpClip;

    private void Awake()
    {
        currentHealth = playerData.MaxHealth;
        playerData.Invulnerable = false;
    }

    public void TakeDamage(float dmg)
    {
        AudioManager.instance.Play("Impact");
        if (currentHealth <= 0) Kill();
        currentDamage = dmg;
        if (playerData.Invulnerable)
            return;
        StartCoroutine(ApplyDamageOverTime());
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
