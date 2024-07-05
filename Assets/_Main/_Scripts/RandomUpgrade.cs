using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomUpgrade : MonoBehaviour
{
    [SerializeField]
    private Button reRoll;
  [SerializeField] Button[] defenceUpgrades;
    [SerializeField] Button[] playerUpgrades;

    private void Start()
    {
        Shuffle(defenceUpgrades);
        Shuffle(playerUpgrades);
    }
    public void ReRoll()
    {

        Shuffle(defenceUpgrades);
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
