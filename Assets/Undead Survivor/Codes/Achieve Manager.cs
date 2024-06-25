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

        foreach (var achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);
            
            /*
            PlayerPrefs.SetInt("unlockPotato", 0); // 저장
            PlayerPrefs.SetInt("unlockBean", 0); // 저장
            */
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
}
