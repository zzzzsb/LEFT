using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 만약 플레이어의 인벤토리에 열쇠와 연료5개가 있는 상태에서
// 플레이어가 헬리콥터방과 연결된 문에 닿는다면
// 플레이어는 헬리콥터 룸으로 이동한다.

// moveCount가 6이되면 이동하도록
public class HelicopterRoom : MonoBehaviour
{
   

    // 탈출여부 판단할 변수
    bool IsExit = false;

    // 아이템 이름이 저장된 인벤토리
    //public List<GameObject> InventoryList;

    // 헬리콥터 방 내의 플레이어 스팟
    public GameObject exitSpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     

        

    }

    // 플레이어와 충돌시 실행되는 함수
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.gameObject.name);
        //transform.position = exitSpot.transform.position;

        PlayerInventory pi = GetComponent<PlayerInventory>();

        // 만약 플레이어의 인벤토리에 열쇠와 연료5개(5개이상)가 있는 상태라면
        // 탈출에 필요한 아이템이 다 있다면
        if ((pi.keyCount == 1) && (pi.fuelCount >= 5))
        {
            // 탈출가능한 상태
            IsExit = true;
        }

        // 만약 플레이어와 충돌한 게임오브젝트의 태그가 "ExitDoor" 라면
        if (hit.collider.gameObject.CompareTag("ExitDoor")){

            print("탈출문에 닿았음");

            // 만약 플레이어가 탈출 가능한 상태라면
            if (IsExit)
            {
                // 플레이어의 위치를 헬리콥터 방 안의 플레이어 스팟으로 이동시킴
                //transform.position = exitSpot.transform.position;
                // 아니면 헬리콥터 탈출루트 씬으로 이동함
                SceneManager.LoadScene("Hell");
            }
            
        }
        

        

    }
}
