using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireForPrototype : MonoBehaviour
{
    public GameObject Clockf;
    public GameObject Falshbangf;
    public GameObject Lighterf;
    public GameObject Fuelf;
    public GameObject firePosition;
    public GameObject parent;
    GameObject fireLight = null;


    int fireCount;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //1번 시계
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject clock = Instantiate(Clockf);
            clock.transform.position = firePosition.transform.position;
        }
        //2번 섬광
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject flashbang = Instantiate(Falshbangf);
            flashbang.transform.position = firePosition.transform.position;

        }
        //3번 라이터
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject lighter;
            fireCount += 1;

            if (fireCount == 1)
            {
                lighter = Instantiate(Lighterf);
                lighter.transform.SetParent(gameObject.transform);
                
                lighter.transform.position = firePosition.transform.position;
                lighter.gameObject.GetComponent<Rigidbody>().useGravity = false;
                

            }
            if (fireCount == 2)
            {
                GetComponentInChildren<Rigidbody>().AddForce(Vector3.forward * 300);
                GetComponentInChildren<Rigidbody>().useGravity = true;
            }
            if (fireCount > 2)
            {
                fireCount = 0;
            }
        }
        




        //4번 연료
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject fuel;
            fireCount += 1;

            if (fireCount == 1)
            {
                fuel = Instantiate(Fuelf);
                fuel.transform.SetParent(gameObject.transform);

                fuel.transform.position = firePosition.transform.position;
                fuel.gameObject.GetComponent<Rigidbody>().useGravity = false;


            }
            if (fireCount == 2)
            {
                GetComponentInChildren<Rigidbody>().AddForce(Vector3.forward * 3);
                GetComponentInChildren<Rigidbody>().useGravity = true;
            }
            if (fireCount > 2)
            {
                fireCount = 0;
            }
        }
        print(fireCount);
    }
}
