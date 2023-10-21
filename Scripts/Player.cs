using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player player;

    [Header("基本参数")]
    public float xPlayerSpeed;
    public float yPlayerSpeed;
    private float _playHP = 1000;
    public float currentPlayerHP;
    public float currentMoney;

    [Header("无敌相关")]
    public float invincibleDuration;
    public bool isInvincible;
    private float invincibleCounter;

    [Header("闪避相关")]
    public float dodgeSpeed;
    public float dodgeDuration;
    public bool isdodge;
    private float dodgeCounter;


    [Header("Player子弹参数")]
    public GameObject playerProjectile;
    public float fireInterval;  //开火间隔时间
    private float fireTimeCounter;  //开火冷却计时器



    

    private void Awake()
    {
        currentPlayerHP = _playHP;
        player = this;
    }

    private void Update()
    {
        playerFire();
        playerInvincible();
        ColliderControll();
    }

    private void FixedUpdate()
    {
        LimitMove();
        playerMove();
        playerDodge();
    }
    /// <summary>
    /// 方法：控制玩家移动
    /// </summary>
    public void playerMove()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = new Vector3(xAxis * xPlayerSpeed, yAxis * yPlayerSpeed, 0);
        this.transform.position += pos;
    }

    /// <summary>
    /// 方法：控制玩家开火
    /// </summary>
    public void playerFire()
    {
        if(Input.GetKey(KeyCode.X))
        {
            if(Time.time - fireTimeCounter >= fireInterval)
            {
                //创建player的子弹
                GameObject go = Instantiate(playerProjectile);
                Vector3 pos = this.transform.position;
                go.transform.position = this.transform.position;
                Projectile.projectile.ProjectileMove("PlayerProjectile");

                fireTimeCounter = Time.time;
            }
        }
    }

    /// <summary>
    /// 玩家闪避
    /// </summary>
    public void playerDodge()
    {
        if (isdodge)
        {
            if(Time.time - dodgeCounter >= dodgeDuration)
            {
                isdodge = false;
                isInvincible = false;
            }
            return;
        }
        if (Input.GetKey(KeyCode.C))
        {
            Vector3 speedDir = Vector3.zero; 
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                speedDir.x += -1;
            } 
            if (Input.GetKey(KeyCode.RightArrow))
            {
                speedDir.x += 1;
            } 
            if (Input.GetKey(KeyCode.UpArrow))
            {
                speedDir.y += 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                speedDir.y += -1;
            }
            if (speedDir == Vector3.zero) speedDir.x = -1f;
            this.transform.position += speedDir.normalized * dodgeSpeed * Time.deltaTime;
            dodgeCounter = Time.time;
            isInvincible = true;
            isdodge = true;
        }
    }

    

    /// <summary>
    /// 限制移动在屏幕内
    /// </summary>
    public void LimitMove()
    {
        Vector3 offmove = Vector3.zero;
        Bounds playerBound = this.GetComponent<SpriteRenderer>().bounds;
        if(playerBound.min.y <= Utils.buttomLeft.y)
        {
            offmove.y += Utils.buttomLeft.y - playerBound.min.y;
        }
        if(playerBound.max.y >= Utils.topRight.y)
        {
            offmove.y += Utils.topRight.y - playerBound.max.y;
        }
        if(playerBound.min.x <= Utils.buttomLeft.x)
        {
            offmove.x += Utils.buttomLeft.x - playerBound.min.x;
        }
        if(playerBound.max.x >= Utils.topRight.x)
        {
            offmove.x += Utils.topRight.x - playerBound.max.x;
        }
        /*Debug.Log(offmove);*/
        this.transform.position += offmove;
    }

    /// <summary>
    /// 玩家受伤的方法写在ontriggerenter里面
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible) return;
        if (collision.tag == "EnemyProjectile" || collision.tag == "Enemy")
        {
            if (collision.tag == "EnemyProjectile")
            {
                currentPlayerHP -= Enemy.enemy.AttackATM;
            }
            else if (collision.tag == "Enemy")
            {
                currentPlayerHP -= Enemy.enemy.AttackATM;
            }
            invincibleCounter = Time.time;
            isInvincible = true;
        }
    }

    /// <summary>
    /// 无敌控制
    /// </summary>
    public void playerInvincible()
    {
        if (isInvincible)
        {
            if (Time.time - invincibleCounter >= invincibleDuration)
            {
                isInvincible = false;
            }
        }
    }

    /// <summary>
    /// 碰撞体控制
    /// </summary>
    public void ColliderControll()
    {
        Collider2D col = this.GetComponent<CapsuleCollider2D>();
        if (isdodge || isInvincible)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }

    }


    public void PlayerDead()
    {

    }
}
