using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomUpgrade : MonoBehaviour
{
    [SerializeField]
    private Button reRoll;
    [SerializeField] Button[] defenceBarrier;
    [SerializeField] Button[] defenceTurret;
    [SerializeField] Button[] playerUpgrades;

    private void Start()
    {
       
    }
    private void OnEnable()
    {
        Shuffle(defenceBarrier);
        Shuffle(defenceTurret);
        Shuffle(playerUpgrades);
    }
    public void ReRoll()
    {
        Shuffle(defenceTurret);
        Shuffle(defenceBarrier);
        Shuffle(playerUpgrades);
    }
    public void Shuffle(Button[] list)
    {
        var random = Random.Range(0, list.Length);

        while (list[random].interactable == false) 
        {
             random = Random.Range(0, list.Length);
        }
        list[random].gameObject.SetActive(true);
        for (int i = 0; i < list.Length; i++)
        {
            if (i != random)
            {
                list[i].gameObject.SetActive(false);

            }
        }
       
    }

}
