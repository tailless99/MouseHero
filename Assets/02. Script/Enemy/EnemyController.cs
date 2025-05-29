using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Setting")]
    [SerializeField] public float moveSpeed = 1f;

    [Header("ObjectPooling Use Name")]
    [SerializeField] string EnemyName;

    protected Animator animator;
    protected CircleCollider2D myCollider;
    protected Rigidbody2D rb;
    protected EnemyStatus enemyStatus;

    public bool isMoving = true;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyStatus = GetComponent<EnemyStatus>();
    }

    protected virtual void Update() {
        if (isMoving) {
            Move();
        }
    }

    // �÷��̾ ���� �����̴� �Լ�
    protected virtual void Move() {}

    // ���� ����
    protected virtual void Attack(PlayerHitBox player) {}

    // �÷��̾�� �ε��� ��� ���� �Լ� ����
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox);
            Attack(playerHitBox);
        }
    }
}
