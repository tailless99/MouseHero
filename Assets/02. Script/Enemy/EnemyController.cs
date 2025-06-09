using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Setting")]
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] private GameObject deathVFX;

    [Header("ObjectPooling Use Name")]
    [SerializeField] string EnemyName;

    protected Animator animator;
    protected CircleCollider2D myCollider;
    protected Rigidbody2D rb;
    protected EnemyStatus enemyStatus;

    private EnemyDropItem enemyDropItem;

    public bool isMoving = true;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyStatus = GetComponent<EnemyStatus>();
        enemyDropItem = GetComponent<EnemyDropItem>();
    }

    protected virtual void Update() {
        if (isMoving) {
            Move();
        }
    }

    // 플레이어를 향해 움직이는 함수
    protected virtual void Move() {}

    // 몸박 공격
    protected virtual void Attack(PlayerHitBox player) {}

    // 플레이어랑 부딪힐 경우 공격 함수 실행
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox);
            Attack(playerHitBox);
        }
    }

    public GameObject GetDeathVFX() => deathVFX;
    public void DropItem() => enemyDropItem.DropItem();
    
    // 에너미가 죽었을 때 작용하는 함수
    public void Die() {
        MainUIContainer.Instance.AddExp(enemyDropItem.GetExp());
        myCollider.gameObject.SetActive(false);
    }
}
