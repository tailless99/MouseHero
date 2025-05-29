using UnityEngine;

public class MouseFollowerLimited : MonoBehaviour {
    public float followSpeed = 1000f; // ���콺 ���󰡴� ���ǵ�
    [SerializeField] private Collider2D aimingBoxCollider;

    void Update() {
        if (aimingBoxCollider == null) return;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        // AimingBox �ݶ��̴� ���ο� �ִ��� Ȯ��
        if (aimingBoxCollider.OverlapPoint(mouseWorldPosition)) {
            transform.position = mouseWorldPosition;
        }
        else {
            Vector3 closestPoint = aimingBoxCollider.ClosestPoint(mouseWorldPosition);
            transform.position = closestPoint;
        }
    }
}
