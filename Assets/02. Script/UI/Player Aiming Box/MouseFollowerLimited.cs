using UnityEngine;

public class MouseFollowerLimited : MonoBehaviour {
    [SerializeField] private float followSpeed = 1000f; // 마우스 따라가는 스피드
    [SerializeField] private Collider2D aimingBoxCollider;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    void Update() {
        if (aimingBoxCollider == null) return;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
