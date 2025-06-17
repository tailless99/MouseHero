using UnityEngine;

public class ThrowGrenade : SkillBase
{
    [SerializeField] private GameObject grenadePrefab;

    public override bool Use() {
        // 수류탄 소환
        Instantiate(grenadePrefab, transform.position, Quaternion.identity);

        return true;
    }
}
