using UnityEngine;

public class KunaiPronta : MonoBehaviour
{
    private PlayerApplyDamage playerAttack;

    private void OnEnable() {
        playerAttack = PlayerController.Instance.GetCharacter().GetComponent<PlayerCharacterBase>().GetPlayerApplyDamageClass();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            // ����� ���
            var damage = playerAttack.GetAttackBaseDamage() * 1.5f;

            // ����� ó��
            var enemyHit = collision.GetComponent<EnemyHitBox>();
            enemyHit.TakeDamage(damage, this.gameObject.transform.position, playerAttack.IsCriticalAttack());
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(this.gameObject);
        }
    }
}
