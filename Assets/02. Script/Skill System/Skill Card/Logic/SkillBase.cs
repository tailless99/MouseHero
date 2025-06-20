using System.Collections;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    // ������� ��� ��ų ���
    public abstract bool Use();

    /// <summary>
    /// �� ��ų�� �ڷ�ƾ(�񵿱�) ������� �۵��ϴ� ��ų���� ���θ� ��Ÿ���� ������Ƽ 
    /// �ڽ� Ŭ�������� override �Ͽ� true/false�� ����
    /// </summary>
    public virtual bool IsCoroutineSkill => false; // �⺻���� false (���� ��ų)

    /// <summary>
    /// �񵿱� ��ų�� �����ؾ� �� �ڷ�ƾ �޼���
    /// IsCoroutineSkill�� true�� ��ų�� �� �޼��带 �����Ѵ�.
    /// </summary>
    public virtual IEnumerator UseCoroutine(System.Action<bool> onClick) {
        onClick?.Invoke(false); // ��������Ʈ ����Ʈ
        yield break; // return; ��� ����ϴ� ����
    }
}
