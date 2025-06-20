using Unity.VisualScripting;
using UnityEngine;

public class SpinningShurikenObject : MonoBehaviour {
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float directionLimit = 10f; // �ִ� �̵� ���

    private Rigidbody2D rb;

    private float damage = 1f;       // ���� ������
    private bool isCritical = false; // ũ��Ƽ�� ����
    private bool isReturn = false; // �θ޶�ó�� ���ƿ��� ������ 
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

    // ��ȯ���� �������� üũ
    private void CheckTurningPoint() {
        var distance = Vector3.Distance(startPoint, transform.position); // ������ �Ÿ� ����

        // �ִ� �̵� ��κ��� �� �հŸ��� �������� ���
        // � ���� ����
        if(directionLimit <= distance) {
            isReturn = true;
            rb.linearVelocity = rb.linearVelocity * -1;
        }
    }

    // �ܺο��� ����ϴ� ��ü�� �߻� ���� ���� �Լ�
    public void ThrowSuriken(Vector3 startPos, Vector3 targetPos) {
        startPoint = startPos; // ���� ���� ��ġ ���
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
        damage *= 3f; // ���� ������ ������
    }

    // �ǰݽ�
    private void OnTriggerEnter2D(Collider2D collision) {
        // ���ʹ� �ǰ��� ���
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            var enemy = collision.gameObject.GetComponent<EnemyHitBox>();

            // ������ ����
            enemy.TakeDamage(damage, enemy.transform.position, isCritical);
        }

        // ���� �����ϸ� �ı�
        else if (isReturn && collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
