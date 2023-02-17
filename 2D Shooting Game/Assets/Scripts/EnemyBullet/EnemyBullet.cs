using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    public bool shotOnAwake = true;

    protected Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(shotOnAwake) Shot();
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

