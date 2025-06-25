using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Orc : EnemyController {
    [Header("���� ��Ÿ��")]
    [SerializeField] private float attackCoolTime = 2.5f;
    
    private bool IsAttacking = false;

    protected override void Update() {
        base.Update();
    }

    protected override void Move() {
        base.Move();
    }

    protected override void Attack(PlayerHitBox player) {
        base.Attack(player);

        StartCoroutine(AttackLoopRoutine(player)); // ���� �ݺ� ��ƾ ����
    }

    private IEnumerator AttackLoopRoutine(PlayerHitBox player) {
        while(true){
            // ���ʹ� ��� üũ
            var isDead = transform.GetComponent<EnemyController>().isDead;
            if (isDead) yield break; // ���ʹ̰� �׾��ٸ� ���� ����

            // ������ ó��
            player.TakeDamage(enemyStatus.GetAttack(), this.gameObject);

            // ���� �ִϸ��̼� ó��
            if (IsAttacking) {
                animator.SetBool("IsAttacking", true);
                IsAttacking = false;
            }
            else {
                animator.SetBool("IsAttacking", false);
                IsAttacking = true;
            }
            animator.SetTrigger("Attack"); // ���� �ڵ�

            // ��Ÿ�� ó��
            yield return new WaitForSeconds(attackCoolTime);
        }
    }
}
