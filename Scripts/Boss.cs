using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    static public bool bossFlag = false;
    private float leftBound;

    /// <summary>
    /// 重写移动方法，对于boss，到位后就不会移动
    /// </summary>
    public override void EnemyMove()
    {
        leftBound = this.gameObject.transform.position.x + this.GetComponent<SpriteRenderer>().bounds.extents.x;
        if (leftBound <= Utils.topRight.x)
        {
            Debug.Log("Boss Stop");
            Debug.Log(leftBound);
            return;
        }
        base.EnemyMove();
    }

    public override void DestroyEnemy()
    {
        if(enemyHP <= 0)
        {
            bossFlag = false;
        }
        base.DestroyEnemy();
    }

}
