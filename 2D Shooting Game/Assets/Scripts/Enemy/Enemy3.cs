using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    protected override IEnumerator Enemy_AI()
    {
        StartCoroutine(ShotBullet());

        while (true)
        {
            if (!PlayerDistanceCheck())
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            yield return new WaitForFixedUpdate();
        }        
    }

    private IEnumerator ShotBullet()
    {
        while (true)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 360; i += Random.Range(10, 30))
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i));
                }
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
