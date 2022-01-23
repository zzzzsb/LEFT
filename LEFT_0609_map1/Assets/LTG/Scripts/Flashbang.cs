using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    // x초 후 발동되게 하고싶다.
    // - 필요 속성
    // 작동시간 
    public float activeTime;
    // 현재시간 
    float currentTime;

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
        cc = GetComponent<CharacterController>();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        //중력적용
        // v = v0 + at(a = 중력가속도)
        yVelocity += gravity * Time.deltaTime;

        // 바닥에 있다면 yVelocity는 0으로 초기화하고싶다
        if (cc.isGrounded)
        {
            //바닥에 있을 대는 yVelocity를 0으로 만들어주고싶다.
            yVelocity = 0;
        }

        dir.y = yVelocity;

        // 3. 이동하고싶다.
        // p = p0 + vt
        // 소문자 = 변수, 대문자+괄호 = 함수, 걍 대문자 = 클래스(자료형)
        // 클래스 = 그릇에 담을 수 있는 종류, 변수(instance) = 그릇의 종류(속성, ~~하고싶다) 함수 =  
        //transform.Translate(dir * speed * Time.deltaTime, Space.World);
        //transform.position += dir * speed * Time.deltaTime;
        cc.Move(dir * speed * Time.deltaTime);

        if (cc.isGrounded)
        {
            Bang();
        }
    }


    public void Bang()
    {
        //현재시간이 흐르다가
        currentTime += Time.deltaTime;
        //만약 현재시간이 작동시간을 넘어섰다면
        if (currentTime >= activeTime)
        {
            //터져라(bang!출력), state 변경
            print("Bang!");
            state = State.Bang;
            Destroy(gameObject);
        }
    }

}
