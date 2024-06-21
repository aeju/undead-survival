using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;

    private Image icon;
    private Text textlevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; // 첫번째 : 자기자신(버튼 배경 이미지) -> 두번째
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textlevel = texts[0];
    }

    private void LateUpdate()
    {
        textlevel.text = "Lv." + (level + 1); // 레벨 텍스트 갱신
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melle: // 여러 개의 case 붙여서 로직 실행 가능
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    // weapon.Init();
                    weapon.Init(data); // 스크립터블 오브젝트 - 매개변수로 받아 활용
                }
                else // 처음 이후의 레벨업 : 데미지, 횟수 계산
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];
                    
                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case ItemData.ItemType.Glove:
                
                break;
            case ItemData.ItemType.Shoe:
                
                break;
            case ItemData.ItemType.Heal:
                
                break;
        }

        level++; // 레벨 값 하나 더하기 

        if (level == data.damages.Length) // 스크립터블 오브젝트에서 작성한 레벨 데이터를 넘기지 않도록!
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
