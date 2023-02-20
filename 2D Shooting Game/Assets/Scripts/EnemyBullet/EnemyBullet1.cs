using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet1 : EnemyBullet
{
    public override void Shot()
    {
        rb.velocity = transform.right * bulletSpeed;
    }
}
