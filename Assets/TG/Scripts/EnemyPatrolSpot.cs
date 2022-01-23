using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


// 만약 에너미가 구역들중 하나에 가까워진다면 다른 구역으로 이동하게 하고싶다
// - 필요 요소 : 에너미의 위치, 지정 구역, 반응 거리




public class EnemyPatrolSpot : MonoBehaviour
{
    // 반응 거리
    public float reactRange;

    // 지정된 구역
    [SerializeField] Transform[] reactSpot;


    // 모든 enemy에게 영향을 주고싶다.
    // - 필요 요소 : 배열, 배열의 정보들을 갖고오기
    
    public GameObject[] enemyObj;
    int enemyArrayLength = 0;




    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        enemyArrayLength = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().enemyNum;
    }

    // Update is called once per frame
    void Update()
    {
        // index = 랜덤

        int index = Random.Range(0, reactSpot.Length);

        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        //for (int i = 0; i < enemyObj.Length; i++  )
        //{
        //    enemyObj[i].GetComponent<EnemyMove>().agent.destination = reactSpot[index].position;
        //}

    }

    public void ChangeDestNation()
    {
        currentTime += Time.deltaTime;

        // 갈곳 랜덤으로 만들어주기
        int index = Random.Range(0, reactSpot.Length);
       

        // enemy 태그 가진 obj들을 배열에 넣기
        enemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        
        //for (int i = 0; i < enemyObj.Length; i++)
        //{
        //    // 배열에 있는것들 
        //    enemyObj[i].GetComponent<EnemyMove>().agent.destination = reactSpot[index].position;
        //    index = Random.Range(0, reactSpot.Length);

        //}

        for (int i = 0; i < enemyObj.Length; i++)
        {
            index = Random.Range(0, reactSpot.Length);
            // 배열에 있는것들 

            enemyObj[i].GetComponent<EnemyMove>().PlayAgent(reactSpot[index].position);
        }


    }

}
