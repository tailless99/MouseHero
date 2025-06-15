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
        { StatusType.HP, 1f },       // �ִ� HP
        { StatusType.Strength, 1f }, // ���ݷ�
        { StatusType.Defence, 1f },  // ����
        { StatusType.Luck, 1f }      // ũ��Ƽ��
    };

    private void Start() {
        currentHp = GetStatus(StatusType.HP);
        SetPlayerCharacter();
    }

    // �÷��̾� ĳ���� ����
    private void SetPlayerCharacter() => player = PlayerController.Instance.GetCharacter();

    // ���� ���� �� ���¸� �����ϴ� �Լ�
    public void SetCharacterStatus(float hp, float strength, float defence, float luck) {
        statusDict[StatusType.HP] = hp;
        statusDict[StatusType.Strength] = strength;
        statusDict[StatusType.Defence] = defence;
        statusDict[StatusType.Luck] = luck;
    }

    // �������ͽ��� �߰��Ѵ�
    public void AddStatus(StatusType type, float value) {
        if (statusDict.ContainsKey(type)) {
            statusDict[type] += value;

            // �ִ� HP�� �����ϴ� ���
            if (type == StatusType.HP) {
                float maxHp = statusDict[type];
                currentHp += value; // �ִ� hp�� ������ ��ŭ ���� ����
                MainUIContainer.Instance.UpdateHpPercent(currentHp / maxHp);
            }
        }

        // �÷��̾� ���� �ݿ�
        if (player == null) SetPlayerCharacter();
        player.UpdateAttackPoint();
    }

    // �������� ����� �ý��� + 1 �Լ�
    public void AddAllStatus() {
        // HP�� ������ �������ͽ� ���� ����
        foreach (StatusType type in Enum.GetValues(typeof(StatusType))) {
            if (type != StatusType.HP)
                statusDict[type] += 1;
        }
        
        // HP ������ ������ �۾��� �ʿ��ϴ� ���� ó��
        statusDict[StatusType.HP] += 1; // �ִ� HP ����
        float maxHp = statusDict[StatusType.HP];
        currentHp += 1; // �ִ� hp�� ������ ��ŭ ���� ����
        
        // UI ����
        MainUIContainer.Instance.UpdateHpPercent(currentHp / maxHp);
        
        // �÷��̾� ���� �ݿ�
        if (player == null) SetPlayerCharacter();
        player.UpdateAttackPoint();
    }

    // �������ͽ��� �����´�
    public float GetStatus(StatusType type) {
        return statusDict.TryGetValue(type, out var value) ? value : 0f;
    }

    // �������� �Դ� ���
    public void GetDamage(float damage) {
        float maxHp = statusDict[StatusType.HP]; // �ִ� ü��
        currentHp -= damage; // ���� ü�� ����

        // ü���� 0 ���Ϸ� �������� �ʵ��� Clamp
        currentHp = Mathf.Max(0, currentHp);
        float hpPercent = currentHp / maxHp;

        // UI ����
        MainUIContainer.Instance.UpdateHpPercent(hpPercent);
    }

    public void PlayerHealing(float healingPoint) {
        float maxHp = statusDict[StatusType.HP]; // �ִ� ü��
        currentHp += healingPoint;

        // ���� ü���� �ִ� ü���� ���� �� ����
        if (currentHp >= maxHp) currentHp = maxHp;
        float hpPercent = currentHp / maxHp;

        // UI ����
        MainUIContainer.Instance.UpdateHpPercent(hpPercent);
    }

    public float GetCurrentHp() => currentHp;
}
