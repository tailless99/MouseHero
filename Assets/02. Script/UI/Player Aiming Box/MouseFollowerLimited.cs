using UnityEngine;

public class MouseFollowerLimited : MonoBehaviour {
    public float followSpeed = 1000f; // 마우스 따라가는 스피드
    [SerializeField] private Collider2D aimingBoxCollider;

    void Update() {
        if (aimingBoxCollider == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        // AimingBox 콜라이더 내부에 있는지 확인
        if (aimingBoxCollider.OverlapPoint(mouseWorldPosition)) {
            transform.position = mouseWorldPosition;
        }
        else {
            Vector3 closestPoint = aimingBoxCollider.ClosestPoint(mouseWorldPosition);
            transform.position = closestPoint;
        }
    }
}
