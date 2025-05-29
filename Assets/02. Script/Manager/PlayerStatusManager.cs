using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StatusType {
    HP,
    Strength,
    Defence,
    Luck
}

public class PlayerStatusManager : Singleton<PlayerStatusManager>
{
    private float currentHp;

    private readonly Dictionary<StatusType, float> statusDict = new Dictionary<StatusType, float>
    {
        { StatusType.HP, 1f },       // �ִ� HP
        { StatusType.Strength, 1f }, // ���ݷ�
        { StatusType.Defence, 1f },  // ����
        { StatusType.Luck, 1f }      // ũ��Ƽ��
    };

    private void Start() {
        currentHp = GetStatus(StatusType.HP);
    }

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
                MainUIContainer.Instance.UpdateHpPercent(maxHp / currentHp);
            }
        }
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

    public float GetCurrentHp() => currentHp;
}
