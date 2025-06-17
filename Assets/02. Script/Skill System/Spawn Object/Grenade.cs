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

        // ������ ����
        var baseDamage = playerAttack.GetAttackBaseDamage();
        isCritical = playerAttack.IsCriticalAttack();
        damage = SetDamage(baseDamage);

        // ������ ���� ����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ThrowGrenade(transform.position, mousePos);
    }

    // ������ ��� �Լ�
    private float SetDamage(float baseDamage) {
        return baseDamage * 2f;
    }

    // �ܺο��� ȣ���Ͽ� ����ź �߻縦 �����ϴ� �Լ�
    public void ThrowGrenade(Vector2 startPos, Vector2 targetPos) {
        transform.position = startPos; // ���� ��ġ�� ����ź �̵�

        Vector2 displacement = targetPos - startPos;
        float gravityY = Physics2D.gravity.y * rb.gravityScale; // �߷� ���ӵ�

        // ��ǥ���� ���� �ð�
        float time = collisionTime;

        // X, Y�� �ʱ� �ӵ� ����
        float velocityX = displacement.x / time;
        float velocityY = (displacement.y - 0.5f * gravityY * time * time) / time;

        rb.linearVelocity = new Vector2(velocityX, velocityY);
    }

    // ��ź ���� �Լ�
    private void BombExplosion() {
        // �ǰ� ������ �ִ� ���ʹ̵� ����Ʈ
        var enemys = Physics2D.OverlapCircleAll(transform.position, detectedRadius, LayerMask.GetMask("Enemy"));
        var spawPos = transform.position;
        spawPos.z = -1; // ���ʹ� ���� �տ� ��µǱ� ����

        Instantiate(explosionEffectPrefab, spawPos, Quaternion.identity); // ���� ����Ʈ ��ȯ

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
