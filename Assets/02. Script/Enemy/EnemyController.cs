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

    // �÷��̾ ���� �����̴� �Լ�
    protected virtual void Move() {
        animator.SetBool("IsMoving", true);

        Vector3 dir = (player.transform.position - this.gameObject.transform.position).normalized * moveSpeed;
        rb.linearVelocity = new Vector2(dir.x, dir.y);
    }

    // �÷��̾ �ٶ󺸵��� Flip�ϴ� ���
    private void LookPlayer() {
        Vector3 dir = (player.transform.position - this.gameObject.transform.position).normalized;
        if (dir.x < 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }

    // ���� ����
    protected virtual void Attack(PlayerHitBox player) {}

    // �÷��̾�� �ε��� ��� ���� �Լ� ����
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox);
            Attack(playerHitBox);
        }
    }

    public GameObject GetDeathVFX() => deathVFX;
    public void DropItem() => enemyDropItem.DropItem();
    
    // ���ʹ̰� �׾��� �� �ۿ��ϴ� �Լ�
    public void Die() {
        MainUIContainer.Instance.AddExp(enemyDropItem.GetExp());
        myCollider.gameObject.SetActive(false);
    }

    /// <summary>
    /// ���ʹ��� �������� �������� �ϴ� �ڷ�ƾ 
    /// </summary>
    /// <param name="time">�������� �ð�</param>
    /// <param name="slowSpeed">0~1 ������ �Ǽ� ������ �Ѱܹ޾� �̵��ӵ��� %�� ����</param>
    public void EnemySlowlyRoutine(float time, float slowSpeed) {
        StartCoroutine(EnemySlolyCoroutine(time, slowSpeed));
    }

    // ���ʹ� �����ð� �������� �ڷ�ƾ
    private IEnumerator EnemySlolyCoroutine(float time, float slowSpeed) {
        var originSpeed = moveSpeed;
        moveSpeed *= slowSpeed;

        yield return new WaitForSeconds(time);

        moveSpeed = originSpeed;
    }
}
