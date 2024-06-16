using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameManager.instance.pool.Get(1); // 게임 매니저의 인스턴스에 접근, 풀링의 함수 호출
        }
    }
}
