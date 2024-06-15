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
        
        Debug.Log(pools.Length);
    }
}
