using UnityEngine;

public class VirtualGuy : EnemyController {
    protected override void Update() {
        base.Update();
    }

    protected override void Move() {
        base.Move();
    }

    protected override void Attack(PlayerHitBox player) {
        base.Move();
        
        // ������ ó��
        player.TakeDamage(enemyStatus.GetAttack(), this.gameObject);

        // ���� ����
        Vector3 spawnPos = transform.position;
        Instantiate(deathVFX, spawnPos, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
