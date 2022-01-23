using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{
    CharacterController cc = null;


    public enum State { Idle, Start }
    public State state = State.Idle;


    // �ʿ� �Ӽ� : �߷��� ũ��, ���� �ӵ�
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
        //�ð谡 ���� ���·� �Ǿ��
        state = State.Idle;

        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        //���� state�� start���

        if (cc.isGrounded)
        {
            //state�� start�� �ٲ����
            state = State.Start;
            
            //���� Limit�� �ð��� �����Ѵٸ�
            if (minutes >= minutesLimit)
            {
                //state�� idle�� �ٲ����
                state = State.Idle;
            }

        }

        //���� state�� start���
        if (state == State.Start)
        {
            StartClock();
        }

        //�߷�����
        // v = v0 + at(a = �߷°��ӵ�)
        yVelocity += gravity * Time.deltaTime;

        // �ٴڿ� �ִٸ� yVelocity�� 0���� �ʱ�ȭ�ϰ�ʹ�
        if (cc.isGrounded)
        {
            //�ٴڿ� ���� ��� yVelocity�� 0���� ������ְ�ʹ�.
            yVelocity = 0;
        }

        dir.y = yVelocity;

        // 3. �̵��ϰ�ʹ�.
        // p = p0 + vt
        // �ҹ��� = ����, �빮��+��ȣ = �Լ�, �� �빮�� = Ŭ����(�ڷ���)
        // Ŭ���� = �׸��� ���� �� �ִ� ����, ����(instance) = �׸��� ����(�Ӽ�, ~~�ϰ�ʹ�) �Լ� =  
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
        ////���� �浹�� �� �±װ� Terrain�� �����Ѵٸ�
        //if (other.gameObject.tag.Contains("Terrain"))
        //{
        //    //�ð踦 �����ϴ� start state�� ��������.
        //    state = State.Start;


        //}
        //print(other.gameObject.name);
    }

}
