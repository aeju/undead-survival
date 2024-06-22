using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 정적으로 사용하겠다는 키워드, 바로 메모리에 얹음 (= 즉시 클래스에서 부를 수 있음)

    [Header("# Game Control")] 
    public bool isLive;
    public float gameTime; // 게임 시간
    public float maxGameTime = 2 * 10f; // 최대 게임 시간
    
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level; // 레벨
    public int kill; // 킬수
    public int exp; // 경험치
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 }; // 각 레벨의 필요경험치 보관 배열
    
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public GameObject uiResult;
    
    void Awake()
    {
        instance = this; // 자기자신 집어 넣음
    }

    public void GameStart() // 게임 시작 버튼에 연결
    {
        health = maxHealth;
        uiLevelUp.Select(0); // 임시 스크립트 (첫번째 캐릭터 선택)
        isLive = true;
    }

    public void GameOver()
    {
        // Stop(); // 묘비로 변하기 전, 멈춤 -> 약간의 딜레이 필요 (코루틴 사용)
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        
        yield return new WaitForSeconds(0.5f);
        
        uiResult.SetActive(true);
        Stop();
    }
    
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive)
            return;
        
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = 0;
        }
    }

    public void GetExp()
    {
        exp++;

        // 필요 경험치에 도달 -> 레벨 업 
        // if (exp == nextExp[level]) 
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)]) // 최고 경험치 그대로 사용하도록 
        {
            level++;
            exp = 0;
            uiLevelUp.Show(); // 아이템 창 보여주기 
        }
    }

    public void Stop() // 시간 정지
    {
        isLive = false;
        Time.timeScale = 0; // 시간속도, 멈춤
    }
    
    public void Resume() // 작동 
    {
        isLive = true;
        Time.timeScale = 1; // 재생 
    }
}
