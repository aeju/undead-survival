using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid; // 물리적으로 힘을 줄 것이기 때문

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    //public void Init(float damage, int per)
    public void Init(float damage, int per, Vector3 dir) // 속도 매개변수 추가
    {
        this.damage = damage; // 왼쪽 : public damage, 오른쪽 : 매개변수
        this.per = per;

        if (per > -1) // 관통이 -1(무한)보다 큰 것 : 속도 적용
        {
            // rigid.velocity = dir;
            rigid.velocity = dir * 15f; // 속력 곱해서, 총알 날아가는 속도 증가시키기 
        }
    }

    void OnTriggerEnter2D(Collider2D collision) // 관통 로직
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--; // 관통 감소

        if (per == -1) // 관통 값이 하나씩 줄어들면서 -1이 되면 -> 비활성화
        {
            rigid.velocity = Vector2.zero; // 미리 물리 속도 초기화
            gameObject.SetActive(false); // 풀링으로 관리
        }
    }
}
