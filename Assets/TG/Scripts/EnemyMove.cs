using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


// Player가 일정 범위 안에 있으면 뛰어들고싶다.
// - 필요요소 : 뛰어드는 속도, 일정 범위(플레이어의 위치, 나의 위치)


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
// - 필요 요소 agent
// 1. agent

// 만약 플레이어가 감지범위에 들어온다면 플레이어를 따라가게 하고싶다.
// - 필요 요소
// 1. 감지범위
// 2. 플레이어와 자신의 위치

// 일정 범위를 기준으로 돌아다니고싶다
//  - 필요요소 : 일정 범위.

// 중력을 주고싶다. 땅에 닿았을 때는 0의 중력을 주고싶다.
// - 필요 요소
// 땅에 닿았는지를 체크(is grounded)


// 일정 범위를 정찰하고싶다
// 필요요소 : agent, nav 범위.

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{

    public NavMeshAgent agent = null;

    Vector3 originPosition;

    // 중력
    public float gravity = -20;
    // 수직속도
    float yVelocity;

    public Animator anim;

    IEnumerator coroutine;


    public GameObject clock;
    public GameObject flashbang;



    Vector3 dirFollow;
    public Vector3 dir;
    CharacterController cc = null;
    public float speed;


    float velocity;

    //스턴시간
    public float stunTime;


    //시선범위
    public float sight = -0.3f;

    // 보는 방향이 돌아가는 속도
    public float rotSpeed = 200f;
    // 2. 현재의 각도
    float mx;
    float my;


    // 달려드는 힘
    public float attackJumpPower;
    // 돌아다니는 범위
    public float movingRange;

    float currentTime;
    public float patrolTime = 3;

    public enum State { Idle, Move, MoveFast, Attack, AttackStart, Stun, FoundPlayer, FollowClock }

    public State state = State.Idle;



    Vector3 forward = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {

        cc = GetComponent<CharacterController>();

        anim = GetComponentInChildren<Animator>();

        state = State.Idle;
        anim.SetTrigger("Idle");


        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        //found에서만 쓰일것이기 떄문에 Move에서만 쓸 수 있게 default는 꺼준다.
        agent.enabled = false;
        //StartCoroutine(lookAround());

        GetComponent<EnemySound>().IdleMoveSound();

    }

    public void PlayAgent(Vector3 position)
    {
        StartCoroutine(ActiveAgent(position));
        //agent.SetDestination(position);
    }
    IEnumerator ActiveAgent(Vector3 position)
    {
        agent.enabled = true;

        yield return new WaitForSeconds(0.1f);
        agent.SetDestination(position);

    }

    // Update is called once per frame
    void Update()
    {
        print(state + "상태");

       




        // 중력 적용
        // v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        switch (state)
        {
            //Idle상태일땐 Idle 함수 실행
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.AttackStart:
                AttackStart();
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

        //만약 섬광탄이 bang state로 바뀐다면
        if (flashbang.GetComponent<Flashbang>().state == Flashbang.State.Bang)
        {
            //상태를 stun으로 바꿔줘라
            state = State.Stun;
            StartCoroutine(Stunning());
            anim.SetTrigger("Idle");
        }
       

        




        if (Input.GetKeyDown(KeyCode.O))
        {
            state = State.Idle;

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GetComponent<EnemySound>().FoundSound();
            print("파운드사운드");
            //State를 FollowClock으로 바꿔줘라
            anim.SetTrigger("MoveFast");
            state = State.FoundPlayer;
        }



        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))  //0 = base layer
        //{
        //    print("ㅇㅇ");
        //}
    }

   

    //감지범위
    public float ditectionRange;
    Transform target;
    Vector3 dist;
    private void Attack()
    {
        //anim.SetTrigger("Attack");

        // 2. Player의 위치를 찾아서 벡터값을 구하고
        GameObject target = GameObject.Find("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();


        // 플레이어를 바라보게 하고싶다.
        //Quaternion q = Quaternion.LookRotation(dir);
        //transform.rotation = q;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized),
            rotSpeed * Time.deltaTime);

        

        //시간이 흘러라
        currentTime += Time.deltaTime;
        // 1초가 흐르고
        if (currentTime > 1f)
        {
            // 잠깐 뒤로 간다음에
            //cc.Move(Vector3.back * speed * Time.deltaTime);
            //agent.destination = -target.transform.position;
            {
                //멈췄다가
                agent.speed = 0;
                anim.SetTrigger("Idle");
                // 뛰어들어라
                if (currentTime > 3f)
                {
                    GetComponent<EnemySound>().AttackSound();
                    anim.SetTrigger("Attack");
                    state = State.AttackStart;
                    
                }
            }
        }

    }

    public void AttackStart()
    {
        agent.enabled = false;
        // 그 쪽을 향해 jumpPower만큼 빠르게 움직여라
        cc.SimpleMove(dir * attackJumpPower);
        //cc.Move(dir * speed * 3 * Time.deltaTime);
    }


    float currentTimeMove;
    public float patrolRange = 4;
    public Vector3 moveDir = Vector3.forward;
    public Vector3 destinationDefault ;
    private void Move()
    {
        ////만약 clock의 state가 start라면
        //if (clock.GetComponent<Clock>().state == Clock.State.StartC)
        //{
        //    print("호출2");
        //    //State를 FollowClock으로 바꿔줘라
        //    state = State.FollowClock;

        //}



        GameObject target = GameObject.Find("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();
        // 내가 향하는 방향과 타겟쪽으로의 방향 사이각이 x 도 이내라면 
        if (Vector3.Dot(transform.forward, dir) < sight)
        {
            // ray 를 생성후  Player를 향해 ray를 쏘고
            Ray ray = new Ray(transform.position, GameObject.Find("Player").transform.position);
            RaycastHit hitInfo = new RaycastHit();
            
            // 만약ray에 충돌한 것 정보가 Player라면
            if(Physics.Raycast(ray, out hitInfo) == name.Contains("Player"))
            {
                GetComponent<EnemySound>().FoundSound();
                print("파운드사운드");
                // 따라가기
                state = State.FoundPlayer;
                anim.SetTrigger("MoveFast");

                
            }
            
            
        }




        anim.SetTrigger("Move");
        destinationDefault.Normalize();
        destinationDefault.y = yVelocity;
        
        


        //6초 후 idle 상태로 바꿔주고싶다
        currentTimeMove += Time.deltaTime;
        if (currentTimeMove > 8)
        {
            
            agent.enabled = false;
            state = State.Idle;
            
            currentTimeMove = 0;
        }

        //if (clock.GetComponent<Clock>().state == Clock.State.Start)
        //{
        //    state = State.FollowClock;
        //}

    }
    float currentTime2;
    private void Idle()
    {
        ////만약 clock의 state가 start라면
        //if (clock.GetComponent<Clock>().state == Clock.State.StartC)
        //{
        //    print("호출2");
        //    //State를 FollowClock으로 바꿔줘라
        //    state = State.FollowClock;

        //}



        GameObject target = GameObject.Find("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();


        Ray ray = new Ray(transform.position, target.transform.position);
        RaycastHit hitInfo = new RaycastHit();

        Debug.DrawRay(transform.position, target.transform.position, Color.green);



        // 내가 향하는 방향과 타겟쪽으로의 방향 사이각이 x도 이내라면 따라가기
        if (Vector3.Dot(transform.forward, dir) < sight)
        {

            // ray 를 생성후  Player를 향해 ray를 쏘고
            //Ray ray = new Ray(transform.position, GameObject.Find("Player").transform.position);
            //RaycastHit hitInfo = new RaycastHit();

            Debug.DrawRay(transform.position, target.transform.position);

            // 만약ray에 충돌한 것 정보가 Player라면
            if (Physics.Raycast(ray, out hitInfo) == name.Contains("Player"))
            {
                GetComponent<EnemySound>().FoundSound();
                print("파운드사운드");
                // 따라가기
                state = State.FoundPlayer;
                anim.SetTrigger("MoveFast");
                
            }

            //if (clock.GetComponent<Clock>().state == Clock.State.Start)
            //{
            //    state = State.FollowClock;
            //}

        }




        anim.SetTrigger("Idle");
        currentTime += Time.deltaTime;
        
        // 일정시간 후 move로 바꿔주고싶다.
        if (currentTime > patrolTime)
        {
            //StopCoroutine(lookAround());
            state = State.Move;
            
            if (agent.enabled == false)
            {
                
                agent.speed = speed;
                agent.enabled = true;
                GameObject.Find("EnemyPatrolManager").GetComponent<EnemyPatrolSpot>().ChangeDestNation();
            }
            
            currentTime = 0;
        }

    }


    public void Stun()
    {
        //coroutine = Stunning();
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

        StopCoroutine(Stunning());
        //StopCoroutine(Stunning());
    }

    private void FoundPlayer()
    {
        // 1. 플레이어를 찾았다면

        // 2. Player의 위치를 찾아서 벡터값을 구하고
        GameObject target = GameObject.Find("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();


        // 1. agent를 활성화시켜주고
        if (agent.enabled == false)
        { 
            agent.speed = speed * 3f;
            agent.enabled = true;
        }
        //길찾기를 수행한다.
        // agent는 목적지만 설정해주면 방향도 알아서 바꾸고 한다
        agent.destination = target.transform.position;


        dir.y = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir.normalized),
            rotSpeed * Time.deltaTime);
        

        //// 그 쪽을 향해 1.3배 빠르게 움직여라
        ////speed = speed * 1.3f;
        //cc.Move(dir * speed * 1.3f * Time.deltaTime);



        // Player가 일정 범위 안에 있으면 뛰어들고싶다.
        // 1. target의 거리에서 나의 위치까지의 거리를 구한다.
        dist = target.transform.position - transform.position;
        dist.Normalize();

        // 2. Enemy와 나의 거리를 float 값으로 치환
        float distance = Vector3.Distance(target.transform.position, transform.position);

        // 3. 일정거리 안에 있을 시, 잠깐 뒤로갔다가다가 Attack해라
        if (distance <= ditectionRange)
        {
            state = State.Attack;
        }
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

        if (target.GetComponent<Clock>().minutesLimit < target.GetComponent<Clock>().minutes)
        {
            state = State.Idle;
        }
    }


    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.name == ("Player"))
        {
            other.transform.GetComponent<PlayerDie>().Die();
        }
    }


}
