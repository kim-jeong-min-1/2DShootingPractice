using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Boss1 : BossEnemy
{
    private Vector3 centerPos = new Vector3(4.5f, 0f, 0f);
    private float patternTime = 0;
    private float currentTime = 0;
    private int prePattern;

    protected override void PatternStart()
    {
        StartCoroutine(BossAI_Update());
    }

    private IEnumerator BossAI_Update()
    {
        while (isBossAlive)
        {
            yield return StartCoroutine(Think());
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator Think()
    {
        Debug.Log("Think....");
        int randPattern;
        do
        {
            randPattern = 5;
            //randPattern = Random.Range(1, 4);
            yield return null;

        } while (randPattern == prePattern);

        prePattern = randPattern;
        currentTime = 0;

        switch (randPattern)
        {
            case 1: yield return StartCoroutine(Pattern1()); break;
            case 2: yield return StartCoroutine(Pattern2()); break;
            case 3: yield return StartCoroutine(Pattern3()); break;
            case 4: yield return StartCoroutine(Pattern4()); break;
            case 5: yield return StartCoroutine(Pattern5()); break;
        }
        yield break;
    }

    private IEnumerator Pattern1()
    {
        patternTime = 15f;
        float dir1 = 0; float dir2 = 36; float dir3 = 100;
        float addValue3 = 20;

        while (patternTime > currentTime)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(bossBullet1, transform.position, Quaternion.Euler(0, 0, dir1));
                dir1 += 72;
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(bossBullet2, transform.position, Quaternion.Euler(0, 0, dir2));
                dir2 -= 72;
            }

            var j = dir3;
            for (int i = 0; i < 5; i++)
            {
                Instantiate(bossBullet3, transform.position, Quaternion.Euler(0, 0, j));
                j += 5f;
            }
            if (dir3 + addValue3 > 240 || dir3 + addValue3 < 120) addValue3 *= -1;

            dir1 += 10; dir2 -= 10; dir3 += addValue3;

            currentTime += 0.25f;
            yield return new WaitForSeconds(0.25f);
        }
    }
    private IEnumerator Pattern2()
    {
        for (int i = 0; i < 10; i++)
        {
            var randPosY = Random.Range(-3.5f, 4f);
            var randPosX = Random.Range(2, 7);
            var movePos = new Vector3(randPosX, randPosY);

            var waitTime = 0.25f;
            var currentTime = 0f;

            while (transform.position != movePos)
            {
                transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);

                if (currentTime >= waitTime)
                {
                    CircleShot(50);
                    currentTime = 0;
                }
                currentTime += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
        }

        yield return StartCoroutine(MovePosition(transform.position, centerPos, 1f));
    }
    private IEnumerator Pattern3()
    {
        patternTime = 8f;
        while (true)
        {
            List<EnemyBullet> bullets1 = new List<EnemyBullet>();
            List<EnemyBullet> bullets2 = new List<EnemyBullet>();

            StartCoroutine(spawnBullet(bullets1));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(spawnBullet(bullets2));


            yield return new WaitForSeconds(3f);
            foreach (var bullet in bullets2)
            {
                if (bullet == null) continue;
                bullet.transform.GetComponent<SpriteRenderer>().color = Color.red;
                var dis = player.transform.position - bullet.transform.position;
                var z = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg;

                bullet.transform.rotation = Quaternion.Euler(0, 0, z);
                bullet.bulletSpeed = 4f;
                bullet.Shot();
            }
            foreach (var bullet in bullets1)
            {
                if (bullet == null) continue;
                bullet.bulletSpeed = 6.5f;
                bullet.Shot();
            }

            bullets1.Clear();
            bullets2.Clear();

            yield return new WaitForSeconds(patternTime - 1f);
        }
        IEnumerator spawnBullet(List<EnemyBullet> bullets = null)
        {
            int dir = 0;
            float speed = 0.5f;

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var temp = Instantiate(bossBullet1, transform.position, Quaternion.Euler(0, 0, dir));
                    temp.bulletSpeed = speed;
                    bullets?.Add(temp);

                    dir += 45;
                }
                dir += 5;
                speed += 0.2f;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    private IEnumerator Pattern4()
    {
        float radius = 0.2f;
        var wait = new WaitForSeconds(0.0000001f);

        Vector3 centerPos = new Vector3(0, 0, 0);
        Stack<EnemyBullet> bullets = new Stack<EnemyBullet>();

        for (int i = 0; i < 5; i++)
        {
            for (int j = i * 10; j < 360 + i * 10 * 2; j++)
            {
                if(j % 5 == 0)
                {
                    var Pos =
                        centerPos + new Vector3(Mathf.Cos(j * Mathf.Deg2Rad) * radius, Mathf.Sin(j * Mathf.Deg2Rad) * radius);
                    var bullet = Instantiate(bossBullet3, Pos, Quaternion.Euler(0, 0, j));
                    bullet.shotOnAwake = false;
                    bullets.Push(bullet);
                    yield return wait;
                }
                radius += 0.001f;
            }
        }
        foreach (var bullet in bullets)
        {
            bullet.Shot();
            yield return wait;
        }
    }
    private IEnumerator Pattern5()
    {
        yield break;
    }

    #region 공용 패턴 함수

    private IEnumerator MovePosition(Vector3 start, Vector3 end, float time)
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
    private void CircleShot(int count)
    {
        for (int i = 0; i <= 360; i += (360 / count))
        {
            Instantiate(bossBullet3, transform.position, Quaternion.Euler(0, 0, i));
        }
    }

    #endregion
}
