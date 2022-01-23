using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


// Clock의 state가 start일때 clock을 향해 다가가고싶다.
// - 필요 요소
// clock의 state
// clock의 위치

// 이동하지 않을시엔 좌우를 두리번거리고싶다.
// - 필요 요소
// 1. 현재 이동속도
// 2. 현재 y의 rotation

// 만약 플레이어가 시선에 들어온다면 플레이어를 따라가게 하고싶다.
// -필요 요소
// 1. 시선
// 2. 시선 쏘기
// 3. 시선이 닿은 곳의 정보
// 4. 시선의 범위

// 만약 플레이어가 시선에 들어온다면 플레이어를 바라보게 하고싶다.
// - 필요 요소(시선에서 썼던것들 제외)
// 1. 보는 방향이 돌아가는 속도
// 2. 현재의 각도

// 만약 플레이어가 감지범위에 들어온다면 플레이어를 따라가게 하고싶다.
// - 필요 요소
// 1. 감지범위
// 2. 플레이어와 자신의 위치


// 중력을 주고싶다. 땅에 닿았을 때는 0의 중력을 주고싶다.
// - 필요 요소
// 땅에 닿았는지를 체크(is grounded)
public class EnemyMove : MonoBehaviour
{

    public GameObject clock;
    public GameObject flashbang;

    Transform target;

    Vector3 dirFollow;
    Vector3 dir;
    CharacterController cc = null;
    public float speed;

    //감지범위
    public float ditectionRange;
    float velocity;

    //스턴시간
    public float stunTime;

    //시선범위
    public float sightRange;

    // 보는 방향이 돌아가는 속도
    public float rotSpeed = 200f;
    // 2. 현재의 각도
    float mx;
    float my;



    enum State { Idle, Attack, Stun, FoundPlayer, FollowClock }

    State state = State.Idle;



    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
                   

        switch (state)
        {
            //Idle상태일땐 Idle 함수 실행
            case State.Idle:
                //Idle();
                break;
            case State.Attack:
                //Attack();
                break;
            case State.Stun:
                Stun();
                break;
            case State.FoundPlayer:
                FoundPlayer();
                break;
            case State.FollowClock:
                FollowClock();
                break;
        }

        ////현재 속도가 0이라면
        //if (speed == 0)
        //{
        //    StartCoroutine(LookAround());
        //}

        //// 앞으로 가면서 지형에 충돌했을시 몸을 돌려서 가고싶다.
        //// 1. 앞으로 가고싶다.
        //cc.Move(Vector3.forward * speed * Time.deltaTime);
        //// 2. 만약 옆면이 지형에 충돌했다면
        //if (cc.collisionFlags == CollisionFlags.Sides)
        //{

        //    StartCoroutine(RotatinoChange());
        //}
        //위의 문제점. 한 번 collision이 됐기 때문에 계속 collision이 되는것. 일시적으로 collision이 deactivate되게 해야함.



        //만약 섬광탄이 bang state로 바뀐다면
        //if (flashbang.GetComponent<Flashbang>().state == Flashbang.State.Bang)
        if (Input.GetKeyDown(KeyCode.H))
        {
            //상태를 stun으로 바꿔줘라
            state = State.Stun;
        }


        //시선 만든다
        Ray ray = new Ray(transform.position, transform.forward);
        //시선의 길이를 정하고
        //시선을 쏜다
        RaycastHit hitInfo = new RaycastHit();
        //시선을 던졌을 때 충돌했다면
        if (Physics.Raycast(ray, out hitInfo))
        {
            //시선이 닿은 곳의 이름이 Player라면
            if (hitInfo.transform.name == "Player")
            {
                //state를 FoundPlayer로 바꿔주고
                state = State.FoundPlayer;
            }
        }
        //임시 출력
        if(Input.GetKeyDown(KeyCode.G))
        {
            state = State.FoundPlayer;
        }

        //만약 clock의 state가 start라면
        //if (clock.GetComponent<Clock>().state == Clock.State.Start)
        //임시 출력
        if (Input.GetKeyDown(KeyCode.J))
        {
            //State를 FollowClock으로 바꿔줘라
            state = State.FollowClock;
        }
        

        //만약 enemy의 state가 followClock이라면
        if (state == State.FollowClock)
        {
            print("followactive");
            //시계 따라가기 함수 실행
            FollowClock();
        }
       
    }
   

    void Stun()
    {
        
        // Stunning 코루틴 실행
        StartCoroutine(Stunning());
    }

    IEnumerator Stunning()
    {
       
        //원래 속도를 originSpeed 저장하자
        float originSpeed = speed;
        //속도를 0으로 바꿔주고
        speed = 0;
        //스턴시간만큼 쉰 후
        yield return new WaitForSeconds(stunTime);
        //스피드를 원래 속도로 바꿔주자.
        speed = originSpeed;
        //state를 idle로 바꿔주자
        state = State.Idle;

        //왜 여기서 originSpeed가 0으로 다시 바뀌는거지????
        print(originSpeed);

        //StopCoroutine(Stunning());

    }

    private void FoundPlayer()
    {
        //플레이어를 찾았다면

        //Player의 위치를 찾아서 벡터값을 구하고
        GameObject target = GameObject.Find("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();

        dir.y = 0;
        // 플레이어를 바라보게 하고싶다.
        //Quaternion q = Quaternion.LookRotation(dir);
        //transform.rotation = q;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized),
            rotSpeed * Time.deltaTime);

        // 그 쪽을 향해 1.3배 빠르게 움직여라
        cc.Move(dir * speed * 1.3f * Time.deltaTime);
    }


    private void FollowClock()
    {
        //Clock의 위치를 찾아서 벡터값을 구하고
        GameObject target = GameObject.Find("Clock");
        dir = target.transform.position - transform.position;
        dir.Normalize();
        //그쪽을 바라보게 하고
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized),
            rotSpeed * Time.deltaTime);
        //그 쪽을 향해 움직여라
        cc.Move(dir * speed * Time.deltaTime);
    }

    
}
