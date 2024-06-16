using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.5f;
    public Rigidbody2D target; // 목표

    private bool isLive = true; // 생존 여부

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // 물리적 이동 : 몬스터가 살아있는 동안에만 
    void FixedUpdate()
    {
        if (!isLive)
            return;
        
        Vector2 dirVec = target.position - rigid.position; // 위치 차이 = 타겟 위치 - 나의 위치, 방향 = 위치 차이의 정규화 (Normalized)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 프레임 영향 x -> fixedDeltaTime
        rigid.MovePosition(rigid.position + nextVec); // 현재 위치 + 다음에 가야할 위치(= 플레이어의 키입력 값을 더한 이동)
        rigid.velocity = Vector2.zero; // 속도 제거 = 물리 속도가 이동에 영향을 주지 않도록 
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        
        // 목표의 X축 값과 자신의 X축 값 비교, 작으면 true (자신보다 왼쪽에 있으면 뒤집기)
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // 프리팹은 장면의 오브젝트에 접근 불가능 -> 생성되면서 변수 초기화해주는 방법 사용
    // OnEnable : 스크립트가 활성화 될 때, 호출되는 이벤트 함수
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
}
