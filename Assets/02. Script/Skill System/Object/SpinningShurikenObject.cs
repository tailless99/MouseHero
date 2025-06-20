using Unity.VisualScripting;
using UnityEngine;

public class SpinningShurikenObject : MonoBehaviour {
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float directionLimit = 10f; // 최대 이동 경로

    private Rigidbody2D rb;

    private float damage = 1f;       // 실제 데미지
    private bool isCritical = false; // 크리티컬 여부
    private bool isReturn = false; // 부메랑처럼 돌아오는 중인지 
    private Vector3 startPoint;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        SetDamage();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ThrowSuriken(transform.position, mousePos);
    }

    private void Update() {
        if (!isReturn) {
            CheckTurningPoint();
        }
    }

    // 반환점을 지났는지 체크
    private void CheckTurningPoint() {
        var distance = Vector3.Distance(startPoint, transform.position); // 움직인 거리 측정

        // 최대 이동 경로보다 더 먼거리를 움직였을 경우
        // 운동 방향 반전
        if(directionLimit <= distance) {
            isReturn = true;
            rb.linearVelocity = rb.linearVelocity * -1;
        }
    }

    // 외부에서 사용하는 물체의 발사 각도 설정 함수
    public void ThrowSuriken(Vector3 startPos, Vector3 targetPos) {
        startPoint = startPos; // 시작 생성 위치 기록
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
        damage *= 3f; // 실제 적용할 데미지
    }

    // 피격시
    private void OnTriggerEnter2D(Collider2D collision) {
        // 에너미 피격의 경우
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            var enemy = collision.gameObject.GetComponent<EnemyHitBox>();

            // 데미지 적용
            enemy.TakeDamage(damage, enemy.transform.position, isCritical);
        }

        // 벽에 도달하면 파괴
        else if (isReturn && collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
