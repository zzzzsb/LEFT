using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

// enemy가 가까이 온다면 램프가 깜빡거리게 하고싶다.
// - 필요 요소 
// 1. enemy
// 2. 감지범위
// 3. 램프의 빛(child object로 있는 light)
// 4. 깜빡거릴 범위
public class Lamp : MonoBehaviour
{
    public AudioClip lampNormal;
    public AudioClip lampGlitch1;
    public AudioClip lampGlitch2;

    AudioSource audioSource;


    // 1. enemy의 위치
    public Transform target;
    public Vector3 direction;
    // 2. 감지범위
    public float ditectionRange = 20;


    // 3. 램프의 빛(child object로 있는 light)
    Light light = null;
    // 4. 깜빡거릴 범위 (최소, 최대값)
    public float blinkRangeMin = 9;
    public float blinkRangeMax = 10.5f;

    void Start()
    {
        light = GetComponentInChildren<Light>();

        light.intensity = 15f;

        audioSource.clip = lampNormal;
        audioSource.Play();

    }

    void Update()
    {
        Blink();

        // 일정거리 안에 있을 시, 반짝여라
      
            light.intensity = Random.Range(blinkRangeMin, blinkRangeMax);
        

    }

    public void Blink()
    {
        // 일정 거리 안에 오면 태그로 감지하고싶다.


        //// target으로 삼을 Enemy의 위치를 찾아라
        //target = GameObject.FindWithTag("Enemy").transform;
        //// target의 거리에서 나의 위치까지의 거리를 구한다.
        //direction = target.position - transform.position;
        //direction.Normalize();

        // 일정 거리 안에 오면 태그로 감지하고싶다.

        //적 배열 만들어주고
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

        //에너미와 나의 거리를 담을 변수
        float distance;

        for (int i = 0; i <enemy.Length; i++)
        {
            // Enemy와 나의 거리를 float 값으로 치환
            distance = Vector3.Distance(enemy[i].gameObject.transform.position, transform.position);
            audioSource.clip = lampGlitch1;
            audioSource.Play();
            // 일정거리 안에 있을 시, 반짝여라
            //if (distance <= ditectionRange)
            //{
            //    light.intensity = Random.Range(blinkRangeMin, blinkRangeMax);
            //}
            // 더 가까이 왔을때 더 많이 깜빡여야한다. = min의 값을 나누기 해주면 그런 연출이 될듯?
            if (distance <= ditectionRange)
            {
                audioSource.clip = lampGlitch2;
                light.intensity = Random.Range(blinkRangeMin / 4, blinkRangeMax / 2);
            }

        }
        



    }
}
