using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 정적으로 사용하겠다는 키워드, 바로 메모리에 얹음 (= 즉시 클래스에서 부를 수 있음)

    public float gameTime; // 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 게임 시간
    
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        instance = this; // 자기자신 집어 넣음
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = 0;
        }
    }
}
