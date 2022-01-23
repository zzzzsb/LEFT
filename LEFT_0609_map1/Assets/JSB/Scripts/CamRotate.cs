using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 마우스 입력에 따라 물체를 회전시키고 싶다.

public class CamRotate : MonoBehaviour
{
    // 필요속성 : 회전속도
    public float rotSpeed = 200;

    // 유니티에서 자체적으로 eulerAngle 프로퍼티가 음수값이 되면 +360 해버림 -> 해결!!
    // 자체적으로 각도를 속성으로 저장해서 쓰고싶다.
    float mx;
    float my;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 마우스 입력에 따라
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        // 회전 각도(방향)
        mx += h * rotSpeed * Time.deltaTime;
        my += v * rotSpeed * Time.deltaTime;
       
        // 만약 회전의 X값이 60보다 크면 60으로 설정
        // 만약 회전의 X값이 -60보다 작으면 -60으로 설정
        my = Mathf.Clamp(my, -60, 60);

        // 회전 제한이 적용된 값을 최종 적용
        transform.eulerAngles = new Vector3(-my, mx, 0);
      
    }
}
