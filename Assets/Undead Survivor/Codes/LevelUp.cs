using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
