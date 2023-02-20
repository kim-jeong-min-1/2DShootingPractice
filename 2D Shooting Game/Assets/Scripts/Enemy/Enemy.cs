using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float enemyHp;
    [SerializeField] protected float radius;
    [SerializeField] protected EnemyBullet bullet;
    protected GameObject player;

    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    public float HP
    {
        get { return enemyHp; }
        set
        {
            enemyHp = value;
            if(enemyHp <= 0) Die();
        }
    }

    private void Start() => EnemySetUP();

    protected void EnemySetUP()
    {
        player = GameObject.Find("Player").gameObject;
        StartCoroutine(Enemy_AI());
    }
    protected abstract IEnumerator Enemy_AI();

    private void Hit()
    {
        StartCoroutine(HitEffect());
        IEnumerator HitEffect()
        {
            var color = spriteRenderer.color;

            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = color;
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    protected IEnumerator MovePosition(Vector3 start, Vector3 end, float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.position = Vector3.Lerp(start, end, percent);
            yield return null;
        }
    }
    protected private bool PlayerDistanceCheck()
    {
        var dis = player.transform.position - transform.position;

        if (Mathf.Pow(radius, 2) >= Mathf.Pow(dis.x, 2) + Mathf.Pow(dis.y, 2))
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            HP -= collision.GetComponent<PlayerBullet>().bulletDamage;
            Hit();
            Destroy(collision.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
