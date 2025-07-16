using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator playerAnimator;
    private bool isAttacking = false; // 공격 중 여부

    [Header("이펙트")]
    public GameObject swordEffectPrefab; // 검기 이펙트 프리팹
    public Transform effectSpawnPoint;   // 이펙트 생성 위치

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        
        {
            Debug.Log("Attack");
            playerAnimator.SetTrigger("Attack");
            
            isAttacking = true; // 공격 중으로 설정
        }
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            playerAnimator.SetBool("Attack1",true);
            isAttacking = true; // 공격 중으로 설정
        }

    }

    // 애니메이션 이벤트에서 호출 (Attack 애니메이션 중간에 이벤트 추가)
    public void SpawnSwordEffect()
    {
        if (swordEffectPrefab != null && effectSpawnPoint != null)
        {
            // 이펙트 프리팹을 생성하고, 1초 뒤 자동 삭제
            GameObject effect = Instantiate(
                swordEffectPrefab,
                effectSpawnPoint.position,
                effectSpawnPoint.rotation,
                effectSpawnPoint // 부모로 설정
            );
          //  Destroy(effect, 1.0f); // 1초 뒤 삭제 (필요에 따라 시간 조절)
        }
    }

    // 애니메이션 이벤트에서 호출 (Attack 애니메이션 끝에 이벤트 추가)
    public void OnAttackEnd()
    {
        isAttacking = false; // 공격 종료
        playerAnimator.SetBool("Attack1", false);
    }
}