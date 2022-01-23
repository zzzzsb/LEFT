using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 뛸 때 팔을 회전하게 하고싶다.
// 왼팔은 흔들리고 (x -20~20)
// 오른팔은 아이템을 들고있어서 고정한다. (0도)

public class ArmMove : MonoBehaviour
{

    // 회전각도
    Vector3 armRotAngle;

    // 회전속도
    public float rotSpeed = 4f;

    // 팔의 rotation의 x값에 대입할 변수
    float armX;

    // 회전 유무를 판단할 부울변수
    bool armRotate = false;

    Vector3 from;
    Vector3 to;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();

        // 플레이어가 뛸때
        if (pm.applySpeed == pm.runSpeed)
        {
            
            transform.Rotate(new Vector3(1, 0, 0) * 200 * Time.deltaTime);

            float rotX = transform.rotation.x;
            
            if(rotX > 60)
            {
                transform.Rotate(new Vector3(0, 0, 0));
            }

            // 팔을 회전시킨다

            armRotate = true;
            //ArmRotate();
        }

    }
    /*
    // 팔을 회전시키는 함수
    IEnumerator ArmRotate()
    {
        Vector3 arm_originEuler = transform.eulerAngles;

        
    }
    */
}
