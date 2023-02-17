using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerBullet : PlayerBullet
{
    public override void Shot()
    {
        rb.velocity = transform.right * bulletSpeed;
    }
}
