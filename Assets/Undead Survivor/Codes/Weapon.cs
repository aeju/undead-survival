using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기 ID, 프리팹 ID, 데미지, 개수, 속도 변수
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    void Start()
    {
        Init();
    }
    
    void Update() // 무기마다 로직 실행
    {
        switch (id) 
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // z축 회전
                break;
            default:
                break;
        }
        
        // .. Test Code ..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count) // 레벨업 기능
    {
        this.damage = damage;
        this.count += count;
        
        if (id == 0)
            Batch(); // 속성 변경과 동시에, 배치 호출
    }

    public void Init() // 무기마다 로직 실행
    {
        switch (id) 
        {
            case 0:
                speed = 150; // - : 반시계 방향
                Batch(); 
                break;
            default:
                break;
        }
    }

    void Batch() // count 수마다 배치 
    {
        for (int index = 0; index < count; index++) // for문으로 count만큼 풀링에서 가져오기 
        {
            Transform bullet;

            if (index < transform.childCount) // 자식 오브젝트 개수 확인 
            {
                bullet = transform.GetChild(index); // index가 아직 childCount 범위 내라면, GetChild 함수로 가져오기 
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform; // 기존 오브젝트 먼저 활용하고, 모자란 것은 풀링에서 가져오기 
                bullet.parent = transform; // 부모 변경 (기존 : 풀매니저)
            }
            
            // 위치, 회전 초기화 
            bullet.localPosition = Vector3.zero; 
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count; // z축 방향 
            bullet.Rotate(rotVec); // 계산된 각도 적용 (회전)
            bullet.Translate(bullet.up * 1.5f, Space.World); // 자신의 위쪽으로 이동, 이동 방향 : Space.World 기준
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per. (무한으로 관통) 
        }
    }
}
