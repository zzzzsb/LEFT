using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGet : MonoBehaviour
{
    //닿은 것들의 tag가 item이라면 inventory로 보내고싶다.
    CharacterController cc = null;

    //인벤토리를 설정해주자
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        // Player 게임오브젝트의 컴포넌트 중 CharacterController 컴포넌트를 얻어오고 싶다.
        cc = GetComponent<CharacterController>();

        //인벤토리를 설정해주자
        GameObject target = GameObject.Find("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.gameObject.name);

        if (hit.collider.gameObject.tag == "Item")
        {
            print("아이템");

            //왜 작동안되는거지
            hit.collider.gameObject.transform.position = target.transform.position;
        }
    }
}
