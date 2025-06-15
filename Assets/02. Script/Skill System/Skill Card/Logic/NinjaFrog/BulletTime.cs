using System.Collections;
using UnityEngine;

public class BulletTime : SkillBase
{
    [SerializeField] private float stopTime = 5f;

    /// <summary>
    /// 비활성화된 오브젝트에서 스크립트 정보만 가져오기 때문에 코루틴 사용불가
    /// 따라서 해당 캐릭터의 내부 로직을 이용해서 코루틴을 사용하기로 했음
    /// </summary>
    public override bool Use() {

        var player = PlayerController.Instance.GetCharacter().GetComponent<NinjaFrog>();
        var currentMoney = MainUIContainer.Instance.GetCurrentMoney();
        var useMoney = 0; // 실제 사용 금액
        
        // 지출된 돈 계산
        if (currentMoney > 200) useMoney = 200;
        else useMoney = currentMoney;
        
        // 필드의 몬스터 미리 색적
        var allMonsters = GameObject.FindGameObjectsWithTag("Enemy");

        // 불릿타임 시작
        var result = player.BulletTime(stopTime, useMoney);
        if (!result) return false; // 불릿 타임이 사용중이라면 사용불가

        // 필드의 모든 몬스터에게 슬로우
        foreach(var monster in allMonsters) {
            monster.TryGetComponent<EnemyController>(out var enemy);
            enemy.EnemySlowlyRoutine(stopTime);
        }

        // 보스 몬스터에게 슬로우
        var bossMonster = GameObject.FindGameObjectWithTag("Boss");
        if(bossMonster != null) {
            // 보스몬스터 슬로우 로직
        }
        return true;
    }
}
