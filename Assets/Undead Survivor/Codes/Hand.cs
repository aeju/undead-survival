using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    private SpriteRenderer player;

    // 오른손
    private Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    private Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    // 왼손
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    private Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);
    
    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX; // 플레이어 반전 상태 : 지역변수로 저장

        if (isLeft) // 근접 무기 
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse; // Y축 반전
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else // 원거리 무기 
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;  // X축 반전
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
