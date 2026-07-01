using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;
    RectTransform hpBar;
    public float height = 1.7f;
    public Player player;
    public PlayerAttack playerAttack;
    public Animator enemyAnimator;
    public Status status;
    Image nowHpbar;
    public UnitCode unitCode;

    void Start()
    {
        status = new Status();
        status = status.SetUnitStatus(unitCode);
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }
    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)status.nowHp / (float)status.maxHp;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (playerAttack.attacked)
            {
                status.nowHp -= player.status.atkDmg;
                player.attacked = false;
                if (status.nowHp <= 0) // 적 사망
                {
                    Die();
                }
            }
        }
    }
    void Die()
    {
        enemyAnimator.SetTrigger("die");            // die 애니메이션 실행
        GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
        Destroy(hpBar.gameObject, 3);               // Hp바 제거
        Destroy(gameObject, 3);
    }
}