using UnityEngine;

public class SpinningShuriken : SkillBase {
    [SerializeField] private GameObject shurikenPrefab;

    public override bool Use() {
        var spawnPos = PlayerController.Instance.GetCharacter().transform.position; // ���� ��ġ
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        // ��ǥ ��ġ

        // ������ �ν��Ͻ� ����
        GameObject shurikenObject = Instantiate(shurikenPrefab, spawnPos, Quaternion.identity);
        var shuriken = shurikenObject.GetComponent<SpinningShurikenObject>();

        if (shuriken != null) {
            // ������ �߻� �Լ� ȣ��
            shuriken.ThrowSuriken(spawnPos, targetPos);
        }
        else Destroy(shurikenObject); // ��ũ��Ʈ�� ������ ������ ������Ʈ �ı�

        return true;
    }
}

