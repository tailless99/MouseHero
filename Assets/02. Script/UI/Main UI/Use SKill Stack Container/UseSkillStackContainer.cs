using UnityEngine;

public class UseSkillStackContainer : MonoBehaviour
{
    public SkillStackContainer skillStackContainer;
    public SkillDrawContainer skillDrawContainer;


    /// <summary>
    /// ��ų ������ ��ų ī�带 ����ϴ� �Լ�
    /// </summary>
    /// <param name="index">
    /// ������ ������ ���� 0~2������ �Ķ���͸� �޾Ƽ� �ѱ�
    /// 0 - ù ��° ��ų ���
    /// 1 - �� ��° ��ų ���
    /// 2 - �� ��° ��ų ���
    /// </param>
    public void UseSkillCard(int index) => skillStackContainer.UseSkillCard(index);

    /// <summary>
    /// ��ư�� ���ε��Ǵ� ��ų ī�带 �߰��ϴ� �Լ�
    /// </summary>
    public void AddSkillCard() => skillStackContainer.AddSkillCard();

    // ��ų ���� �����̳ʸ� ��ȯ
    public SkillStackContainer GetSkillStackContainer() => skillStackContainer;
}
