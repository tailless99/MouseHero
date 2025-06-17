using UnityEngine;

public class SurikenBarrage : SkillBase
{
    [SerializeField] private GameObject shurikenPrefab; // 수리검 프리팹
    [SerializeField] private int spawnCount; // 소환할 숫자

    public override bool Use() {
        float angleStep = 360f / spawnCount; // 360도
        
        // 스폰 위치
        var spawnPos = PlayerController.Instance.GetCharacter().gameObject.transform.position;

        for (int i=0; i < spawnCount; i++) {
            float currentAngle = i * angleStep; // 현재 수리검의 각도

            // 각도를 라디안으로 변환
            float angleRad = currentAngle * Mathf.Deg2Rad;

            Vector2 targetDir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
            Vector2 targetPos = (Vector2)spawnPos + targetDir * 100f; // 중심에서 먼 목표점

            // 수리검 인스턴스 생성
            GameObject newShurikenGO = Instantiate(shurikenPrefab, spawnPos, Quaternion.identity);
            Suriken_v1 shuriken = newShurikenGO.GetComponent<Suriken_v1>();

            if (shuriken != null) {
                // 수리검 발사 함수 호출
                shuriken.ThrowSuriken(spawnPos, targetPos);
            }
            else Destroy(newShurikenGO); // 스크립트가 없으면 생성된 오브젝트 파괴
        }
        return true;
    }
}
