using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StatusType {
    HP,
    Strength,
    Defence,
    Luck
}

public class PlayerStatusManager : Singleton<PlayerStatusManager> {
    private float currentHp;
    private PlayerCharacterBase player;


    private readonly Dictionary<StatusType, float> statusDict = new Dictionary<StatusType, float>
    {
        { StatusType.HP, 1f },       // 최대 HP
        { StatusType.Strength, 1f }, // 공격력
        { StatusType.Defence, 1f },  // 방어력
        { StatusType.Luck, 1f }      // 크리티컬
    };

    private void Start() {
        currentHp = GetStatus(StatusType.HP);
        SetPlayerCharacter();
    }

    // 플레이어 캐릭터 대입
    private void SetPlayerCharacter() => player = PlayerController.Instance.GetCharacter();

    // 게임 시작 시 상태를 설정하는 함수
    public void SetCharacterStatus(float hp, float strength, float defence, float luck) {
        statusDict[StatusType.HP] = hp;
        statusDict[StatusType.Strength] = strength;
        statusDict[StatusType.Defence] = defence;
        statusDict[StatusType.Luck] = luck;
    }

    // 스테이터스를 추가한다
    public void AddStatus(StatusType type, float value) {
        if (statusDict.ContainsKey(type)) {
            statusDict[type] += value;

            // 최대 HP가 증가하는 경우
            if (type == StatusType.HP) {
                float maxHp = statusDict[type];
                currentHp += value; // 최대 hp가 증가한 만큼 같이 증가
                MainUIContainer.Instance.UpdateHpPercent(currentHp / maxHp);
            }
        }

        // 플레이어 스탯 반영
        if (player == null) SetPlayerCharacter();
        player.UpdateAttackPoint();
    }

    // 레벨업에 사용할 올스텟 + 1 함수
    public void AddAllStatus() {
        // HP를 제외한 스테이터스 전부 증가
        foreach (StatusType type in Enum.GetValues(typeof(StatusType))) {
            if (type != StatusType.HP)
                statusDict[type] += 1;
        }
        
        // HP 증가는 별도의 작업이 필요하니 따로 처리
        statusDict[StatusType.HP] += 1; // 최대 HP 증가
        float maxHp = statusDict[StatusType.HP];
        currentHp += 1; // 최대 hp가 증가한 만큼 같이 증가
        
        // UI 적용
        MainUIContainer.Instance.UpdateHpPercent(currentHp / maxHp);
        
        // 플레이어 스탯 반영
        if (player == null) SetPlayerCharacter();
        player.UpdateAttackPoint();
    }

    // 스테이터스를 가져온다
    public float GetStatus(StatusType type) {
        return statusDict.TryGetValue(type, out var value) ? value : 0f;
    }

    // 데미지를 입는 경우
    public void GetDamage(float damage) {
        float maxHp = statusDict[StatusType.HP]; // 최대 체력
        currentHp -= damage; // 현재 체력 감소

        // 체력이 0 이하로 내려가지 않도록 Clamp
        currentHp = Mathf.Max(0, currentHp);
        float hpPercent = currentHp / maxHp;

        // UI 갱신
        MainUIContainer.Instance.UpdateHpPercent(hpPercent);
    }

    public void PlayerHealing(float healingPoint) {
        float maxHp = statusDict[StatusType.HP]; // 최대 체력
        currentHp += healingPoint;

        // 현재 체력은 최대 체력을 넘을 수 없다
        if (currentHp >= maxHp) currentHp = maxHp;
        float hpPercent = currentHp / maxHp;

        // UI 갱신
        MainUIContainer.Instance.UpdateHpPercent(hpPercent);
    }

    public float GetCurrentHp() => currentHp;
}
