using UnityEngine;
public class ItemPick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.CheckProgressWave();
            gameObject.SetActive(false);
        }
    }

}
