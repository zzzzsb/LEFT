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
    // 1. enemy의 위치
    public Transform target;
    public Vector3 direction;
    // 2. 감지범위
    public float ditectionRange;


    // 3. 램프의 빛(child object로 있는 light)
    Light light = null;
    // 4. 깜빡거릴 범위 (최소, 최대값)
    public float blinkRangeMin;
    public float blinkRangeMax;

    void Start()
    {
        light = GetComponentInChildren<Light>();

        light.intensity = 70;
    }

    void Update()
    {
        // target으로 삼을 Enemy의 위치를 찾아라
        target = GameObject.Find("Enemy").transform;
        // target의 거리에서 나의 위치까지의 거리를 구한다.
        direction = target.position - transform.position;
        direction.Normalize();

        // Enemy와 나의 거리를 float 값으로 치환
        float distance = Vector3.Distance(target.position, transform.position);
        {
            // 일정거리 안에 있을 시, 반짝여라
            if (distance <= ditectionRange)
            {
                light.intensity = Random.Range(blinkRangeMin, blinkRangeMax);
            }
            // 더 가까이 왔을때 더 많이 깜빢여야한다. = min의 값을 나누기 해주면 그런 연출이 될듯?
            if (distance <= ditectionRange/2)
            {
                light.intensity = Random.Range(blinkRangeMin/4, blinkRangeMax);
            }
        }

    }

    public void Blink()
    {
        
       

    }
}
