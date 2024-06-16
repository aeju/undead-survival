using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    private float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // GetComponentsInChildern : 자기 자신(Spawner 오브젝트)도 포함 -> 가장 아래 Random.Range에서 0대신 1!
    }
    
    void Update()
    {
        timer += Time.deltaTime; // 타이머 변수 : deltaTime 계속 더하기 

        if (timer > 0.2f) // 타이머가 일정 시간 값에 도달하면 소환
        {
            timer = 0;
            // GameManager.instance.pool.Get(1);
            Spawn(); // 소환 함수 새로 작성
        }
    }

    void Spawn()
    {
        //GameManager.instance.pool.Get(Random.Range(0, 2)); 
        
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2)); // 변수에 담는 이유 : 한 번 더 이용할 것이기 때문 (아래줄 -> 만들어둔 소환 위치 중 하나로 배치되도록)
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 자식 오브젝트에서만 선택되도록, 랜덤 시작 -> 1부터 
    }
}
