using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyData
{
    public EnemyData(int enemyNum, int spawnPoint, float spawnDelay)
    {
        this.enemyNum = enemyNum;
        this.spawnPoint = spawnPoint;
        this.spwanDelay = spawnDelay;
    }
    public int enemyNum;
    public int spawnPoint;
    public float spwanDelay;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<Enemy> enemys = new List<Enemy>();
    [SerializeField] private BossEnemy boss;

    private Queue<EnemyData> enemyDatas = new Queue<EnemyData>();
    private void Start() => ReadWave();

    private void ReadWave()
    {
        TextAsset textFile = Resources.Load("Wave") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (true)
        {
            var line = stringReader.ReadLine();

            if (line != null)
            {
                var data = line.Split(',');

                var num = int.Parse(data[0]);
                var point = int.Parse(data[1]);
                var delay = float.Parse(data[2]);

                EnemyData enemy = new EnemyData(num, point, delay);
                enemyDatas.Enqueue(enemy);
            }
            else
            {
                stringReader.Close();
                break;
            }
        }
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int count = 0;
        while (enemyDatas.Count > 0)
        {
            var enemyData = enemyDatas.Dequeue();
            var enemy = enemyData.enemyNum switch
            {
                1 => enemys[0],
                2 => enemys[1],  
                3 => enemys[2],
                _ => null
            };

            Instantiate(enemy, spawnPoints[enemyData.spawnPoint - 1].position, Quaternion.identity);
            count++;

            yield return new WaitForSeconds(enemyData.spwanDelay);
        }

        yield return new WaitForSeconds(12f);
        Instantiate(boss, spawnPoints[2].position, Quaternion.identity);
    }
}
