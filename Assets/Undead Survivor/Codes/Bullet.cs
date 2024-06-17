using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    public void Init(float damage, int per)
    {
        this.damage = damage; // 왼쪽 : public damage, 오른쪽 : 매개변수
        this.per = per;
    }
}
