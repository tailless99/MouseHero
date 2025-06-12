using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    // 캐릭터들의 모든 스킬이 담긴 스크립트오브젝트
    public CharacterSkillListSO characterSkillSO;

    [SerializeField] private List<SkillSO> currentCharacterAllSkillList; // 사용중인 캐릭터의 전체 스킬 리스트
    private List<SkillSO> hasSkillList = new List<SkillSO>(); // 플레이어가 얻은 스킬 리스트
    private bool isInitialized = false; // 초기화 여부

    const float maxHasCount = 100f;

    private void Start() {
        hasSkillList.Clear();
    }

    // 초기화 함수
    public void InitializeSKillManager(string characterID) {
        //currentCharacterAllSkillList = characterSkillSO.GetSkillsByCharacter(characterID);
        currentCharacterAllSkillList = characterSkillSO.GetSkillsByCharacter("NinjaFrog");
        hasSkillList.Clear();
        isInitialized = true;
    }

    // 새로운 스킬을 추가할 수 있는지 체크하는 기능
    public bool CanAddNewSkill() {
        return hasSkillList.Count >= maxHasCount ? false : true;
    }

    // 스킬을 얻는 기능
    public void AddNewSkill(SkillSO skill) {
        if (hasSkillList.Count >= maxHasCount) return;
        hasSkillList.Add(skill);
    }

    // 얻은 스킬을 삭제하는 기능
    public void DeleteHasSkill(SkillSO skill) {
        if (skill == null || !hasSkillList.Contains(skill)) return;

        hasSkillList.Remove(skill);
    }

    // 스킬 카드 뽑기에 사용될 스킬 반환 함수
    public SkillSO GetHasSkill() {
        if (hasSkillList == null || hasSkillList.Count == 0 || !isInitialized) return null;

        int randomIndex = Random.Range(0, hasSkillList.Count);
        return hasSkillList[randomIndex];
    }

    // 플레이어가 획득한 스킬 리스트를 반환
    public List<SkillSO> GetHasSkillList() => hasSkillList;

    // 플레이어가 획득할 수 있는 모든 리스트를 반환
    public List<SkillSO> GetCharacterAllSkillList() => currentCharacterAllSkillList;
}
