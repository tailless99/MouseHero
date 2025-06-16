using UnityEngine;

public class UseSkillStackContainer : MonoBehaviour
{
    public SkillStackContainer skillStackContainer;
    public SkillDrawContainer skillDrawContainer;


    /// <summary>
    /// 스킬 스택의 스킬 카드를 사용하는 함수
    /// </summary>
    /// <param name="index">
    /// 슬롯의 순서에 따라 0~2까지의 파라메터를 받아서 넘김
    /// 0 - 첫 번째 스킬 사용
    /// 1 - 두 번째 스킬 사용
    /// 2 - 세 번째 스킬 사용
    /// </param>
    public void UseSkillCard(int index) => skillStackContainer.UseSkillCard(index);

    /// <summary>
    /// 버튼에 바인딩되는 스킬 카드를 추가하는 함수
    /// </summary>
    public void AddSkillCard() => skillStackContainer.AddSkillCard();

    // 스킬 스택 컨테이너를 반환
    public SkillStackContainer GetSkillStackContainer() => skillStackContainer;
}
