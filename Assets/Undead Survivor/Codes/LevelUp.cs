using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show() // 보이기 : 레벨업 
    {
        rect.localScale = Vector3.one;
    }
    
    public void Hide() // 숨기기 :
    {
        rect.localScale = Vector3.zero;
    }
}
