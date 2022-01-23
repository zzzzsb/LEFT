using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject bloodUI;
    public GameObject InventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        bloodUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        print("죽음");
        //keyText.text = "";
        //fuelText.text = "";
        InventoryUI.SetActive(false);
        // 피 UI 출력
        bloodUI.SetActive(true);
        // 카메라 흔들림 함수 실행
        Camera.main.transform.GetComponent<CameraShake>().ShakeCamera();
        // 게임오버UI 함수 실행
        Invoke("GameOverUI", 3f);
        
        // 죽는다
        // Destroy(gameObject);
       
        
    }



    // 에너미한테 닿으면 죽기
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 에너미한테 닿으면
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            print("죽음");
            /*
            // 피 UI 출력
            bloodUI.SetActive(true);

            // 카메라 흔들림 함수 실행
            Camera.main.transform.GetComponent<CameraShake>().ShakeCamera();


            // 죽는다
            // Destroy(gameObject);
            */
            GetComponent<PlayerSound>().Die();
        }
    }

    public void GameOverUI()
    {
        //Camera.main.transform.GetComponent<CameraShake>().ResetCamera();
        // 피 효과 UI 비활성화
        bloodUI.SetActive(false);
        // 게임오버 UI 활성화
        gameOverUI.SetActive(true);
    }

    

    public void OnClickResume()
    {
        // 씬 재로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
