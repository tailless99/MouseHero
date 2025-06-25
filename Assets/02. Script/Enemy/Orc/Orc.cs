using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Orc : EnemyController {
    [Header("공격 쿨타임")]
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

        StartCoroutine(AttackLoopRoutine(player)); // 공격 반복 루틴 실행
    }

    private IEnumerator AttackLoopRoutine(PlayerHitBox player) {
        while(true){
            // 에너미 사망 체크
            var isDead = transform.GetComponent<EnemyController>().isDead;
            if (isDead) yield break; // 에너미가 죽었다면 공격 종료

            // 데미지 처리
            player.TakeDamage(enemyStatus.GetAttack(), this.gameObject);

            // 공격 애니메이션 처리
            if (IsAttacking) {
                animator.SetBool("IsAttacking", true);
                IsAttacking = false;
            }
            else {
                animator.SetBool("IsAttacking", false);
                IsAttacking = true;
            }
            animator.SetTrigger("Attack"); // 공용 코드

            // 쿨타임 처리
            yield return new WaitForSeconds(attackCoolTime);
        }
    }
}
