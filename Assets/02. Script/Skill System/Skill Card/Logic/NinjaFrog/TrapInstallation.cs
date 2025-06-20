using System.Collections;
using UnityEngine;

public class TrapInstallation : SkillBase
{
    [SerializeField] private GameObject trapPrefab;

    // �ڷ�ƾ ��ų�� ����Ѵٰ� ����
    public override bool IsCoroutineSkill => true;

    // �ڷ�ƾ ����Լ��� ����ϴ�, �̰� ������� ����
    public override bool Use() {
        return false;
    }

    /// <summary>
    /// �ڷ�ƾ ��� �Լ�. �۾����� ��ų ����� ��ٸ���, ó�� ����� ���� ��ų ����� ���� ��ҵ��� ������
    /// </summary>
    public override IEnumerator UseCoroutine(System.Action<bool> onClick) {
        var trapInstance = Instantiate(trapPrefab);
        bool result = false; // ��ȯ ����
        bool inputHandled = false; // while�� ���ο��� Ŭ�� ���� ����

        while(!inputHandled){
            // �� Ŭ�� ���� - Ʈ�� ��ġ
            if (Input.GetMouseButtonDown(0)) {
                trapInstance.TryGetComponent<LightningTrap>(out var trap);
                trap.Installationed(); // ��ġ ��ó�� �Լ�
                result = true;
                inputHandled = true;
            }
            // ���� Ŭ�� ���� - Ʈ�� ��ġ ���
            else if (Input.GetMouseButtonDown(1)) {
                // ��ų ��� ��� ����
                Destroy(trapInstance); // ������ Ʈ�� �ı�
                result = false;
                inputHandled = true;
            }
            yield return null;
        }
        onClick?.Invoke(result);
    }
}
