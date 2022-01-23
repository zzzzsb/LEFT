using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemName : MonoBehaviour
{
    

    public int itemIdx;
    public string item;


    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        item = GameObject.Find("Player").GetComponent<PlayerInventory>().inventoryList[itemIdx];

        

        // 아이템 리스트에 들어온것의 0번 인덱스 이름을 출력해줄것이다
        text.text = item;
    }
}
