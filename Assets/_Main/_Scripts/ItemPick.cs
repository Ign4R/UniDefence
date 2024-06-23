using UnityEngine;
public class ItemPick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("Item");
            GameManager.instance.CheckProgressWave();
            gameObject.SetActive(false);
            Destroy(gameObject,2);
        }
    }

}
