using UnityEngine;

public class Destroy_Self : MonoBehaviour
{
    public void DestroySelf() {
        Destroy(this.gameObject);
    }
}
