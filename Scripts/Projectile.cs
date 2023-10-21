using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectile��(�����Ǹ�û�е���)
/// </summary>
public class Projectile : MonoBehaviour
{
    static public Projectile projectile;
    public GameObject projectileGO;
    public float projectileSpeed;

    /// <summary>
    /// flagΪ-1��ʾ��enemyProjectile, 1��ʾplayerProjectile
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
    /// Ĭ���ӵ�����������string PlayerProjectile������
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

    

