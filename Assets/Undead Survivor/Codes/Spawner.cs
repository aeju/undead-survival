using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] SpawnData;
    
    private int level; // 레벨 
    private float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // GetComponentsInChildern : 자기 자신(Spawner 오브젝트)도 포함 -> 가장 아래 Random.Range에서 0대신 1!
    }
    
    void Update()
    {
        timer += Time.deltaTime; // 타이머 변수 : deltaTime 계속 더하기 
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f); // 적절한 숫자로 나눠 시간에 맞춰 레벨이 올라가도록 (Int형으로 변환) 
        // FloorToInt : 소수점 아래 버리고 Int형으로 변환
        // CeilToInt : 소수점 아래 올리고 Int형으로 변환
        
        //if (timer > 0.2f) // 타이머가 일정 시간 값에 도달하면 소환
        if (timer > (level == 0 ? 0.5f : 0.2f)) // 레벨을 활용해 소환 타이밍 변경
        {
            timer = 0;
            // GameManager.instance.pool.Get(1);
            Spawn(); // 소환 함수 새로 작성
        }
    }

    void Spawn()
    {
        // GameManager.instance.pool.Get(Random.Range(0, 2)); 
        // GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2)); // 변수에 담는 이유 : 한 번 더 이용할 것이기 때문 (아래줄 -> 만들어둔 소환 위치 중 하나로 배치되도록)
        GameObject enemy = GameManager.instance.pool.Get(level); // 풀링에서 가져오는 함수에도 레벨 적용  
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 자식 오브젝트에서만 선택되도록, 랜덤 시작 -> 1부터 
    }
}

// 속성 : 스프라이트 타입, 소환시간, 체력, 속도
[System.Serializable] // 직렬화
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
