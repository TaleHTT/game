using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile类(可能是个没有的类)
/// </summary>
public class Projectile : MonoBehaviour
{
    static public Projectile projectile;
    public GameObject projectileGO;
    public float projectileSpeed;

    /// <summary>
    /// flag为-1表示是enemyProjectile, 1表示playerProjectile
    /// </summary>
    public float flag = -1;

    private void Awake()
    {
        projectile = this;
        projectileGO = this.gameObject;
    }

    private void Update()
    {
        Utils.OutScreenDestroy(this.gameObject);
        ProjectileMove();
    }

    /// <summary>
    /// 默认子弹方向向左传入string PlayerProjectile后向右
    /// </summary>
    /// <param name="str"></param>
    public void ProjectileMove(string str = null)
    {
        if (str == "PlayerProjectile") flag = 1;
        Vector3 speedDir = new Vector3(1, 0, 0);
        this.transform.position += flag * speedDir * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(flag == -1)
        {
            if(collision.tag == "Player")
            {
                Destroy(this.gameObject);
            }
        }
        else if (flag == 1)
        {
            if(collision.tag == "Enemy")
            {
                Destroy(this.gameObject);
            }
        }
    }
}

    

