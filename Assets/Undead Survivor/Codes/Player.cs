using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 3;

    private Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // 초기화
    }
    
    void Update()
    {
        //inputVec.x = Input.GetAxis("Horizontal"); // 보정 o
        //inputVec.y = Input.GetAxis("Vertical");
        
        inputVec.x = Input.GetAxisRaw("Horizontal"); // 더 명확한 컨트롤
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출
    {
        // 물리이동 세 가지
        /*
        // 1. 힘을 준다
        rigid.AddForce(inputVec);
        
        // 2. 속도 제어 
        rigid.velocity = inputVec; 
        */
        
        // 3. 위치 이동 (다른 프레임 환경에서도 이동 거리 같도록)
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; // fixedDeltaTime : 물리 프레임 하나 소비 시간 (FixedUpdate)
        //rigid.MovePosition(rigid.position + inputVec); // 위치 이동 -> 현재 위치도 더해줘야 (inputVec = 방향)
        rigid.MovePosition(rigid.position + nextVec); // 
    }
}
