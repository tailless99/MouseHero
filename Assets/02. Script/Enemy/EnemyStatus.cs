using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public Slider hpSlider;
    public float maxHp = 1f;
    public float attack = 1f;

    private float currentHp;
    private bool IsHiting;
    private Animator animator;
    private EnemyController myEnemyController;

    private void Awake() {
        animator = GetComponent<Animator>();
        myEnemyController = GetComponent<EnemyController>();
    }

    private void Start() {
        currentHp = maxHp;
    }

    // 데미지를 주는 함수
    public void GetDamage(float damage) {
        currentHp -= damage;
        hpSlider.value = (currentHp / maxHp) <= 0 ? 0 : (currentHp / maxHp);
        
        // 죽음 체크
        if (currentHp <= 0) {
            animator.SetBool("IsDead", true);
            return;
        }

        // 죽지 않은 경우
        if(!IsHiting)
        {
            animator.SetTrigger("Hit");
            myEnemyController.isMoving = false;
            IsHiting = true;
        }
    }

    // 에너미의 공격력을 반환
    public float GetAttack() => attack;

    // Animator에서 사용될 함수
    public void Die() => this.gameObject.SetActive(false);
    public void OnHitEnd() {
        myEnemyController.isMoving = true;
        IsHiting = false;
    }
}
