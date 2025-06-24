using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Setting")]
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] protected GameObject deathVFX;

    [Header("ObjectPooling Use Name")]
    [SerializeField] string EnemyName;

    protected Animator animator;
    protected CircleCollider2D myCollider;
    protected Rigidbody2D rb;
    protected EnemyStatus enemyStatus;
    protected PlayerCharacterBase player;
    protected SpriteRenderer spriteRenderer;

    private EnemyDropItem enemyDropItem;

    public bool isMoving = true;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyStatus = GetComponent<EnemyStatus>();
        enemyDropItem = GetComponent<EnemyDropItem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        player = PlayerController.Instance.GetCharacter();
    }

    protected virtual void Update() {
        LookPlayer();

        if (isMoving) {
            Move();
        }
        else {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // 플레이어를 향해 움직이는 함수
    protected virtual void Move() {
        animator.SetBool("IsMoving", true);

        Vector3 dir = (player.transform.position - this.gameObject.transform.position).normalized * moveSpeed;
        rb.linearVelocity = new Vector2(dir.x, dir.y);
    }

    // 플레이어를 바라보도록 Flip하는 기능
    private void LookPlayer() {
        Vector3 dir = (player.transform.position - this.gameObject.transform.position).normalized;
        if (dir.x < 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }

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

    /// <summary>
    /// 에너미의 움직임을 느려지게 하는 코루틴 
    /// </summary>
    /// <param name="time">느려지는 시간</param>
    /// <param name="slowSpeed">0~1 까지의 실수 변수를 넘겨받아 이동속도의 %로 감속</param>
    public void EnemySlowlyRoutine(float time, float slowSpeed) {
        StartCoroutine(EnemySlolyCoroutine(time, slowSpeed));
    }

    // 에너미 일정시간 느려지는 코루틴
    private IEnumerator EnemySlolyCoroutine(float time, float slowSpeed) {
        var originSpeed = moveSpeed;
        moveSpeed *= slowSpeed;

        yield return new WaitForSeconds(time);

        moveSpeed = originSpeed;
    }
}
