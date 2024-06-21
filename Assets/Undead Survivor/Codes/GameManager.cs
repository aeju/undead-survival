using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 정적으로 사용하겠다는 키워드, 바로 메모리에 얹음 (= 즉시 클래스에서 부를 수 있음)
    
    [Header("# Game Control")]
    public float gameTime; // 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 게임 시간
    
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level; // 레벨
    public int kill; // 킬수
    public int exp; // 경험치
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; // 각 레벨의 필요경험치 보관 배열
    
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp; 
    
    void Awake()
    {
        instance = this; // 자기자신 집어 넣음
    }

    private void Start()
    {
        health = maxHealth;
        
        // 임시 스크립트 (첫번째 캐릭터 선택)
        uiLevelUp.Select(0);
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = 0;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level]) // 레벨 업 : 필요 경험치에 도달하면
        {
            level++;
            exp = 0;
            uiLevelUp.Show(); // 아이템 창 보여주기 
        }
    }
}
