using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private Collider2D coll; // Collider2D : 기본 도형의 모든 콜라이더2D 포함

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    
    // 트리거에서 나갔을 때 발생
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) // 매개변수 상대방 콜라이더의 태그를 조건으로 (Area가 아니면 = 필터 역할)
            return; // return 키워드 만나면, 코드 아래로 더 이상 실행하지 않고 함수 탈출

        // 거리 구하기 위해, 플레이어 위치 / 타일맵 위치 미리 저장
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        
        /*
        // 플레이어 위치 - 타일맵 위치 계산, 거리 구하기
        float diffX = Mathf.Abs(playerPos.x - myPos.x); // 절댓값 함수 (음수 -> 양수)
        float diffY = Mathf.Abs(playerPos.y - myPos.y); // 절댓값 함수 (음수 -> 양수)
        
        // 플레이어 이동 방향 저장 변수
        // Vector3 playrDir = GameManager.instance.player.inputVec;
        // 대각선일 때는, Normalized에 의해 1보다 작은 값이 되어버림
        float dirX = playrDir.x < 0 ? -1 : 1; // 3항 연산자 (조건) ? (true일 때 값) : (false일 때 값)
        float dirY = playrDir.y < 0 ? -1 : 1;
        */
        
        switch (transform.tag) // switch ~ case : 값의 상태에 따라 로직을 나눠주는 키워드
        {
            case "Ground":
                // 1) 방향 먼저 구하기 (Mathf.Abs 제외)  
                float diffX = playerPos.x - myPos.x; 
                float diffY = playerPos.y - myPos.y; 
                float dirX = diffX < 0 ? -1 : 1; 
                float dirY = diffY < 0 ? -1 : 1;
                // 2) 거리 구하기  
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                
                if (diffX > diffY) // 두 오브젝트의 거리 차이에서, X축이 Y축보다 크면 수평 이동
                {
                    transform.Translate(Vector3.right * dirX * 40); // Translate : 지정된 값 만큼 현재 위치에서 이동 (좌표 이동x, 이동할 양o) // dirX : 방향(+ / -)  
                }
                else if (diffX < diffY) // 수직 이동
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled) // 콜라이더가 활성화 되어있는지 (살아있을 때만)
                {
                    /*
                    // 플레이어의 이동 방향에 따라 맞은 편에서 등장하도록 이동 (카메라가 안 보이는 먼 곳에서 이동시키기 -> 한 맵의 크기만큼)
                    transform.Translate(playrDir * 20
                                        + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f)); // 랜덤한 위치에서 등장하도록 벡터 더하기
                    */
                    
                    // 두 오브젝트 거리(플레이어 - 몬스터) 그대로 활용
                    Vector3 dist = playerPos - myPos;
                    
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0); // 랜덤 벡터 더하여 퍼져있는 몬스터 재배치 만들기 
                    // transform.Translate(dist);
                    transform.Translate(ran + dist * 2); // 이동 거리만큼 한 번 더 가면 = 플레이어 앞 쪽으로 미리 간 느낌 
                }
                break;
        }
    }
}
 