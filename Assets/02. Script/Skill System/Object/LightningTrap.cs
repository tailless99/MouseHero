using System.Collections;
using System.Threading;
using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private float destoryTime = 5f; // ��ġ �� �ı��Ǳ������ �ð�

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform playerPos;
    private bool Isinstallation;

    // ��ġ ��/�� �÷�
    private Color beforeColor = new Color(255f/255f, 255f/255f, 255f / 255f, 100/255f);
    private Color afterColor = new Color(255f/255f, 255f/255f, 255f / 255f, 255f/255f);


    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.enabled = false;
    }

    private void OnEnable() {
        playerPos = PlayerController.Instance.GetCharacter().transform;
        spriteRenderer.color = beforeColor;
        Isinstallation = false;
    }

    private void Update() {
        if (!Isinstallation) {
            LookPlayer();
            movePosition();
        }
    }

    // �÷��̾�� �׻� ���� ������ ȸ���� �����ϴ� ���
    private void LookPlayer() {
        // Ʈ������ �÷��̾������� ���� ����
        Vector2 directionTrapToPlayer = (playerPos.position - this.transform.position).normalized;
        float angleRad = Mathf.Atan2(directionTrapToPlayer.y, directionTrapToPlayer.x);
        
        this.transform.rotation = Quaternion.Euler(0, 0, angleRad * Mathf.Rad2Deg);
    }

    /// <summary>
    /// ��ġ �� ������Ʈ�� ���콺�� ��ġ�� ����ٴϴ� ���
    /// </summary>
    private void movePosition() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1; //  ���� ���� �տ� ��ġ�ϱ� ���� ����
        transform.position = mousePos;
    }

    // ���� ��ġ �Ϸ� �� �ʱ�ȭ �Լ�
    public void Installationed() {
        // �ʱ�ȭ ����
        Isinstallation = true;
        spriteRenderer.color = afterColor;
        animator.enabled = true;

        // ���� �ı� ����
        Destroy(this.gameObject, destoryTime); // N�� �� �ı�
    }

    // ���Ͱ� ������ ���
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            collision.TryGetComponent<EnemyController>(out var enemy);
            enemy.EnemySlowlyRoutine(5f, 0.4f); // ���� �ð�, ���� ���ǵ�
        }
    }
}
