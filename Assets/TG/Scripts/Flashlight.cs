using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


// enemy가 가까이 온다면 플래쉬가 깜빡거리게 하고싶다.
// - 필요 요소 
// 1. enemy
// 2. 감지범위
// 3. 램프의 빛(child object로 있는 light)
// 4. 깜빡거릴 범위


// 특정 버튼을 누를시 꺼졌다가 켜졌다가 하고싶다.
// - 필요속성 : child로 들어가있는 light component들, light의 on off 상태


public class Flashlight : MonoBehaviour
{
    AudioSource audioSource;


    // 1. enemy의 위치
    public Transform target;
    public Vector3 direction;
    // 2. 감지범위
    public float ditectionRange;
    // 3. 깜빡거릴 범위 (최소, 최대값)
    public float blinkRangeMin;
    public float blinkRangeMax;

    Light light1 = null;
    Light light2 = null;
    Light light3 = null;
    Light light4 = null;

    int brightness;
    

    public enum State
    {
        Brightness1, Brightness2, Brightness3, Off
    }
    State state = State.Off;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //light = GetComponentInChildren<Light>();

        light1 = transform.GetChild(1).GetComponentInChildren<Light>();
        light2 = transform.GetChild(2).GetComponentInChildren<Light>();
        light3 = transform.GetChild(3).GetComponentInChildren<Light>();

        state = State.Off;

        switch (state)
        {
            case State.Off:
                Off();
                break;
            case State.Brightness1:
                Brightness1();
                break;
            case State.Brightness2:
                Brightness2();
                break;
            case State.Brightness3:
                Brightness3();
                break;
           
        }

    }

    void Update()
    {
        // y키를 눌렀을때 불의 밝기를 바꾸다가 최대 다음은 끄게해라
        // 디폴트 = 0에서 하나씩 증가하면 된다.

        // 1. y키를 눌렀을 때
        if  (Input.GetKeyDown(KeyCode.Y))
        {
            // 2. 불의 밝기를 하나씩 증가시켜라.
            brightness += 1;
            audioSource.Play();
        }
        

        #region 불의 밝기
        // 3. 만약 불의 밝기가 0이라면
        if (brightness == 0)
        {
            Off();
        }
        //5. 만약 불의 밝기가 1이라면
        if (brightness == 1)
        {
            Brightness1();
        }
        // 이하동문이다.
        if (brightness == 2)
        {
            Brightness2();
        }
        if (brightness == 3)
        {
            Brightness3();
        }
        // 7. 불의 밝기가 3보다 크다면 0으로 만들어줘라
        if (brightness > 3)
        {
            brightness = 0;
        }
        #endregion


        Blink();
    }

    private void Off()
    {
        light1.enabled = false;
        light2.enabled = false;
        light3.enabled = false;

    }
    private void Brightness1()
    {
        light3.enabled = true;
    }
    private void Brightness2()
    {
        light2.enabled = true;
    }
    private void Brightness3()
    {
        light1.enabled = true;
    }


    public void Blink()
    {

        //적 배열 만들어주고
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");

        //에너미와 나의 거리를 담을 변수
        float distance;

        for (int i = 0; i < enemy.Length; i++)
        {
            // Enemy와 나의 거리를 float 값으로 치환
            distance = Vector3.Distance(enemy[i].gameObject.transform.position, transform.position);

            // 일정거리 안에 있을 시, 반짝여라
            if (distance <= ditectionRange)
            {
                light1.intensity = Random.Range(blinkRangeMin, blinkRangeMax);
                light2.intensity = Random.Range(blinkRangeMin, blinkRangeMax);
                light3.intensity = Random.Range(blinkRangeMin, blinkRangeMax);
            }
            // 더 가까이 왔을때 더 많이 깜빡여야한다. = min의 값을 나누기 해주면 그런 연출이 될듯?
            if (distance <= ditectionRange / 2)
            {
                light1.intensity = Random.Range(blinkRangeMin / 4, blinkRangeMax / 4);
                light2.intensity = Random.Range(blinkRangeMin / 4, blinkRangeMax / 4);
                light3.intensity = Random.Range(blinkRangeMin / 4, blinkRangeMax / 4);
            }

        }


    }
}
