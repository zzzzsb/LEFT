using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
// 필요속성 : 속도

// 카메라가 바라보는 방향으로 이동하고 싶다.

// Character Controller 컴포넌트를 이용하여 이동하고 싶다.

// 중력 적용하기
// 필요속성 : 중력, 수직속도

// 점프처리 하고싶다.
// 필요속성 : 점프파워

// 이단점프만 하고싶다.
// 필요속성 : 점프횟수

public class PlayerMove : MonoBehaviour
{
    // 플레이어 걸을때 이동속도
    public float walkSpeed = 3;
    // 플레이어 뛸때 이동속도
    public float runSpeed = 10;
    // 플레이어가 움직일때 적용할 속도
    public float applySpeed;

    // 앉을 때 속도
    public float crouchSpeed = 1.5f;

    // 움직임 상태변수
    private bool isRun = false;
    // 앉음 유무 상태변수
    private bool isCrouch = false;

    // 앉았을 때 얼마나 앉을지 결정하는 변수
    public float crouchPosY;
    // 원래 처음 높이
    private float originPosY;
    // 적용할 높
    private float applyCrouchPosY;

    // Character Controller 컴포넌트
    CharacterController cc;

    // 중력
    public float gravity = -20;
    // 수직속도
    float yVelocity;

    // 점프파워
    public float jumpPower = 10;

    // 점프횟수
    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        // Player 게임오브젝트의 컴포넌트 중 CharacterController 컴포넌트를 얻어오고 싶다.
        cc = GetComponent<CharacterController>();
        applySpeed = walkSpeed;
        originPosY = Camera.main.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
        // 사용자의 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 방향이 필요
        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        // Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        // 카메라가 바라보는 방향으로 이동하고 싶다.
        dir = Camera.main.transform.TransformDirection(dir);

        // 사용자가 바닥에 있다면
        if (cc.isGrounded)
        {
            // yVelocity를 0으로 초기화
            yVelocity = 0;
            // 땅에 있으면 점프횟수를 초기화
            jumpCount = 0;
        }

        // 점프하고 싶다
        // 2단점프까지만 하고싶다.
        // 점프버튼을 눌렀을때 점프횟수가 2미만이면 점프횟수를 증가시키고, 점프한다.
        if(jumpCount < 2)
        {
            // 만약 사용자가 점프버튼(스페이스바) 누르면
            if (Input.GetButtonDown("Jump"))
            {
                // 점프 횟수를 증가시키고
                jumpCount++;
                // 수직 속도에 점프파워를 넣자
                yVelocity = jumpPower;
            }
        }

        // 중력 적용
        // v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 걸을건지 뛸건지 체크
        RunCheck();
        // 앉을건지 서있을건지 체크
        CrouchCheck();
        // 이동하고 싶다
        // transform.position += dir * speed * Time.deltaTime;
        cc.Move(dir * applySpeed * Time.deltaTime);

    }

    // LeftShift키를 누르면 뛰고, 아니면 걷는다.
    private void RunCheck()
    {
        // LeftShift키를 눌렀을때(뛸때)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 뛸때는 applySpeed에 runSpeed를 적용하고 싶다.
            isRun = true;
            applySpeed = runSpeed;
        }
        // LeftShift키를 누르지 않았을때(걸을때)
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // 걸을때는 applySpeed에 walkSpeed를 적용하고 싶다.
            isRun = false;
            applySpeed = walkSpeed;
        }
    }

    // LeftControl키를 누르면 앉고, 아니면 서있고 싶다.
    private void CrouchCheck()
    {
        // LeftControl키를 눌렀을때(앉고 싶을 때)
        if (Input.GetKeyDown(KeyCode.LeftCommand))
        {
            Crouch();
        }

    }

    private void Crouch()
    {
        isCrouch = !isCrouch;

        // 앉았을때
        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        // 일어 서있을때
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        //  카메라 위치 변경
        //Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, applyCrouchPosY, Camera.main.transform.localPosition.z);
        StartCoroutine(CrouchCoroutine());
    }

    // 병렬처리 개념
    IEnumerator CrouchCoroutine()
    {
        float _posY = Camera.main.transform.localPosition.y;
        int count = 0;

        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            Camera.main.transform.localPosition = new Vector3(0, _posY, 0);
            if(count > 15)
            {
                break;
            }
            //한프레임 대기
            yield return null; 
        }
        Camera.main.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

}
