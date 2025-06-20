using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;
    [SerializeField] private float detectedRadius = 15f;
    [SerializeField] private float collisionTime = 0.5f;

    private Rigidbody2D rb;
    private float damage = 1f;
    private bool isCritical = false;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable() {
        var playerAttack = PlayerController.Instance?.GetCharacter()?.GetPlayerApplyDamageClass();
        if (playerAttack == null) return;

        // 데미지 셋팅
        var baseDamage = playerAttack.GetAttackBaseDamage();
        isCritical = playerAttack.IsCriticalAttack();
        damage = SetDamage(baseDamage);

        // 던지는 방향 셋팅
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ThrowGrenade(transform.position, mousePos);
    }

    // 데미지 계산 함수
    private float SetDamage(float baseDamage) {
        return baseDamage * 2f;
    }

    // 외부에서 호출하여 수류탄 발사를 시작하는 함수
    public void ThrowGrenade(Vector2 startPos, Vector2 targetPos) {
        transform.position = startPos; // 시작 위치로 수류탄 이동

        Vector2 displacement = targetPos - startPos;
        float gravityY = Physics2D.gravity.y * rb.gravityScale; // 중력 가속도

        // 목표까지 도달 시간
        float time = collisionTime;

        // X, Y축 초기 속도 역산
        float velocityX = displacement.x / time;
        float velocityY = (displacement.y - 0.5f * gravityY * time * time) / time;

        rb.linearVelocity = new Vector2(velocityX, velocityY);
    }

    // 폭탄 폭발 함수
    private void BombExplosion() {
        // 피격 범위에 있는 에너미들 리스트
        var enemys = Physics2D.OverlapCircleAll(transform.position, detectedRadius, LayerMask.GetMask("Enemy"));
        var spawPos = transform.position;
        spawPos.z = -1; // 에너미 보다 앞에 출력되기 위해

        Instantiate(explosionEffectPrefab, spawPos, Quaternion.identity); // 폭발 이펙트 소환

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
