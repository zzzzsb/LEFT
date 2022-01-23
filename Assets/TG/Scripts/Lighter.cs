using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{

    // 특정 키를 누르면 불이 나오게 하고싶다
    // 특정 키를 일정 시간동안 누르면 불이 계속 나오게 하고싶다
    // 불은 fireLimit이 넘지 않는이상계속 나올것이다.
    // state를 만들자
    //불이 붙어있는 상태라면 몇초동안 지속되다가 꺼져라

    public GameObject firePosition;
    public GameObject fireFactory;
    float currentTime;
    public float ignitionTime = 0.7f;
    float currentFire;
    public float gasLimit = 5;

    public enum State
    {
        Intermittent, Activate, Deactivate
    }
    State state = State.Deactivate;

    void Start()
    {
        switch (state)
        {

            case State.Intermittent:
                Intermittent();
                break;
            case State.Activate:
                Activate();
                break;
            case State.Deactivate:
                Deactivate();
                break;

        }

        state = State.Deactivate;
    }


    void Update()
    {
        if (gasLimit > 0)
        {
            // 특정 키를 누르면 불이 나오게 하고싶다
            if (Input.GetKeyDown(KeyCode.T))
            {
                state = State.Intermittent;
                Intermittent();
            }

            // t키를 누르고있으면
            if (Input.GetKey(KeyCode.T))
            {
                StartCoroutine(Ignition());
            }

            //키를 떼면 꺼지게 한다
            if (Input.GetKeyUp(KeyCode.T))
            {
                state = State.Deactivate;
                Deactivate();
            }
        }
        //print(gasLimit);
    }

    private void Intermittent()
    {
        //팩토리에서 만들어서
        GameObject fire = Instantiate(fireFactory);
        //이동시켜준다.
        fire.transform.position = firePosition.transform.position;
        //가스가 점점 줄게해라
        gasLimit -= 0.5f;
    }

    private void Activate()
    {
    }

    private void Deactivate()
    {
        //ignitionTime = 0;
        //currentFire = 0;
    }

    IEnumerator Ignition()
    {
        state = State.Activate;
        yield return new WaitForSeconds(0.5f);
        currentTime += Time.deltaTime;
        gasLimit -= Time.deltaTime / 3;
        // ignitionTime을 초과할때까지 누르고있다면
        if (currentTime > ignitionTime)
        {
            //팩토리에서 만들어서
            GameObject fire = Instantiate(fireFactory);
            //이동시켜준다.
            fire.transform.position = firePosition.transform.position;
        }

    }
    private void OnCollisionEnter(Collision other)
    {



        // 발화된 상태에서 fuel과 충돌시 폭발음을 내면서 사라지고싶다. 

        // 1.부딪한 물체 이름이 fuel이라면
        if (other.collider.name.Contains("Fuel"))
        {
            print("불붙었다");
            GameObject.Find("Fuel").GetComponent<Barrel>().Burn();
            // 2. 부딪혔을때 만약 발화된 상태라면
            if (state == State.Activate)
            {

                // 3. 폭발음을 내라
                // 4. Fuel의 state를 변경해주고
                GameObject.Find("Fuel").GetComponent<Barrel>().state = Barrel.State.Burn;
                // 5. 사라져라
                Destroy(gameObject);
            }
        }

    }
}
