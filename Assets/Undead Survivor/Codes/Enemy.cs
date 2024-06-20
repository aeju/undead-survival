using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed = 2.5f;
    public float health; // 현재 체력
    public float maxHealth; // 최대 체력
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target; // 목표

    private bool isLive; // 생존 여부

    private Rigidbody2D rigid;
    private Collider2D coll;
    private Animator anim;
    private SpriteRenderer spriter;
    private WaitForFixedUpdate wait;
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>(); // 빼먹었더니, coll.enabled = true; 가 실행되지 않음 ㅠ 
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    // 물리적 이동 : 몬스터가 살아있는 동안에만 
    void FixedUpdate()
    {
        // if (!isLive) 
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) // Hit 애니메이션 동안 멈추기
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
        isLive = true;
        // 재활용 -> 되돌리기 
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data) // 초기 속성 적용, 매개변수 : 소환데이터
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // 무기랑 충돌할 때만, 살아있을 때만 (사망 로직 연속 실행 방지)
            return;

        health -= collision.GetComponent<Bullet>().damage; // Bullet 컴포넌트로 접근하여 데미지를 가져와 피격 계산
        
        // 실행 x : KnockBack(); 
        StartCoroutine(KnockBack()); // 동일 : StartCoroutine("KnockBack"); 
        

        // 로직 분리 조건 : 남은 체력 
        if (health > 0) // 피격
        {
            anim.SetTrigger("Hit");
        }
        else // 사망
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++; // 킬수 증가
            GameManager.instance.GetExp(); // 경험치 함수 호출
            // Dead(); 애니메이션 이벤트에서 실행      
        }
    }

    IEnumerator KnockBack()
    {
        // 동일 : yield return null; // 1프레임 쉬기 
        yield return wait; // 하나의 물리 프레임 딜레이 
        
        // 플레이어 위치, 나의 위치 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos; // 플레이어 기준의 반대 방향 : 현재 위치 - 플레이어 위치 
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // dirVec(크기 포함).normalized = 크기 1 (=방향만 가진 벡터)
    }
    
    void Dead()
    {
        gameObject.SetActive(false); // 오브젝트 비활성화
    }
}
