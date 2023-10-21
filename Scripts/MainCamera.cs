using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public List<GameObject> prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;    //ÿ�����ɵ�������
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
    /// ������������������Ҳ�����
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

        //�ӵл��б���������һ������ʼ��
        int index = Random.Range(0, prefabEnemies.Count);
        GameObject go = Instantiate(prefabEnemies[index]);

        //���˳�ʼ����λ��
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
