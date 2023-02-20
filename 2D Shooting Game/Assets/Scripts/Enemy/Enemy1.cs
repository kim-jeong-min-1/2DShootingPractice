using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    private float revolveRadius = 8.5f;
    protected override IEnumerator Enemy_AI()
    {
        while (!PlayerDistanceCheck())
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(ShotBullet());
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(Movement());
    }
    private IEnumerator Movement()
    {
        while (true)
        {
            var randDir = Random.Range(-40, 41);
            var pos = player.transform.position +
                new Vector3(Mathf.Cos(randDir * Mathf.Deg2Rad) * revolveRadius, Mathf.Sin(randDir * Mathf.Deg2Rad) * revolveRadius);
            yield return StartCoroutine(MovePosition(transform.position, pos, 1.5f));
            yield return new WaitForSeconds(1.5f);
        }       
    }
    private IEnumerator ShotBullet()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                var dir = player.transform.position - transform.position;
                var z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, z));
                yield return new WaitForSeconds(0.07f);
            }
            yield return new WaitForSeconds(2.5f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, revolveRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
