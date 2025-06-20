using UnityEngine;

public class SpinningShuriken : SkillBase {
    [SerializeField] private GameObject shurikenPrefab;

    public override bool Use() {
        var spawnPos = PlayerController.Instance.GetCharacter().transform.position; // 시작 위치
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        // 목표 위치

        // 수리검 인스턴스 생성
        GameObject shurikenObject = Instantiate(shurikenPrefab, spawnPos, Quaternion.identity);
        var shuriken = shurikenObject.GetComponent<SpinningShurikenObject>();

        if (shuriken != null) {
            // 수리검 발사 함수 호출
            shuriken.ThrowSuriken(spawnPos, targetPos);
        }
        else Destroy(shurikenObject); // 스크립트가 없으면 생성된 오브젝트 파괴

        return true;
    }
}

