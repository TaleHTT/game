using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public List<GameObject> prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;    //每秒生成敌人数量
    public GameObject PlayerPrefab;
    /*public float timeBeforeStart;*/

    public int numCounter;
    public GameObject boss;


    private void Start()
    {
        GameObject go = Instantiate(PlayerPrefab);
        SpawnEnemy();
    }


    /// <summary>
    /// 方法：敌人在摄像机右侧生成
    /// </summary>
    public void SpawnEnemy()
    {
        numCounter++;
        if(numCounter >= 10 && !Boss.bossFlag)
        {
            Boss.bossFlag = true;
            numCounter = 0;
            Instantiate(boss);
        }

        //从敌机列表中随机获得一个并初始化
        int index = Random.Range(0, prefabEnemies.Count);
        GameObject go = Instantiate(prefabEnemies[index]);

        //敌人初始化的位置
        Bounds goBound = go.gameObject.GetComponent<SpriteRenderer>().bounds;
        Vector3 pos = Vector3.zero;
        float yMin = Utils.buttomLeft.y + goBound.extents.y;
        float yMax = Utils.topRight.y - goBound.extents.y;
        pos.y = Random.Range(yMin, yMax);
        pos.x = Utils.topRight.x + goBound.extents.x + 1f;
        go.transform.position = pos;

        /*Debug.Log("yMin = " + yMin);
        Debug.Log("yMax = " + yMax);
        Debug.Log("pos.x = " + pos.x);
        Debug.Log("goBound.extents = " + goBound.extents);*/
        
        Invoke("SpawnEnemy", enemySpawnPerSecond);
    }

   
}
