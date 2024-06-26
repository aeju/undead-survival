using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    
    // .. 풀 담당을 하는 리스트들
    private List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        // for (반복문) : 시작문; 조건문; 증감 -> 반복문을 통해 모든 오브젝트 풀 리스트 초기화 
        for (int index = 0; index < pools.Length; index++) // 프리팹 배열 길이 활용
        {
            pools[index] = new List<GameObject>();
        }
    }

    // 게임 오브젝트를 반환하는 함수 선언
    public GameObject Get(int index) // 매개변수 : 가져올 오브젝트 종류 결정 
    {
        GameObject select = null;
        
        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임오브젝트 접근
        foreach (GameObject item in pools[index]) // 배열, 리스트 데이터를 순차적으로 접근
        {
            if (!item.activeSelf) // 오브젝트가 비활성화(대기 상태)인지 확인
            {
                // ... 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true); // 비활성화 오브젝트 찾으면, SetActive 함수로 활성화
                break;
            }
        }
            
        // ...  못 찾았으면? -> 생성 로직
        if (!select)
        {
            // ... 새롭게 생성하고 select 변수에 할당   
            select = Instantiate(prefabs[index], transform); // Instantiate : 원본 오브젝트를 복제하여 장면에 생성
            pools[index].Add(select); // 생성된 오브젝트, 해당 오브젝트 풀 리스트에 추가
        }
        return select;
    }
}
