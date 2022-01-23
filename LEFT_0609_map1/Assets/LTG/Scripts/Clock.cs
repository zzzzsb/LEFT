using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{
    CharacterController cc = null;


    public enum State { Idle, Start }
    public State state = State.Idle;


    // 필요 속성 : 중력의 크기, 수직 속도
    public float gravity = -20f;
    float yVelocity;
    public float speed = 3f;
    Vector3 dir;

    //-- set start time 00:00
    public int minutes = 0;
    public int hour = 0;
    public int minutesLimit = 5;

    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    int seconds;
    float msecs;
    GameObject pointerSeconds;
    GameObject pointerMinutes;
    GameObject pointerHours;
    void Start()
    {
        pointerSeconds = transform.Find("rotation_axis_pointer_seconds").gameObject;
        pointerMinutes = transform.Find("rotation_axis_pointer_minutes").gameObject;
        pointerHours = transform.Find("rotation_axis_pointer_hour").gameObject;

        msecs = 0.0f;
        seconds = 0;
        //시계가 꺼진 상태로 되어라
        state = State.Idle;

        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        //만약 state가 start라면

        if (cc.isGrounded)
        {
            //state를 start로 바꿔줘라
            state = State.Start;
            
            //만약 Limit의 시간에 도달한다면
            if (minutes >= minutesLimit)
            {
                //state를 idle로 바꿔줘라
                state = State.Idle;
            }

        }

        //만약 state가 start라면
        if (state == State.Start)
        {
            StartClock();
        }

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

    }
    void StartClock()
    {
        //-- calculate time
        msecs += Time.deltaTime * clockSpeed;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
                if (minutes > 60)
                {
                    minutes = 0;
                    hour++;
                    if (hour >= 24)
                        hour = 0;
                }
            }
        }


        //-- calculate pointer angles
        float rotationSeconds = (360.0f / 60.0f) * seconds;
        float rotationMinutes = (360.0f / 60.0f) * minutes;
        float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

        //-- draw pointers
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
    }

    private void OnCollisionEnter(Collision other)
    {
        ////만약 충돌한 것 태그가 Terrain을 포함한다면
        //if (other.gameObject.tag.Contains("Terrain"))
        //{
        //    //시계를 시작하는 start state로 만들어줘라.
        //    state = State.Start;


        //}
        //print(other.gameObject.name);
    }

}
