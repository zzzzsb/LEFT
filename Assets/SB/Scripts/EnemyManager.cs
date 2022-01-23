using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 정해진 장소에서 랜덤으로 스팟을 선택하여 일정시간에 한번씩 만들어 배치하고 싶다.
// 필요 속성 : 정해진 장소, enemyFactory, 생성시간, 경과시간
public class EnemyManager : MonoBehaviour
{
    // 필요 속성 : 정해진 장소, enemyFactory, 생성시간, 경과시간
    // 정해진 에너미생성장소
    public Transform[] spawnPoints;
    // 에너미 공장
    public GameObject enemyFactory;
    // 생성시간
    public float createTime = 5f;

    // 생성하려고 하는 에너미 갯수(정한 에너미갯수)
    public int enemyNum = 3;

    int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        // 생성된 에너미 갯수가 정해진 갯수보다 작으면 계속 생성한다.
        while (enemyCount < enemyNum)
        {
            // 일정시간이 지났으니까
            yield return new WaitForSeconds(createTime);
            // 에너미를 만들어야 함
            GameObject enemy = Instantiate(enemyFactory);
            enemyCount++;
            // 에너미를 정해진 장소에 배치하기
            int index = Random.Range(0, spawnPoints.Length-1);
            enemy.transform.position = spawnPoints[index].position;
        }
        // 에너미가 정해진 갯수만큼 다 생성되었다면
        // 생성을 멈춘다.
        yield break;
    }
}
