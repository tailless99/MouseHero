using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public int minDropMoney = 1;
    public int maxDropMoney = 5;

    public void DropItem() {
        // ¾ò´Â °ñµå´Â º¸Á¤Ä¡ Áß¿¡ ·£´ýÇÏ°Ô È¹µæ
        int addMoney = Random.Range(minDropMoney, maxDropMoney + 1);
        MainUIContainer.Instance.UpdateMoney(addMoney);
    }
}
