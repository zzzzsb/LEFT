using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    Light light = null;


    // 일정거리 안으로 들어오면 몬스터가 스턴으로 변하고싶다
    // - 감지범위, 
    public float ditectionRange = 200f;
    public Transform target = null;
    public Vector3 direction;

    // x초 후 발동되게 하고싶다.
    // - 필요 속성
    // 작동시간 
    public float activeTime;
    // 현재시간 
    float currentTime;

    // 빛 터지는 시간
    public float flashTime /*= GameObject.Find("Enemy").GetComponent<EnemyMove>().stunTime - 1f*/;

    // 필요 속성 : 중력의 크기, 수직 속도
    public float gravity = -20f;
    float yVelocity;
    public float speed = 0f;
    Vector3 dir;

    CharacterController cc = null;

    public enum State { Idle, Bang }
    public State state = State.Idle;

    void Start()
    {
        light = GetComponentInChildren<Light>();


        cc = GetComponent<CharacterController>();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {

            case State.Idle:
                break;
            case State.Bang:
                Bang();
                break;
        }
    }

    float currentTime2;
    public void Bang()
    {
        print("Bang!");
        //현재시간이 흐르다가
        currentTime += Time.deltaTime;
        //만약 현재시간이 작동시간을 넘어섰다면
        if (currentTime >= flashTime)
        {
            
           
        }
        

    }


    float colTime;
    public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag.Contains("Terrain"))
        {
            colTime++;

            if (colTime >= 2)
            {
                state = State.Bang;
                StartCoroutine(Flashing());
            }
        }

       
    }


    IEnumerator Flashing()
    {
        while(currentTime < flashTime)
        {
            light.intensity = Mathf.Lerp(1, 10000, 0.3f);
            yield return  null;
        }
        while(currentTime2 < 6)

        {
            light.intensity = Mathf.Lerp(10000, 0, 6f);
            Destroy(gameObject);
            yield return null;
        }
        
    }
}
