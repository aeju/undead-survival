using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; // 범위
    public LayerMask targetLayer; // 레이어
    public RaycastHit2D[] targets; // 스캔 결과 배열
    public Transform nearestTarget; // 가장 가까운 목표

    void FixedUpdate()
    {
        // 1. 캐스팅 시작 위치 2. 원의 반지름 3. 캐스팅 방향 4. 캐스팅 길이 5. 대상 레이어
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer); // CircleCastAll : 원형의 캐스트를 쏘고 모든 결과를 반환
        nearestTarget = GetNearest(); // 지속적으로 가장 가까운 목표 변수 업데이트 
    }

    Transform GetNearest() // 가장 가까운 몬스터를 찾는 함수
    {
        Transform result = null;
        float diff = 100; // 거리

        foreach (RaycastHit2D target in targets) // foreach 문으로 캐스팅 결과 오브젝트를 하나씩 접근
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff) // 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 교체
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        
        return result;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
