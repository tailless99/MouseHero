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

    // 데미지 계산 함수
    private float SetDamage(float baseDamage) {
        return baseDamage * 2f;
    }

    // 폭탄 폭발 함수
    private void BombExplosion() {
        // 피격 범위에 있는 에너미들 리스트
        var enemys = Physics2D.OverlapCircleAll(transform.position, detectedRadius, LayerMask.GetMask("Enemy"));
        Instantiate(explosionEffectPrefab); // 폭발 이펙트 소환

        foreach(var enemy in enemys) {
            enemy.TryGetComponent<EnemyHitBox>(out EnemyHitBox enemyHitBox);
            enemyHitBox?.TakeDamage(damage, enemy.transform.position, isCritical);
        }

        Destroy(gameObject);
    }

    // 수류탄 충돌 출력 함수
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss")) {
            BombExplosion();
        }
    }
}
