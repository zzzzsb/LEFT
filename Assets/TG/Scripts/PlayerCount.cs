using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//문제점 = 스팀과 충돌처리가 안된다. 캐릭터 컨트롤러 충돌처리 어떻게 하는걸까.




public class PlayerCount : MonoBehaviour
{
    public float steamCount;
    CharacterController cc = null;
    float fuelCount;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //만약 스팀을 2번이상 맞았다면
        if (steamCount >= 2)
        {
            print("스팀맞고 주금");
            steamCount = 0;
        }
    }
    //스팀에 맞는다면
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.collider.gameObject.name);

         //닿은 물체의 이름이 steam일시
        if (hit.collider.gameObject.name.Contains("Steam"))
        {
            //스팀카운터 1씩증가
            steamCount += 1;
            print("스팀맞았음");
        }

        if (hit.collider.gameObject.name.Contains("Fuel"))
        {
            fuelCount += 1;
            print(fuelCount + "개의 연료 획득");
        }
    }
}
