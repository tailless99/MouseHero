using UnityEngine;

public class ThrowKunai : SkillBase
{
    public GameObject projectilePrefab;
    public float speed = 10f;

    public override void Use() {
        if (projectilePrefab == null) return;

        //var player = PlayerController.Instance.GetCharacter();
        //Vector3 direction = (GetMouseWorldPosition() - player.transform.position).normalized;
        //GameObject proj = Instantiate(projectilePrefab, player.transform.position, Quaternion.identity);
        //proj.GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
