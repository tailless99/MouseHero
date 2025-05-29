using UnityEngine;

public class FrogAttack : MonoBehaviour
{
    public void EndAnimDestroySelf() {
        this.gameObject.SetActive(false);
    }
}
