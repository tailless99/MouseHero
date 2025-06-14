using UnityEngine;

/// <summary>
/// ��ų�� ������ ���� ��ũ��Ʈ ���.
/// Params : skillName - ��ų �̸�, cost - ���,
/// description - ��ų ���� ����, detailDescription - ��ų �� ����,
/// formula - ��ų ������ ���� �ؽ�Ʈ, skillIcon - ��ų ������ ��������Ʈ
/// skillLogicPrefab - ��ų ������ ���� ������Ʈ
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
