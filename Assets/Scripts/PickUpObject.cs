using UnityEngine;

public class PickUpObject : MonoBehaviour
{
       private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddMonsters(1);
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerController>().HealLifePoint(30);
        }
            
    }
}
