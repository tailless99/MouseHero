using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    // ĳ���͵��� ��� ��ų�� ��� ��ũ��Ʈ������Ʈ
    public CharacterSkillListSO characterSkillSO;

    [SerializeField] private List<SkillSO> currentCharacterAllSkillList; // ������� ĳ������ ��ü ��ų ����Ʈ
    private List<SkillSO> hasSkillList = new List<SkillSO>(); // �÷��̾ ���� ��ų ����Ʈ
    private bool isInitialized = false; // �ʱ�ȭ ����

    const float maxHasCount = 100f;

    private void Start() {
        hasSkillList.Clear();
    }

    // �ʱ�ȭ �Լ�
    public void InitializeSKillManager(string characterID) {
        //currentCharacterAllSkillList = characterSkillSO.GetSkillsByCharacter(characterID);
        currentCharacterAllSkillList = characterSkillSO.GetSkillsByCharacter("NinjaFrog");
        hasSkillList.Clear();
        isInitialized = true;
    }

    // ���ο� ��ų�� �߰��� �� �ִ��� üũ�ϴ� ���
    public bool CanAddNewSkill() {
        return hasSkillList.Count >= maxHasCount ? false : true;
    }

    // ��ų�� ��� ���
    public void AddNewSkill(SkillSO skill) {
        if (hasSkillList.Count >= maxHasCount) return;
        hasSkillList.Add(skill);
    }

    // ���� ��ų�� �����ϴ� ���
    public void DeleteHasSkill(SkillSO skill) {
        if (skill == null || !hasSkillList.Contains(skill)) return;

        hasSkillList.Remove(skill);
    }

    // ��ų ī�� �̱⿡ ���� ��ų ��ȯ �Լ�
    public SkillSO GetHasSkill() {
        if (hasSkillList == null || hasSkillList.Count == 0 || !isInitialized) return null;

        int randomIndex = Random.Range(0, hasSkillList.Count);
        return hasSkillList[randomIndex];
    }

    // �÷��̾ ȹ���� ��ų ����Ʈ�� ��ȯ
    public List<SkillSO> GetHasSkillList() => hasSkillList;

    // �÷��̾ ȹ���� �� �ִ� ��� ����Ʈ�� ��ȯ
    public List<SkillSO> GetCharacterAllSkillList() => currentCharacterAllSkillList;
}
