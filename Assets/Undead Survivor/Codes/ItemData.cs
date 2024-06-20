using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melle, Range, Glove, Shoe, Heal }

    [Header("# Main Info")] 
    public ItemType itemType;
    public int itemId; // 아이템 ID
    public string itemName; // 아이템 이름
    public string itemDesc; // 아이템 설명
    public Sprite itemIcon; // 아이템 아이콘

    [Header("# Level Data")] 
    public float baseDamage; // 0레벨 데미지
    public int baseCount; // 근접 : 개수, 원거리 : 관통 
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")] 
    public GameObject projectile; // 투사체
}
