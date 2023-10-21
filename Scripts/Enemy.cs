using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    static public Enemy enemy;

    [Header("基本参数")]
    public GameObject enemyPrefab;
    public float enemyHP;
    public float enemySpeed;
    public float AttackATM;
    public float value;
    public bool flag = true;   //状态，即从屏幕外进来之前都为false，为true时才能开火和受到伤害
    public float fireInterval;  //开火间隔时间
    private float fireTimeCounter;  //开火冷却计时器
    public GameObject enemyProjectile;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            enemyHP -= Enemy.enemy.AttackATM;
        }
    }

    private void Start()
    {
        enemy = this;
        enemyPrefab = this.gameObject;
    }

    private void Update()
    {

        DestroyEnemy();
        Fire();
    }
    private void FixedUpdate()
    {
        EnemyMove();
    }

    /// <summary>
    /// 方法：敌人从左往右移动
    /// </summary>
    public virtual void EnemyMove()
    {
        Vector3 speedDir = new Vector3(-1, 0, 0);
        this.transform.position += speedDir * enemySpeed * Time.deltaTime;
    }

    /// <summary>
    /// 方法：敌人的摧毁，出了摄像机范围或者hp归0
    /// </summary>
    public virtual void DestroyEnemy()
    {
        if(enemyHP <= 0)
        {
            Destroy(enemyPrefab);
        }

        if(enemyPrefab.transform.position.x + 3f < Utils.buttomLeft.x)
        {
            Destroy(enemyPrefab);
        }


    }

    public void Fire()
    {
        if (flag)
        {
            if (Time.time - fireTimeCounter >= fireInterval)
            {
                GameObject go = Instantiate(enemyProjectile);
                Vector3 pos = this.transform.position;
                go.transform.position = pos;

                fireTimeCounter = Time.time;
            }

        }
    }

    
    

}
