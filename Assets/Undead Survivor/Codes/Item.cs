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
    public Gear gear;

    private Image icon;
    private Text textLevel; // 레벨
    private Text textName; // 이름
    private Text textDesc; // 설명

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; // 첫번째 : 자기자신(버튼 배경 이미지) -> 두번째
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName; // 처음에 넣어주고, 수정 x 
    } 

    private void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1); // 레벨 텍스트 갱신

        switch (data.itemType) // 아이템 타입에 따라 로직 분리 (무기 : 설명 2줄)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]); // 데미지 % 상승 보여줄 땐, 100 곱하기 
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    /*
    private void LateUpdate()
    {
        textLevel.text = "Lv." + (level + 1); // 레벨 텍스트 갱신
    }
    */

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee: // 여러 개의 case 붙여서 로직 실행 가능
            case ItemData.ItemType.Range:
                if (level == 0) // 최초 레벨업 : 게임 오브젝트 생성 
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
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
                
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        // level++; // 힐 (횟수 제한 x) 제외

        if (level == data.damages.Length) // 스크립터블 오브젝트에서 작성한 레벨 데이터를 넘기지 않도록!
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
