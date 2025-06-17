using UnityEngine;

public class ThrowGrenade : SkillBase
{
    [SerializeField] private GameObject grenadePrefab;

    public override bool Use() {
        // ����ź ��ȯ
        Instantiate(grenadePrefab, transform.position, Quaternion.identity);

        return true;
    }
}
