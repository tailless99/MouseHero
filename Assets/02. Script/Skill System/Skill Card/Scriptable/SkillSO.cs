using UnityEngine;

/// <summary>
/// 스킬의 정보를 담은 스크립트 어블.
/// Params : skillName - 스킬 이름, cost - 비용,
/// description - 스킬 간단 설명, detailDescription - 스킬 상세 설명,
/// formula - 스킬 데미지 계산식 텍스트, skillIcon - 스킬 아이콘 스프라이트
/// skillLogicPrefab - 스킬 로직을 담은 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "Skill", menuName = "Skills/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int cost;
    [TextArea] public string description;
    [TextArea] public string detailDescription;
    [TextArea] public string formula;
    public Sprite skillIcon;
    public SkillBase skillLogicPrefab;
}
