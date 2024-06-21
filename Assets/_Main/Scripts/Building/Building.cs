using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingScriptableObject buildingData;

    //[Header("current stats")]
    float currentHealth;

    private void Awake()
    {
        currentHealth = buildingData.MaxHealth;
    }

    public void TakeDamage(float dmg)
    {
        if (buildingData.Invulnerable)
            return;

        currentHealth -= dmg;
        //Debug.Log(currentHealth);
        buildingData.Invulnerable = true;
        StartCoroutine(MakeVulnerableAgain());
        if (currentHealth <= 0) Kill();
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    IEnumerator MakeVulnerableAgain()
    {
        //StartCoroutine(BlinkRountine());
        yield return new WaitForSeconds(buildingData.InvulnerableTime);
        buildingData.Invulnerable = false;
    }

    //IEnumerator BlinkRountine() //blink parpadeos
    //{
    //    int t = 10;
    //    while (t > 0)
    //    {
    //        //spriteRenderer.enabled = false;
    //        //yield return new WaitForSeconds(t * blinkRate);
    //        //spriteRenderer.enabled = true;
    //        //yield return new WaitForSeconds(t * blinkRate);
    //        t--;
    //    }
    //}
}
