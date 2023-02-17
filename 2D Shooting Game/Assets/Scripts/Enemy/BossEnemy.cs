using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BossEnemy : MonoBehaviour 
{
    [SerializeField] protected float bossHp;
    [SerializeField] protected float moveSpeed;

    [SerializeField] private Slider bossHpBar;
    [SerializeField] protected GameObject player;
    [SerializeField] protected EnemyBullet bossBullet1;
    [SerializeField] protected EnemyBullet bossBullet2;
    [SerializeField] protected EnemyBullet bossBullet3;

    [HideInInspector] public bool isBossAlive = true;

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
            else isBossAlive = false;
        }
    }

    private void Start()
    {
        bossHpBar.maxValue = bossHp;
        PatternStart();
    }

    protected abstract void PatternStart();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            HP -= collision.GetComponent<PlayerBullet>().bulletDamage;
            Destroy(collision.gameObject);
        }
    }

}
