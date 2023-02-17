using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Bullet1 : EnemyBullet
{
    public override void Shot()
    {
        rb.velocity = transform.right * bulletSpeed;
    }
}
