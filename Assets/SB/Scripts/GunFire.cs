using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 발사 버튼을 누르면 총에서 총알을 발사하고 싶다.
// 필요속성 : 총알공장, 총구위치
public class GunFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public Transform firePosition;

    public GameObject effectFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 사용자가 발사 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            // 시선을 이용해서 총을 발사하고 싶다.
            // 시선을 만든다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 시선을 던진다.
            RaycastHit hitInfo = new RaycastHit();

            // 시선이 던져져서 충돌했다면(총을 쏴서 맞았다면)
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 부딪힌 지점에 이펙트를 주고 싶다.
                // 파편 이펙트 공장에서 파편을 생성하여
                GameObject effect = Instantiate(effectFactory);
                // 부딪힌 지점에 위치하게 하고싶다.
                effect.transform.position = hitInfo.point;
                // 파편의 방향을 부딪힌 지점의 Normal 방향으로 설정하고 싶다.
                effect.transform.forward = hitInfo.normal;
            }

            // 그리고 시선이 닿은 애의 레이어가 에너미라면
            if (Physics.Raycast(ray, out hitInfo, 5, 1<< LayerMask.NameToLayer("Enemy")))
            {
                // 부딪힌 지점에 이펙트를 주고 싶다.
                // 파편 이펙트 공장에서 파편을 생성하여
                GameObject effect = Instantiate(effectFactory);
                // 부딪힌 지점에 위치하게 하고싶다.
                effect.transform.position = hitInfo.point;
                // 파편의 방향을 부딪힌 지점의 Normal 방향으로 설정하고 싶다.
                effect.transform.forward = hitInfo.normal;

                /////////////// 에너미 스턴하는 코드 넣기!!
                /*
                GameObject stunEnemy = GameObject.Find("Enemy");
                stunEnemy.GetComponent<EnemyMove>().Stun();

                GameObject.Find("Enemy").GetComponent<EnemyMove>().state =.State.Stun;
                */
                hitInfo.transform.gameObject.GetComponent<EnemyMove>().Stun();

            }
        }
    }

    private void BulletFire()
    {
        // 총알공장에서 총알을 생성하여
        GameObject bullet = Instantiate(bulletFactory);
        // 총알을 발사하고 싶다.(총알을 총구에 위치시킨다)
        bullet.transform.position = firePosition.position;
        // 총알의 방향을 설정한다.
        bullet.transform.forward = firePosition.forward;
    }
}
