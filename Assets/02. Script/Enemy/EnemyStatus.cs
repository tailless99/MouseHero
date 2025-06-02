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

    // �������� �ִ� �Լ�
    public void GetDamage(float damage) {
        currentHp -= damage;
        hpSlider.value = (currentHp / maxHp) <= 0 ? 0 : (currentHp / maxHp);
        
        // ���� üũ
        if (currentHp <= 0) {
            myEnemyController.isMoving = false;
            animator.SetBool("IsDead", true);
            return;
        }

        // ���� ���� ���
        if(!IsHiting)
        {
            animator.SetTrigger("Hit");
            myEnemyController.isMoving = false;
            IsHiting = true;
        }
    }

    // ���ʹ��� ���ݷ��� ��ȯ
    public float GetAttack() => attack;

    // Animator���� ���� �Լ�
    public void Die() {
        // ���� ����Ʈ ���
        var deathVFX = myEnemyController.GetDeathVFX();
        if (deathVFX) Instantiate(deathVFX, this.gameObject.transform.position, Quaternion.identity);

        // ��� ������ ����
        myEnemyController.DropItem();

        // ��� ���� ���� ��Ȱ��ȭ
        this.gameObject.SetActive(false);
    }

    public void OnHitEnd() {
        myEnemyController.isMoving = true;
        IsHiting = false;
    }
}
