using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUp : MonoBehaviour
{
    private RectTransform rect;
    private Item[] items;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true); // 비활성화 존재 
    }

    public void Show() // 보이기 : 레벨업 
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop(); // 레벨업 창 나타날 때 : 멈추기
    }
    
    public void Hide() // 숨기기 : 아이템 버튼 클릭 
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume(); // 레벨업 창 사라질 때 : 멈추기
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        
        // 2. 그 중에서 랜덤 3개 아이템 활성화
        int[] ran = new int[3]; // 랜덤으로 활성화 할 아이템 - 인덱스 3개 담을 배열 
        while (true)
        {
            ran[0] = Random.Range(0, items.Length); // 3개 데이터 모두, 임의의 수 생성 
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]) // 서로 비교 -> 모두 같지 않으면 반복문 빠져나감
                break;
        }
 
        for (int index = 0; index < ran.Length; index++) // 3개의 아이템 버튼 활성화
        {
            Item ranItem = items[ran[index]];
            
            // 3. 만렙 아이템의 경우는 소비아이템으로 대체 
            if (ranItem.level == ranItem.data.damages.Length)
            {
                // items[Random.Range(4, 7)].gameObject.SetActive(true); // 소비 아이템 더 많다면
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
