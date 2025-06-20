using UnityEngine;

public class Suriken_v1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    
    private Rigidbody2D rb;

    private float damage = 1f;       // ���� ������
    private bool isCritical = false; // ũ��Ƽ�� ����


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        SetDamage();
    }

    // �ܺο��� ����ϴ� ��ü�� �߻� ���� ���� �Լ�
    public void ThrowSuriken(Vector3 startPos, Vector3 targetPos) {
        var dir = (targetPos - startPos).normalized;
        rb.linearVelocity = dir * moveSpeed;
    }

    // ������ ���� ���
    private void SetDamage() {
        var playerAttackComp = PlayerController.Instance?.GetCharacter()?.GetPlayerApplyDamageClass();
        if (playerAttackComp == null) return;
        
        damage = playerAttackComp.GetAttackBaseDamage();  // ���̽� ������
        isCritical = playerAttackComp.IsCriticalAttack(); // ũ��Ƽ�� ����

        // ������ ����� ����
        // ���̽� ���ݷ� * 1.3f
        damage *= 1.3f; // ���� ������ ������
    }

    // �ǰݽ�
    private void OnTriggerEnter2D(Collider2D collision) {
        // ���ʹ� �ǰ��� ���
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            var enemy = collision.gameObject.GetComponent<EnemyHitBox>();

            // ������ ����
            enemy.TakeDamage(damage, enemy.transform.position, isCritical);
            Destroy(gameObject);
        }
        
        // ���� �����ϸ� �ı�
        else if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
