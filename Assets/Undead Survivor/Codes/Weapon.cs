using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 무기 ID, 프리팹 ID, 데미지, 개수, 속도 변수
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;
    private Player player;

    void Awake()
    {
        // player = GetComponentInParent<Player>(); // 부모의 컴포넌트 가져오기 (nearestTarget : Scanner에게 있음!) 
        player = GameManager.instance.player;
    }
    
    void Update() // 무기마다 로직 실행
    {
        if (!GameManager.instance.isLive)
            return;
        
        switch (id) 
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // z축 회전
                break;
            default:
                timer += Time.deltaTime; // 빼먹음 -> 생성되지 않음 
                
                if (timer > speed) // speed보다 커지면 
                {
                    timer = 0f; // 1) 초기화
                    Fire(); // 2) 발사 
                }
                break;
        }
        
        // .. Test Code ..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 5);
        }
    }

    public void LevelUp(float damage, int count) // 레벨업 기능
    {
        // this.damage = damage;
        this.damage = damage + Character.Damage;
        this.count += count;
        
        if (id == 0)
            Batch(); // 속성 변경과 동시에, 배치 호출
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // 레벨업에 대한 기어 데미지도 올려달라 
    }

    public void Init(ItemData data) // 무기마다 로직 실행
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform; // 부모 오브젝트 : 플레이어로 지정
        transform.localPosition = Vector3.zero; // local Position(지역 위치) : 원점 
        
        // Property Set (각종 무기 속성 변수 : 스크립터블 오브젝트 데이터로 초기화)
        id = data.itemId;
        // damage = data.baseDamage;
        damage = data.baseDamage * Character.Damage;
        // count = data.baseCount;
        count = data.baseCount + Character.Count;

        // 데이터 : 프리팹(인덱스 x) -> prefabId : 풀링 매니저 변수에서 찾아서 초기화
        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index; 
                break;
            }
        }
        
        switch (id) 
        {
            case 0: // 근거리 무기 
                // speed = 150; // - : 반시계 방향
                speed = 150 * Character.WeaponSpeed; 
                Batch(); 
                break;
            default: // 원거리 무기 
                // speed = 0.3f; // 연사 속도 (적을 수록 많이 발사)
                speed = 0.5f * Character.WeaponRate; 
                break;
        }
        
        // Hand Set
        Hand hand = player.hands[(int)data.itemType]; // enum -> 정수 변환
        hand.spriter.sprite = data.hand; // 스프라이트 적용
        hand.gameObject.SetActive(true); // 활성화 
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // 특정 함수 호출을 모든 자식에게 방송, 두 번째 인자 - 꼭 리시버가 필요하진 않다 
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
            
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 근접 공격에 사용했던 초기화 함수 호출 수정 
        }
    }
 
    void Fire()
    {
        if (!player.scanner.nearestTarget) // 저장된 목표가 없으면 넘어가는 조건 로직 
            return;
        
        // 총알 나아가고자 하는 방향 계산
        Vector3 targetPos = player.scanner.nearestTarget.position; // 위치
        Vector3 dir = targetPos - transform.position; // 방향 (크기가 포함된 방향 : 목표 위치 - 나의 위치)
        dir = dir.normalized; // normalized : 현재 벡터의 방향은 유지하고 크기가 1로 변환된 속성 
        
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform; // 생성 : 불렛 0과 동일
        bullet.position = transform.position; // 위치 : 플레이어 위치
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 회전 (FromToRotation : 지정된 축을 중심으로 목표를 향해 회전)
        bullet.GetComponent<Bullet>().Init(damage, count, dir); // 원거리 공격 초기화 함수 호출 (관통력, dir)
        
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
