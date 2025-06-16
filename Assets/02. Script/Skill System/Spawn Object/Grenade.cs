using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float detectedRadius = 15f;

    private Rigidbody2D rb;
    private float damage = 1f;
    private bool isCritical = false;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        var playerAttack = PlayerController.Instance.GetCharacter().GetPlayerApplyDamageClass();
        var baseDamage = playerAttack.GetAttackBaseDamage();
        isCritical = playerAttack.IsCriticalAttack();
        damage = SetDamage(baseDamage);
    }

    // ������ ��� �Լ�
    private float SetDamage(float baseDamage) {
        return baseDamage * 2f;
    }

    // ��ź ���� �Լ�
    private void BombExplosion() {
        // �ǰ� ������ �ִ� ���ʹ̵� ����Ʈ
        var enemys = Physics2D.OverlapCircleAll(transform.position, detectedRadius, LayerMask.GetMask("Enemy"));
        Instantiate(explosionEffectPrefab); // ���� ����Ʈ ��ȯ

        foreach(var enemy in enemys) {
            enemy.TryGetComponent<EnemyHitBox>(out EnemyHitBox enemyHitBox);
            enemyHitBox?.TakeDamage(damage, enemy.transform.position, isCritical);
        }

        Destroy(gameObject);
    }

    // ����ź �浹 ��� �Լ�
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            BombExplosion();
        }
    }
}
