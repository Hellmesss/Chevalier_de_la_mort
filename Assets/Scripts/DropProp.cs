using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropProp : MonoBehaviour
{
    public Rigidbody2D RB;
    public BoxCollider2D BC;
    public Transform impact;
    public float circle;
    bool isGrounded;
    public LayerMask coll;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RB.isKinematic = false;
        }
        if (BC.IsTouching(collision) && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(15);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(impact.position, circle);
    }
    private void Impact()
    {
        isGrounded = Physics2D.OverlapCircle(impact.position, circle, coll);
    }
    private void FixedUpdate()
    {
        Impact();
    }
    private void delete()
    {
        if (isGrounded)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        delete();
    }
}
