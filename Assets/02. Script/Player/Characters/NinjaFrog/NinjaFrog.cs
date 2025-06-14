using System.Collections;
using UnityEngine;

public class NinjaFrog : PlayerCharacterBase
{
    [SerializeField] private GameObject bulletTimeEffect;

    public override void Attack() {
        base.Attack();
    }

    // 불렛타임 스킬
    public bool BulletTime(float time, int moneySpent) {
        // 불렛타임은 중복 실행이 불가능하다
        if (bulletTimeEffect.activeSelf) return false;
        bulletTimeEffect.SetActive(true); // 불릿 타임 타이머 이펙트 활성화

        BulletTimeStart(time, moneySpent);

        return true;
    }

    private IEnumerator BulletTimeStart(float time, int moneySpent) {
        // 비용 소모
        MainUIContainer.Instance.UpdateMoney(-moneySpent);

        // 스텟 증가
        var str = PlayerStatusManager.Instance.GetStatus(StatusType.Strength);
        var addStr = ((moneySpent * 1.1f) + (str * 1.5f)) * str; // 스텟 증가량
        PlayerStatusManager.Instance.AddStatus(StatusType.Strength, addStr); // 스텟 증가
        yield return new WaitForSecondsRealtime(time);

        // 지속 시간 종료 후 스테이터스 원상복귀
        PlayerStatusManager.Instance.AddStatus(StatusType.Strength, -addStr); // 스텟 감소
    }

    // 불릿 애니메이션에서 호출할 함수
    public void BulletTimeEnd() => bulletTimeEffect.SetActive(false);
}
