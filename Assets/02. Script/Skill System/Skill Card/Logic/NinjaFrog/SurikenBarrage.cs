using UnityEngine;

public class SurikenBarrage : SkillBase
{
    [SerializeField] private GameObject shurikenPrefab; // ������ ������
    [SerializeField] private int spawnCount; // ��ȯ�� ����

    public override bool Use() {
        float angleStep = 360f / spawnCount; // 360��
        
        // ���� ��ġ
        var spawnPos = PlayerController.Instance.GetCharacter().gameObject.transform.position;

        for (int i=0; i < spawnCount; i++) {
            float currentAngle = i * angleStep; // ���� �������� ����

            // ������ �������� ��ȯ
            float angleRad = currentAngle * Mathf.Deg2Rad;

            Vector2 targetDir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
            Vector2 targetPos = (Vector2)spawnPos + targetDir * 100f; // �߽ɿ��� �� ��ǥ��

            // ������ �ν��Ͻ� ����
            GameObject newShurikenGO = Instantiate(shurikenPrefab, spawnPos, Quaternion.identity);
            Suriken_v1 shuriken = newShurikenGO.GetComponent<Suriken_v1>();

            if (shuriken != null) {
                // ������ �߻� �Լ� ȣ��
                shuriken.ThrowSuriken(spawnPos, targetPos);
            }
            else Destroy(newShurikenGO); // ��ũ��Ʈ�� ������ ������ ������Ʈ �ı�
        }
        return true;
    }
}
