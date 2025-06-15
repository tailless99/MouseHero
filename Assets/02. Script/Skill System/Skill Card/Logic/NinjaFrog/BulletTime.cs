using System.Collections;
using UnityEngine;

public class BulletTime : SkillBase
{
    [SerializeField] private float stopTime = 5f;

    /// <summary>
    /// ��Ȱ��ȭ�� ������Ʈ���� ��ũ��Ʈ ������ �������� ������ �ڷ�ƾ ���Ұ�
    /// ���� �ش� ĳ������ ���� ������ �̿��ؼ� �ڷ�ƾ�� ����ϱ�� ����
    /// </summary>
    public override bool Use() {

        var player = PlayerController.Instance.GetCharacter().GetComponent<NinjaFrog>();
        var currentMoney = MainUIContainer.Instance.GetCurrentMoney();
        var useMoney = 0; // ���� ��� �ݾ�
        
        // ����� �� ���
        if (currentMoney > 200) useMoney = 200;
        else useMoney = currentMoney;
        
        // �ʵ��� ���� �̸� ����
        var allMonsters = GameObject.FindGameObjectsWithTag("Enemy");

        // �Ҹ�Ÿ�� ����
        var result = player.BulletTime(stopTime, useMoney);
        if (!result) return false; // �Ҹ� Ÿ���� ������̶�� ���Ұ�

        // �ʵ��� ��� ���Ϳ��� ���ο�
        foreach(var monster in allMonsters) {
            monster.TryGetComponent<EnemyController>(out var enemy);
            enemy.EnemySlowlyRoutine(stopTime);
        }

        // ���� ���Ϳ��� ���ο�
        var bossMonster = GameObject.FindGameObjectWithTag("Boss");
        if(bossMonster != null) {
            // �������� ���ο� ����
        }
        return true;
    }
}
