
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
        if (playerData.Invulnerable)
            return;

        currentHealth -= dmg;
        hpBar.SetState(currentHealth, playerData.MaxHealth);
        Debug.Log(currentHealth);
        playerData.Invulnerable = true;
        StartCoroutine(MakeVulnerableAgain());
        if (currentHealth <= 0) Kill();
    }

    void Kill()
    {
        gameObject.SetActive(false);
        GameManager.instance.GameOver();
    }

    IEnumerator MakeVulnerableAgain()
    {
        //StartCoroutine(BlinkRountine());
        yield return new WaitForSeconds(playerData.InvulnerableTime);
        playerData.Invulnerable = false;
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
