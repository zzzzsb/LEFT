using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 카메라 흔들림 크기 변수
    [SerializeField] float m_force = 0f;
    // 흔들릴 방향을 결정지을 벡터
    [SerializeField] Vector3 m_offSet = Vector3.zero;
    // 카메라의 초기값을 저장할 쿼터니온 변수
    Quaternion m_originRot;

    // Start is called before the first frame update
    void Start()
    {
        // 카메라의 회전값을 쿼터니온 변수에 넣는다.
        m_originRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ShakeCoroutine());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            StartCoroutine(Reset());
        }

    }

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCoroutine());
    }
    public void ResetCamera()
    {
        StopAllCoroutines();
        StartCoroutine(Reset());
    }

    // 카메라의 흔들림을 구현할 코루틴 함수
    IEnumerator ShakeCoroutine()
    {
        // 카메라의 오일러 초기값 지정
        Vector3 t_origninEuler = transform.eulerAngles;

        while (true)
        {
            // 벡터 축(x,y,z)마다 랜덤값 부여
            float t_rotX = Random.Range(-m_offSet.x, m_offSet.x);
            float t_rotY = Random.Range(-m_offSet.y, m_offSet.y);
            float t_rotZ = Random.Range(-m_offSet.z, m_offSet.z);

            // 흔들림 값 = 초기 값 + 랜덤 값
            Vector3 t_randomRot = t_origninEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            // 흔들림 값을 쿼터니온으로 변환
            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            // 목적 값까지 움직일 때 까지 반복하기
            // 반복되며 랜덤하게 흔들린다.
            while(Quaternion.Angle(transform.rotation, t_rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            }

            yield return null;
        }
    }

    // 카메라를 초기값으로 되돌리는 리셋함수
    IEnumerator Reset()
    {
        while(Quaternion.Angle(transform.rotation, m_originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;

        }
    }
}
