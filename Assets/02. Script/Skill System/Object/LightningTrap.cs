using System.Collections;
using System.Threading;
using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    //[SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private float destoryTime = 5f; // 설치 후 파괴되기까지의 시간

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform playerPos;
    private bool Isinstallation;

    // 설치 전/후 컬러
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

    // 플레이어에서 항상 직각 방향의 회전을 유지하는 기능
    private void LookPlayer() {
        // 트랩에서 플레이어쪽으로 가는 방향
        Vector2 directionTrapToPlayer = (playerPos.position - this.transform.position).normalized;
        float angleRad = Mathf.Atan2(directionTrapToPlayer.y, directionTrapToPlayer.x);
        
        this.transform.rotation = Quaternion.Euler(0, 0, angleRad * Mathf.Rad2Deg);
    }

    /// <summary>
    /// 설치 전 오브젝트가 마우스의 위치를 따라다니는 기능
    /// </summary>
    private void movePosition() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1; //  몬스터 보다 앞에 위치하기 위해 조정
        transform.position = mousePos;
    }

    // 함정 설치 완료 시 초기화 함수
    public void Installationed() {
        // 초기화 셋팅
        Isinstallation = true;
        spriteRenderer.color = afterColor;
        animator.enabled = true;

        // 지연 파괴 셋팅
        Destroy(this.gameObject, destoryTime); // N초 후 파괴
    }

    // 몬스터가 감지될 경우
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            collision.TryGetComponent<EnemyController>(out var enemy);
            enemy.EnemySlowlyRoutine(5f, 0.4f); // 감속 시간, 감속 스피드
        }
    }
}
