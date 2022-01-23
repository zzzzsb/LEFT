﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // 목표가 될 트랜스폼 컴포턴트
    public Transform target;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라의 위치를 목표 트랜스폼의 위치(플레이어 머리(눈)에 일치시킨다.
        transform.position = target.position;
    }
}
