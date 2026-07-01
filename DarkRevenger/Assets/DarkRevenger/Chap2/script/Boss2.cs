using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject prfHpBar;
    public bool dead = false;
    public GameObject canvas;
    RectTransform hpBar;
    public float height = 1.7f;
    public Player player;
    public PlayerAttack playerAttack;
    public Animator enemyAnimator;
    public Status status;
    Image nowHpbar;
    public UnitCode unitCode;
    public string targetTextName = "BSHP";

    void Start()
    {
        status = new Status();
        status = status.SetUnitStatus(unitCode);
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();

        hpBar.anchorMin = new Vector2(0.5f, 1f);
        hpBar.anchorMax = new Vector2(0.5f, 1f);
        hpBar.pivot = new Vector2(0.5f, 1f);
        hpBar.anchoredPosition = new Vector2(0, -150);
    }
    void Update()
    {
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
        dead = true;
        GetComponent<Boss2AI>().enabled = false;    // 추적 비활성화
        enemyAnimator.SetTrigger("die");            // die 애니메이션 실행
        GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
        Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
        Destroy(hpBar.gameObject, 3);               // Hp바 제거

        Transform targetTextTransform = canvas.transform.Find(targetTextName);
        if (targetTextTransform != null)
        {
            Destroy(targetTextTransform.gameObject, 3);
        }
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        GameObject sceneChangeObj = GameObject.Find("SceneChange");
        if (sceneChangeObj != null)
        {
            BoxCollider2D sceneChangeCollider = sceneChangeObj.gameObject.GetComponent<BoxCollider2D>();
            if (sceneChangeCollider != null)
            {
                sceneChangeCollider.isTrigger = true;
            }
        }
    }
    public void Destroygameobject(){
        Destroy(gameObject);
    }
}