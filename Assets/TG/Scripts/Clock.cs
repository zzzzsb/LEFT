using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{
    CharacterController cc = null;
    public AudioClip audioClip;
    public AudioSource audioSource;


    public enum State { Idle, StartC }
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
        audioSource = GetComponent<AudioSource>();


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
        print(state + "시계스테");

        switch (state)
        {

            case State.Idle:
                break;
            case State.StartC:
                StartC();
                break;
        }


        if(minutesLimit < minutes)
        {
            state = State.Idle;
            Destroy(gameObject);
        }

    }
    void StartC()
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


    public float colTime;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Terrain"))
        {
            colTime++;

            if (colTime == 2)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                //시계를 시작하는 start state로 만들어줘라.
                state = State.StartC;
                

            }
        }
    }
}
