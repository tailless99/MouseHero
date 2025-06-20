using UnityEngine;

public class Infinity_Rotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private void Update() {
        Roll();
    }

    private void Roll() {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
