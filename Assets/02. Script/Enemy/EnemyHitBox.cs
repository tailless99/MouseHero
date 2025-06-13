using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [Header("Damage_UI")]

    private CircleCollider2D myCollider;
    private EnemyStatus enemyStatus;
    private EnemyController enemyController;

    private void Awake() {
        myCollider = GetComponent<CircleCollider2D>();
        enemyStatus = GetComponent<EnemyStatus>();
        enemyController = GetComponent<EnemyController>();
    }

    //  데미지를 입을 때 출력되는 함수
    public void TakeDamage(float damage, Vector3 DamageTextSpawnPos, bool isCritical) {
        // UI Setting
        var textGameObject = ObjectPoolingManager.Instance.MakeObj("Damage");
        
        // Text 기본 설정 셋팅
        textGameObject.transform.position = DamageTextSpawnPos;
        textGameObject.TryGetComponent<TextMeshPro>(out TextMeshPro damageText);
        damageText.text = damage.ToString();
        damageText.color = Color.white;

        // 크리티컬일 경우 색상 변경
        if (isCritical) {
            Color criticalColor;
            if (ColorUtility.TryParseHtmlString("#FA6302", out criticalColor)) { 
                damageText.color = criticalColor; 
            }
        }

        // 에너미 경직 코루틴
        StartCoroutine(StopMoveCoroutine());

        // 데미지 처리
        enemyStatus.GetDamage(damage);
    }

    // 에너미의 경직을 조절
    private IEnumerator StopMoveCoroutine() {
        enemyController.isMoving = false;
        yield return new WaitForSeconds(0.5f);

        enemyController.isMoving = true;
    }
}
