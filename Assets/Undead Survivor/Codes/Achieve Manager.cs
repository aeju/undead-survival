using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    
    enum Achieve { unlockPotato, unlockBean }
    private Achieve[] achieves;

    private void Awake()
    {
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve)); // 주어진 열거형의 데이터 모두 가져옴 

        if (!PlayerPrefs.HasKey("MyData")) // 데이터 유무 체크 
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1); // 저장 : key와 연결된 int형 데이터 저장

        foreach (Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter() // 캐릭터 버튼 해금 
    {
        for (int index = 0; index < lockCharacter.Length; index++) // 인덱스에 해당하는 업적 이름 가져오기 
        {
            string acheiveName = achieves[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(acheiveName) == 1; // 저장된 업적 상태 가져와서, 버튼 활성화에 적용 
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    private void LateUpdate() // 모든 업적 확인을 위한 반복문 
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }

    void CheckAchieve(Achieve achieve) // 업적 달성 
    {
        bool isAchieve = false;

        switch (achieve) // 각 업적 달성 조건 
        {
            case Achieve.unlockPotato:
                // if (GameManager.instance.isLive) // 게임 성공 시 자동으로 달성 방지 
                {
                    isAchieve = GameManager.instance.kill >= 10;
                }
                break;
            case Achieve.unlockBean:
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime; // 이걸론 왜 안 되지...? 일단 넘어가기 
                // isAchieve = GameManager.instance.kill >= 20;
                break;
        }

        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0) // 해당 업적이 처음 달성됨 
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);
        }
    }
}
