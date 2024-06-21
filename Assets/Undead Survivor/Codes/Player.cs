using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 3;
    public Scanner scanner;
    public Hand[] hands;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // 초기화
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출 (위치 이동, 다른 프레임 환경에서도 이동 거리 같도록)
    {
        if (!GameManager.instance.isLive)
            return;
        
        // 이미 InputSystem에서 normalize 해줘서, 해줄 필요 x
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime; // fixedDeltaTime : 물리 프레임 하나 소비 시간 
        rigid.MovePosition(rigid.position + nextVec); // 위치 이동 -> 현재 위치도 더해줘야 (nextVec = 방향)
    }
    
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // Vector2 != InputValue (값 형식 다름)
    }

    void LateUpdate() // 다음 프레임으로 넘어가기 직전에 실행
    {
        if (!GameManager.instance.isLive)
            return;
        
        anim.SetFloat("Speed", inputVec.magnitude); // magnitude : 벡터의 순수한 크기 값
        
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0; // 비교 연산자 결과 바로 넣기 (좌측키 = 마이너스 = 왼쪽)
        }
    }
}
