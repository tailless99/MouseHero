using System.Collections;
using UnityEngine;

public class TrapInstallation : SkillBase
{
    [SerializeField] private GameObject trapPrefab;

    // 코루틴 스킬을 사용한다고 선언
    public override bool IsCoroutineSkill => true;

    // 코루틴 사용함수를 사용하니, 이건 사용하지 않음
    public override bool Use() {
        return false;
    }

    /// <summary>
    /// 코루틴 사용 함수. 작업동안 스킬 사용을 기다리고, 처리 결과에 따라 스킬 사용이 될지 취소될지 정해짐
    /// </summary>
    public override IEnumerator UseCoroutine(System.Action<bool> onClick) {
        var trapInstance = Instantiate(trapPrefab);
        bool result = false; // 반환 변수
        bool inputHandled = false; // while문 내부에서 클릭 감지 변수

        while(!inputHandled){
            // 왼 클릭 감지 - 트랩 설치
            if (Input.GetMouseButtonDown(0)) {
                trapInstance.TryGetComponent<LightningTrap>(out var trap);
                trap.Installationed(); // 설치 후처리 함수
                result = true;
                inputHandled = true;
            }
            // 오른 클릭 감지 - 트랩 설치 취소
            else if (Input.GetMouseButtonDown(1)) {
                // 스킬 사용 취소 절차
                Destroy(trapInstance); // 생성한 트랩 파괴
                result = false;
                inputHandled = true;
            }
            yield return null;
        }
        onClick?.Invoke(result);
    }
}
