using UnityEngine;

public class Suriken_v1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    private Rigidbody2D rb;

    private float damage = 1f;       // 실제 데미지
    private bool isCritical = false; // 크리티컬 여부


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        SetDamage();
    }

    // 외부에서 사용하는 물체의 발사 각도 설정 함수
    public void ThrowSuriken(Vector3 startPos, Vector3 targetPos) {
        var dir = (targetPos - startPos).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    // 데미지 설정 기능
    private void SetDamage() {
        var playerAttackComp = PlayerController.Instance?.GetCharacter()?.GetPlayerApplyDamageClass();
        if (playerAttackComp == null) return;
        
        damage = playerAttackComp.GetAttackBaseDamage();  // 베이스 데미지
        isCritical = playerAttackComp.IsCriticalAttack(); // 크리티컬 여부

        // 수리검 대미지 적용
        // 베이스 공격력 * 1.3f
        damage *= 1.3f; // 실제 적용할 데미지
    }

    // 피격시
    private void OnTriggerEnter2D(Collider2D collision) {
        // 에너미 피격의 경우
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            var enemy = collision.gameObject.GetComponent<EnemyHitBox>();

            // 데미지 적용
            enemy.TakeDamage(damage, enemy.transform.position, isCritical);
            Destroy(gameObject);
        }
        
        // 벽에 도달하면 파괴
        else if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
