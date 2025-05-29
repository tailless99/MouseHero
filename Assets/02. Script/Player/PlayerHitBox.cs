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
        
        // 데미지를 입고도 죽지 않은 경우
        if(currentHp - damage > 0) {
            instance.GetDamage(damage);
        }
        // 데미지를 입고 죽는 경우
        else {
            instance.GetDamage(currentHp);
            Die();
        }
    }

    private void Die() {
        this.gameObject.SetActive(false);
    }
}
