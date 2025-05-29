using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{
    [Header("Player Attack Effect")]
    [SerializeField] public GameObject frogBaseAttack_Prefab;

    [Header("UI Effect")]
    [SerializeField] public GameObject damageText_Prefab;

    [Header("Enemy")]
    [SerializeField] public GameObject virtualGuy_Prefab;

    // ������Ʈ Ǯ�� ����Ʈ
    [SerializeField] GameObject[] frogBaseAttack;
    [SerializeField] GameObject[] damageText;
    [SerializeField] GameObject[] virtualGuy;
    
    // return List
    [SerializeField] GameObject[] targetPool;


    private void Start() {
        damageText = new GameObject[200];

        Generate();
    }

    private void Generate() {
        // Player Base Attack
        PlayerBaseAttackGenerate(PlayerController.Instance.GetCharacter().gameObject.name);

        // UI
        for (int i = 0; i < damageText.Length; i++) {
            damageText[i] = Instantiate(damageText_Prefab);
            damageText[i].SetActive(false);
        }
        // Enemy
        for (int i = 0; i < virtualGuy.Length; i++) {
            virtualGuy[i] = Instantiate(virtualGuy_Prefab);
            virtualGuy[i].SetActive(false);
        }

        // Item

    }

    // �÷��� ���� ĳ���� ���� ���� ������ Ȱ��ȭ
    private void PlayerBaseAttackGenerate(string name) {
        switch (name) {
            case "NinjaFrog":
                // Player Base Attack
                frogBaseAttack = new GameObject[100];
                for (int i = 0; i < frogBaseAttack.Length; i++) {
                    frogBaseAttack[i] = Instantiate(frogBaseAttack_Prefab);
                    frogBaseAttack[i].SetActive(false);
                }
                break;
        }
        
    }

    // Ȱ��ȭ�� ���� ������Ʈ�� ��ȯ
    public GameObject MakeObj(string type) {
        // �ѱ� ����Ʈ�� ����
        switch (type) {
            case "NinjaFrog":
                targetPool = frogBaseAttack;
                break;
            case "Damage":
                targetPool = damageText;
                break;
            case "VirtualGuy":
                targetPool = virtualGuy;
                break;
        }

        // ����Ʈ�� ��Ȱ��ȭ �� ������Ʈ�� Ȱ��ȭ �� ��ȯ
        for (int i = 0; i < targetPool.Length; i++) {
            if (!targetPool[i].activeSelf) {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}
