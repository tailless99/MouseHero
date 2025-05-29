using UnityEngine;

public class VirtualGuy : EnemyController {

    PlayerCharacterBase player;

    private void Start() {
        player = PlayerController.Instance.GetCharacter();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void Move() {
        base.Move();
        
        Vector3 dir = (player.transform.position - this.gameObject.transform.position).normalized * moveSpeed;
        rb.linearVelocity = new Vector2(dir.x, dir.y);
    }

    protected override void Attack(PlayerHitBox player) {
        base.Move();
        
        // 데미지 처리
        player.TakeDamage(enemyStatus.GetAttack(), this.gameObject);
        
        // 자폭 연출
        this.gameObject.SetActive(false);
    }
}
