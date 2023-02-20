using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BossEnemy : MonoBehaviour 
{
    [SerializeField] protected float bossHp;
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected GameObject player;
    [SerializeField] private Canvas bossCanvas;
    [SerializeField] private Slider bossHpBar;

    [SerializeField] protected EnemyBullet bossBullet1;
    [SerializeField] protected EnemyBullet bossBullet2;
    [SerializeField] protected EnemyBullet bossBullet3;

    [HideInInspector] public bool isBossAlive = true;
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();

    public float HP
    {
        get { return bossHp; }
        set
        {
            if (bossHp > 0)
            {
                bossHp = value;
                bossHpBar.value = bossHp;
            }
            else
            {
                isBossAlive = false;
                Die();
            }
        }
    }
    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
        bossCanvas = transform.GetChild(0).GetComponent<Canvas>();
        bossCanvas.worldCamera = Camera.main;
        bossHpBar.maxValue = bossHp;

        PatternStart();
    }
    protected abstract void PatternStart();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            HP -= collision.GetComponent<PlayerBullet>().bulletDamage;
            Hit();
            Destroy(collision.gameObject);
        }
    }

}
