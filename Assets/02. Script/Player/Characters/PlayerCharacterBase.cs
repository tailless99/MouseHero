using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterBase : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] protected float hp = 1f;
    [SerializeField] protected float strength = 1f;
    [SerializeField] protected float defence = 1f;
    [SerializeField] protected float luck = 1f;

    [Header("Player Skill")]
    [SerializeField] protected string skill_JSON_Name;

    [Header("Player Attack Base")]
    [SerializeField] protected float attackMaxCoolTime = 0.1f;

    [Header("Use Character Setteing")]
    [SerializeField] private string CharacterName;


    // ������Ʈ ����
    private Player_Input_Action playerInput;
    private PlayerApplyDamage playerAttack;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    // ��� ���� ����
    private float currentAttackCoolTime = 0f;


    private void Awake() {
        playerInput = new Player_Input_Action();
        playerAttack = GetComponent<PlayerApplyDamage>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    private void Start() {
        PlayerController.Instance.SetCurrentCharacter(this);

        // Input system Button ����
        playerInput.Enable();
        
        // Z, X, LBMouse ��� ������ �ϴ� ����� ������, ���ݿ� ��Ÿ�� �ʿ��� Ŭ��Ŀ �����̱� ������ ���� ����� ������ Ű�� �����ߴ�.
        playerInput.Attack.Z.performed += _ => Attack();
        playerInput.Attack.X.performed += _ => Attack();
        playerInput.Attack.LeftMouse.performed += _ => Attack();


        // �������ͽ� ����
        PlayerStatusManager.Instance.SetCharacterStatus(hp, strength, defence, luck);

        // ��ų �Ŵ��� �ʱ�ȭ
        SkillManager.Instance.InitializeSKillManager(CharacterName);
    }

    protected virtual void Update() {
        LookMouse();

        if (currentAttackCoolTime > 0) {
            currentAttackCoolTime -= Time.unscaledDeltaTime;
        }
    }

    // ĳ���Ͱ� ���콺 ������ �ٶ󺸵��� Flip�ϴ� ���
    private void LookMouse() {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (mousePos - this.gameObject.transform.position).normalized;
        if (dir.x < 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }

    // �ڽ� Ŭ�������� �����ǵǴ� ���� �Լ�
    public virtual void Attack() {
        if (currentAttackCoolTime > 0) return;

        // ��Ÿ�� ����
        currentAttackCoolTime = attackMaxCoolTime;

        // ���� ����Ʈ ��ġ ����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 spawnPos = mousePos;
        spawnPos.x += Random.Range(-0.3f, 0.3f); // ���� ����ġ �߰�
        spawnPos.y += Random.Range(-0.3f, 0.3f); // ���� ����ġ �߰�
        spawnPos.z = 0;

        var attackEffect = ObjectPoolingManager.Instance.MakeObj(CharacterName);
        attackEffect.transform.position = spawnPos;
        attackEffect.transform.rotation = Quaternion.identity;

        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0.1f, LayerMask.GetMask("Enemy"));
        if(ray) {
            if (ray.collider.gameObject.TryGetComponent<EnemyHitBox>(out EnemyHitBox enemyHitBox)) {
                float damage = playerAttack.GetAttackBaseDamage();
                enemyHitBox.TakeDamage(damage, spawnPos, playerAttack.IsCriticalAttack());
            }
        }
    }

    // ������ ó�� Ŭ���� ��ȯ
    public PlayerApplyDamage GetPlayerApplyDamageClass() => playerAttack;
}
