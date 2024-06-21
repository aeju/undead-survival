using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type; // 장비 타입
    public float rate; // 장비 수치 

    public void Init(ItemData data) // 초기화 
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;
        
        // Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear(); // 적용 함수 호출 : 장비가 새롭게 추가될 때  
    }
    
    public void LevelUp(float rate) 
    {
        this.rate = rate;
        ApplyGear(); // 적용 함수 호출 : 레벨업 할 때  
    }
    
    void ApplyGear() // 타입에 따라 적절하게 로직 적용 시켜줌
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp() // 연사업 : 장갑 - 연사력 올리는 
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (var weapon in weapons) // 하나씩 순회하면서, 타입에 따라 속도 올리기 
        {
            switch (weapon.id) 
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp() // 이속업 : 신발 - 이동 속도 올리는 
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
