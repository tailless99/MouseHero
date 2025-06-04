using UnityEngine;

public class SkillShopUIContainer : MonoBehaviour
{
    [SerializeField] SkillDetaill skillDetaill;

    public void ShowDetailUI(SkillCardSlot cardBase) {
        Debug.Log(cardBase.skillSO.skillName);
        var data = cardBase.skillSO;
        skillDetaill.Initialize_DetaillView(
            data.skillIcon, data.skillName, data.detailDescription, data.formula, data.cost);
    }
}
