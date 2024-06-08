using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 3;

    private Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // 초기화
    }

    void FixedUpdate() // 물리 연산 프레임마다 호출
    {
        // 위치 이동 (다른 프레임 환경에서도 이동 거리 같도록)
        // Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime; // fixedDeltaTime : 물리 프레임 하나 소비 시간 (FixedUpdate)
        Vector2 nextVec = inputVec  * speed * Time.fixedDeltaTime; // 이미 InputSystem에서 normalized 해줘서, 빼도 o
        rigid.MovePosition(rigid.position + nextVec); // 위치 이동 -> 현재 위치도 더해줘야 (nextVec = 방향)
    }
    
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // 값이 다르기 때문에, = value (x)
    }
    
    /*
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal"); // 더 명확한 컨트롤
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    */
}
