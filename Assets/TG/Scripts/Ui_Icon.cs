using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Icon : MonoBehaviour
{
    public List<string> ui_IconList;

    public Texture Flashbang;
    public Texture Fuel;
    public Texture Clock;
    public Texture Key;

    RawImage rawImage0;
    RawImage rawImage1;
    RawImage rawImage2;
    RawImage rawImage3;
    RawImage rawImage4;
    RawImage rawImage5;



    // Start is called before the first frame update
    void Start()
    {
        

        rawImage0 = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RawImage>();
        rawImage1 = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RawImage>();
        rawImage2 = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<RawImage>();
        rawImage3 = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<RawImage>();
        rawImage4 = transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<RawImage>();
        rawImage5 = transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<RawImage>();

        rawImage0.enabled = false;
        rawImage1.enabled = false;
        rawImage2.enabled = false;
        rawImage3.enabled = false;
        rawImage4.enabled = false;
        rawImage5.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // 기존에 만들어둔 리스트에서 갖고와 아이콘 띄워주고싶다.
        // 1. 리스트를 갖고오자
        for (int i = 0; i < ui_IconList.Count; i++)
        {
            #region 0번리스트
            //2. 만약 리스트 0번의 이름이 ~~라면
            if (ui_IconList[0] == "Fuel")
            {
                //3. getchild해서 1번째 child의 child의 child의  image를 가져와 ~~의 아이콘으로 바꿔줘라.
                rawImage0.enabled = true;
                rawImage0.texture = Fuel;
            }
            else if (ui_IconList[0] == "Flashbang")
            {
                rawImage0.enabled = true;
                rawImage0.texture = Flashbang;
            }
            else if (ui_IconList[0] == "Clock")
            {
                rawImage0.enabled = true;
                rawImage0.texture = Clock;
            }
            else if (ui_IconList[0] == "Key")
            {
                rawImage0.enabled = true;
                rawImage0.texture = Key;
            }
            else if (ui_IconList[0] == "Key")
            {
                rawImage0.enabled = true;
                rawImage0.texture = Key;
            }
            else if (ui_IconList[0] == null)
            {
                rawImage0.enabled = false;
            }
            #endregion
            #region 1번리스트
            //2. 만약 리스트 0번의 이름이 ~~라면
            if (ui_IconList[1] == "Fuel")
            {
                rawImage1.enabled = true;
                //3. getchild해서 1번째 child의 child의 child의  image를 가져와 ~~의 아이콘으로 바꿔줘라.
                rawImage1.texture = Fuel;

            }
            else if (ui_IconList[1] == "Flashbang")
            {
                rawImage1.enabled = true;
                rawImage1.texture = Flashbang;
            }
            else if (ui_IconList[1] == "Clock")
            {
                rawImage1.enabled = true;
                rawImage1.texture = Clock;
            }
            else if (ui_IconList[1] == "Key")
            {
                rawImage1.enabled = true;
                rawImage1.texture = Key;
            }
            else if (ui_IconList[1] == null)
            {
                rawImage1.enabled = false;
            }
            #endregion
            #region 2번리스트
            //2. 만약 리스트 0번의 이름이 ~~라면
            if (ui_IconList[2] == "Fuel")
            {
                rawImage2.enabled = true;
                //3. getchild해서 1번째 child의 child의 child의  image를 가져와 ~~의 아이콘으로 바꿔줘라.
                rawImage2.texture = Fuel;
            }
            else if (ui_IconList[2] == "Flashbang")
            {
                rawImage2.enabled = true;
                rawImage2.texture = Flashbang;
            }
            else if (ui_IconList[2] == "Clock")
            {
                rawImage2.enabled = true;
                rawImage2.texture = Clock;
            }
            else if (ui_IconList[2] == "Key")
            {
                rawImage2.enabled = true;
                rawImage2.texture = Key;
            }
            else if (ui_IconList[2] == null)
            {
                rawImage2.enabled = false;
            }
            #endregion
            #region 3번리스트
            //2. 만약 리스트 0번의 이름이 ~~라면
            if (ui_IconList[3] == "Fuel")
            {
                rawImage3.enabled = true;
                //3. getchild해서 1번째 child의 child의 child의  image를 가져와 ~~의 아이콘으로 바꿔줘라.
                rawImage3.texture = Fuel;
            }
            else if (ui_IconList[3] == "Flashbang")
            {
                rawImage3.enabled = true;
                rawImage3.texture = Flashbang;
            }
            else if (ui_IconList[3] == "Clock")
            {
                rawImage3.enabled = true;
                rawImage3.texture = Clock;
            }
            else if (ui_IconList[3] == "Key")
            {
                rawImage3.enabled = true;
                rawImage3.texture = Key;
            }
            else if (ui_IconList[3] == null)
            {
                rawImage3.enabled = false;
            }
            #endregion
            #region 4번리스트
            //2. 만약 리스트 0번의 이름이 ~~라면
            if (ui_IconList[4] == "Fuel")
            {
                rawImage4.enabled = true;
                //3. getchild해서 1번째 child의 child의 child의  image를 가져와 ~~의 아이콘으로 바꿔줘라.
                rawImage4.texture = Fuel;
            }
            else if (ui_IconList[4] == "Flashbang")
            {
                rawImage4.enabled = true;
                rawImage4.texture = Flashbang;
            }
            else if (ui_IconList[4] == "Clock")
            {
                rawImage4.enabled = true;
                rawImage4.texture = Clock;
            }
            else if (ui_IconList[4] == "Key")
            {
                rawImage4.enabled = true;
                rawImage4.texture = Key;
            }
            else if (ui_IconList[4] == null)
            {
                rawImage4.enabled = false;
                //rawImage4.texture = null;
            }
            #endregion
            #region 5번리스트
            //2. 만약 리스트 0번의 이름이 ~~라면
            if (ui_IconList[5] == "Fuel")
            {
                rawImage5.enabled = true;
                //3. getchild해서 1번째 child의 child의 child의  image를 가져와 ~~의 아이콘으로 바꿔줘라.
                rawImage5.texture = Fuel;
            }
            else if (ui_IconList[5] == "Flashbang")
            {
                rawImage5.enabled = true;
                rawImage5.texture = Flashbang;
            }
            else if (ui_IconList[5] == "Clock")
            {
                rawImage5.enabled = true;
                rawImage5.texture = Clock;
            }
            else if (ui_IconList[5] == "Key")
            {
                rawImage5.enabled = true;
                rawImage5.texture = Key;
            }
            else if (ui_IconList[5] == null)
            {
                rawImage5.enabled = false;
                //rawImage5.texture = null;
            }
            #endregion
        }


    }
}
