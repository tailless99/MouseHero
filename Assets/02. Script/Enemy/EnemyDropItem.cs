using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public int minDropMoney = 1;
    public int maxDropMoney = 5;
    public float GiveExp = 5f;

    public void DropItem() {
        // ¾ò´Â °ñµå´Â º¸Á¤Ä¡ Áß¿¡ ·£´ýÇÏ°Ô È¹µæ
        int addMoney = Random.Range(minDropMoney, maxDropMoney + 1);
        MainUIContainer.Instance.UpdateMoney(addMoney);
    }

    // »ç¸Á½Ã ÇÃ·¹ÀÌ¾î°¡ È¹µæÇÒ °æÇèÄ¡¸¦ ¹ÝÈ¯ÇÑ´Ù.
    public float GetExp() => GiveExp;
}
