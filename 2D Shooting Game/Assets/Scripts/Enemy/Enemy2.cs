using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private float waitTime = 3f;
    private float currentTime = 0f;
    private bool isDown => transform.position.y > 0;
    private Vector3 dir;

    protected override IEnumerator Enemy_AI()
    {
        if (isDown) dir = Vector3.down;
        else dir = Vector3.up;

        while (true)
        {
            if (!PlayerDistanceCheck() && waitTime > currentTime)
            {
                transform.Translate(dir * moveSpeed * Time.deltaTime);
                currentTime += Time.deltaTime;
            }
            else
            {
                var dis = player.transform.position - transform.position;
                var z = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, z), moveSpeed * Time.deltaTime);
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            }
            yield return new WaitForFixedUpdate();
        }

    }
}
