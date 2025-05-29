using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private CircleCollider2D myCollider;

    private void Awake() {
        myCollider = GetComponent<CircleCollider2D>();
    }

    public void TakeDamage(float damage, GameObject cursor) {
        var instance = PlayerStatusManager.Instance;
        float currentHp = instance.GetCurrentHp();
        
        // �������� �԰� ���� ���� ���
        if(currentHp - damage > 0) {
            instance.GetDamage(damage);
        }
        // �������� �԰� �״� ���
        else {
            instance.GetDamage(currentHp);
            Die();
        }
    }

    private void Die() {
        this.gameObject.SetActive(false);
    }
}
