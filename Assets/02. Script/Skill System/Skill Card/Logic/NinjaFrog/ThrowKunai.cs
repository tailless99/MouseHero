using UnityEngine;

public class ThrowKunai : SkillBase
{
    public GameObject projectilePrefab;
    public float speed = 10f;

    public override bool Use() {
        if (projectilePrefab == null) return false;
        
        // 진행 방향 설정
        var player = PlayerController.Instance.GetCharacter();
        Vector3 centerDirection = (GetMouseWorldPosition() - player.transform.position).normalized;

        // 회전 방향 설정
        float angle = Mathf.Atan2(centerDirection.y, centerDirection.x) * Mathf.Rad2Deg;

        // 3방향 회전값
        float[] angleOffsets = { 0f, 15f, -15f };

        foreach(float offset in angleOffsets) {
            // 각 방향 벡터 계산
            Vector2 dir = RotateVector(centerDirection, offset);
            Quaternion rot = Quaternion.Euler(0, 0, angle + offset);

            // 오브젝트 생성 및 투척 방향 설정
            GameObject proj = Instantiate(projectilePrefab, player.transform.position, rot);
            proj.GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
        }

        return true;
    }

    // 벡터를 주어진 각도로 회전시키는 함수
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
