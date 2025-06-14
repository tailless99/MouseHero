using UnityEngine;

public class ThrowKunai : SkillBase
{
    public GameObject projectilePrefab;
    public float speed = 10f;

    public override bool Use() {
        if (projectilePrefab == null) return false;
        
        // ���� ���� ����
        var player = PlayerController.Instance.GetCharacter();
        Vector3 centerDirection = (GetMouseWorldPosition() - player.transform.position).normalized;

        // ȸ�� ���� ����
        float angle = Mathf.Atan2(centerDirection.y, centerDirection.x) * Mathf.Rad2Deg;

        // 3���� ȸ����
        float[] angleOffsets = { 0f, 15f, -15f };

        foreach(float offset in angleOffsets) {
            // �� ���� ���� ���
            Vector2 dir = RotateVector(centerDirection, offset);
            Quaternion rot = Quaternion.Euler(0, 0, angle + offset);

            // ������Ʈ ���� �� ��ô ���� ����
            GameObject proj = Instantiate(projectilePrefab, player.transform.position, rot);
            proj.GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
        }

        return true;
    }

    // ���͸� �־��� ������ ȸ����Ű�� �Լ�
    private Vector2 RotateVector(Vector2 basePos, float angleDegrees) {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(
            basePos.x * cos - basePos.y * sin,
            basePos.x * sin + basePos.y * cos
        );
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
