using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//충돌하고 일정시간 후 oil이 나오게 하고싶다.

public class Barrel : MonoBehaviour
{


    public GameObject firePosition;
    public GameObject fuelFactory;

    float count;
    float currentTime;
    public float oilLeakingTime;
    public float burningLimitTime;
    bool burning = false;

    public enum State
    {
        Idle, Burn
    }
    public State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                
                break;
            case State.Burn:
                Burn();
                break;
        }
    }

    // 불에 타면서 검게 변해라
    // 산산조각날 수 있으면 좋을듯

        // 1. 함수가 호출되면
    public void Burn()
    {
       

        currentTime += Time.deltaTime;
        // 4. 색을 lerp로 천천히 바뀌게 해준다.
        GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.black, 3);

        
        //// 2. 타기 시작한다
        //burning = true;
        //// 3. 만약 타는것이 사실이라면
        //if (burning)
        //{

        //}

        //// 만약 타고있는 현재 시간이 제한시간을 지난다면
        //if (currentTime > burningLimitTime)
        //{
        //    // 그만타라
        //    burning = false;
        //}
    }


    // 지형에 충돌하고 leakingtime이 지나면 기름이 흘러나오게 하고싶다.
    private void OnCollisionEnter(Collision other)
    {
       
    }
    public void Destroy()
    {
        float destroyTime = 3f;   
        currentTime += Time.deltaTime;
        if( currentTime > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
