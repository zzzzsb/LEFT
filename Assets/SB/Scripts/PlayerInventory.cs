using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

// 만약 플레이어한테 아이템이 닿으면
// 아이템은 인벤토리 맵으로 이동하게 하고싶다.
// 아이템은 플레이어 인벤토리 리스트로 들어가게 하고싶다.


public class PlayerInventory : MonoBehaviour
{

    public List<string> inventoryList;
    public GameObject inventoryMap;
    public GameObject rightArmItemSpot;

    // 탈출에 필요한 아이템을 확인할 변수
    public int keyCount;
    public int fuelCount;

    public Text keyText;
    public Text fuelText;

    GameObject rightArm = null;

    public int itemIdx;

    // Start is called before the first frame update
    void Start()
    {
        itemIdx = -1;

        rightArm = GameObject.Find("RightArm");

    }

    // Update is called once per frame
    void Update()
    {
        // 1을 누를때마다 0부터 1번까지 아이템이 번갈아가며 나온다.
        // 아이템 인벤토리 리스트의 개수를 초과하는 인덱스에 접근하면 인덱스는 0번으로 만들고싶다.
        // 사용자가 z를누르면 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //아이템이 오른손에 나오게 하고싶다.
            itemIdx++;
            
            // 아이템 인벤토리 리스트의 개수를 초과하는 인덱스에 접근하면 인덱스는 0번으로 만들고싶다.
            if (inventoryList.Count < itemIdx)
            {
                itemIdx = 0;
            }

            print(itemIdx);

            //해당인덱스의 아이템을 찾아서
            GameObject item = GameObject.Find(inventoryList[itemIdx]);
            //플레이어의 오른손에 나타나게 하고싶다.
            item.transform.position = rightArmItemSpot.transform.position;

            //오른팔을 parent로 해주고
            item.transform.SetParent(rightArm.transform);

            //충돌 안되게 하고
            item.GetComponent<Rigidbody>().isKinematic = true;
            //중력 적용 꺼주고
            item.GetComponent<Rigidbody>().useGravity = false;



        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            GameObject item = GameObject.Find(inventoryList[itemIdx]);

            item.transform.SetParent(null);

            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().AddForce(Vector3.forward * 500);

            if(item.name.Contains ("Fuel"))
            {
                fuelCount--;
            }

            if (item.name.Contains("Key"))
            {
                keyCount--;
            }

            // 던진아이템을 인벤토리 리스트에서 제거하고싶다.
            inventoryList.Remove(item.name);
            // 아이템 UI에서 던진아이템을 삭제하고싶다.
            GameObject.Find("InvenIcon_Grp").GetComponent<Ui_Icon>().ui_IconList.Remove(item.name);

        }



    }

    // 만약 플레이어한테 아이템이 닿으면
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Item"))
        {
            // 아이템은 인벤토리 맵으로 이동하게 하고싶다.
            hit.gameObject.transform.position = inventoryMap.transform.position;
            // 아이템은 플레이어 인벤토리 리스트로 들어가게 하고싶다.
            inventoryList.Add(hit.gameObject.name);


            //추가된것
            GameObject.Find("InvenIcon_Grp").GetComponent<Ui_Icon>().ui_IconList.Add(hit.gameObject.name);
            //


            if (hit.gameObject.name.Contains("Key"))
            {
                keyCount++;
                keyText.text = "Key : " + keyCount;
            }

            if (hit.gameObject.name.Contains("Fuel"))
            {
                fuelCount++;
                fuelText.text = "Fuel : " + fuelCount;
            }
        }
    }
}
