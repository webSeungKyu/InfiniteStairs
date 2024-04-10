using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;


    public void LeftBlockList()
    {
        Dictionary<int, GameObject> blockList = new Dictionary<int, GameObject>();
        List<GameObject> block = new List<GameObject>(GameObject.FindGameObjectsWithTag("LeftBlock"));
        
        for (int i = 0; i < block.Count; i++)
        {
            int blockNum = Convert.ToInt32(block[i].name.Replace("LeftBlock", ""));
            blockList.Add(blockNum, block[i]);
        }


        foreach (var i in  blockList.OrderBy(item => item.Key))
        {
            Debug.Log(i);
        }
    }
    public void RightBlockList()
    {
        List<GameObject> block = new List<GameObject>(GameObject.FindGameObjectsWithTag("RightBlock"));
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            //�̹� �����Ǿ��ִٸ� ���� ���� �� ����
            Destroy(this.gameObject);
        }
        //���� �Ѿ�� ������Ʈ ����
        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
