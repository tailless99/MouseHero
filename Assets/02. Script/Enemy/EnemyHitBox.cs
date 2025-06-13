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

    //  �������� ���� �� ��µǴ� �Լ�
    public void TakeDamage(float damage, Vector3 DamageTextSpawnPos, bool isCritical) {
        // UI Setting
        var textGameObject = ObjectPoolingManager.Instance.MakeObj("Damage");
        
        // Text �⺻ ���� ����
        textGameObject.transform.position = DamageTextSpawnPos;
        textGameObject.TryGetComponent<TextMeshPro>(out TextMeshPro damageText);
        damageText.text = damage.ToString();
        damageText.color = Color.white;

        // ũ��Ƽ���� ��� ���� ����
        if (isCritical) {
            Color criticalColor;
            if (ColorUtility.TryParseHtmlString("#FA6302", out criticalColor)) { 
                damageText.color = criticalColor; 
            }
        }

        // ���ʹ� ���� �ڷ�ƾ
        StartCoroutine(StopMoveCoroutine());

        // ������ ó��
        enemyStatus.GetDamage(damage);
    }

    // ���ʹ��� ������ ����
    private IEnumerator StopMoveCoroutine() {
        enemyController.isMoving = false;
        yield return new WaitForSeconds(0.5f);

        enemyController.isMoving = true;
    }
}
