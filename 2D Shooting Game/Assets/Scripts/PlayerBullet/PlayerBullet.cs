using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed;
    public float bulletDamage;

    protected Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Shot();
    }

    public abstract void Shot();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyZone"))
        {
            Destroy(this.gameObject);
        }
    }
}
