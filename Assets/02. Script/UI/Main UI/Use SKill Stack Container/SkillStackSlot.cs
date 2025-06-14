using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillStackSlot : MonoBehaviour
{
    [SerializeField] private Sprite emptyImg;

    private Image skillStackImg; // ��ȯ�� �̹��� ������Ʈ
    [SerializeField] private SkillSO currentSkillObject; // ���� ���Ե� ��ų ����
    private bool isUseSlot;

    private void Start() {
        skillStackImg = GetComponent<Image>();
    }

    /// <summary>
    /// ������ ����ϱ� ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="imgSprite"></param>
    public void SlotSetting(SkillSO skill) {
        currentSkillObject = skill;
        skillStackImg.sprite = currentSkillObject.skillIcon;
        isUseSlot = true;
    }

    /// <summary>
    /// ��ϵ� ��ų�� ����ϰ�, ������ �ٽ� �ʱ�ȭ ��Ű�� �Լ�
    /// </summary>
    public void UseSkillCard() {
        var result = currentSkillObject?.skillLogicPrefab?.Use(); // ��ų�� �ߵ� ���� ����

        // ���� ��ų�� ������ �� ���� ���¶�� ��ȯ
        if (result == false || result == null) return;

        // �ʱ�ȭ
        currentSkillObject = null;
        skillStackImg.sprite = emptyImg;
        isUseSlot = false;
    }

    /// <summary>
    /// ������ ��������� ���θ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool IsUsedSlot() => isUseSlot;
}
