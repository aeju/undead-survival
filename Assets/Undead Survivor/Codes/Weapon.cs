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
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform; // 가져온 오브젝트의 Transform을 지역변수로 저장
            bullet.parent = transform; // 부모 변경 (기존 : 풀매니저)

            Vector3 rotVec = Vector3.forward * 360 * index / count; // z축 방향 
            bullet.Rotate(rotVec); // 계산된 각도 적용 (회전)
            
            bullet.Translate(bullet.up * 1.5f, Space.World); // 자신의 위쪽으로 이동, 이동 방향 : Space.World 기준
            
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per. (무한으로 관통) 
        }
    }
}
