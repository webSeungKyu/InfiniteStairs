using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;


    /// <summary>
    /// �±װ� LeftBlock�� List<GameObject>�� �ʿ��� �� ���
    /// </summary>
    /// <returns>List<GameObject></returns>
    public List<GameObject> LeftBlockList()
    {
        Dictionary<int, GameObject> blockList = new Dictionary<int, GameObject>();
        List<GameObject> block = new List<GameObject>(GameObject.FindGameObjectsWithTag("LeftBlock"));
        
        for (int i = 0; i < block.Count; i++)
        {
            int blockNum = Convert.ToInt32(block[i].name.Replace("LeftBlock", ""));
            blockList.Add(blockNum, block[i]);
        }

        List<GameObject> returnList = new List<GameObject>();
        foreach (var i in  blockList.OrderBy(item => item.Key))
        {
            Debug.Log(i);
            returnList.Add(i.Value);
        }
        return returnList;
    }

    /// <summary>
    /// �±װ� RightBlock�� List<GameObject>�� �ʿ��� �� ���
    /// </summary>
    /// <returns>List<GameObject></returns>
    public List<GameObject> RightBlockList()
    {
        Dictionary<int, GameObject> blockList = new Dictionary<int, GameObject>();
        List<GameObject> block = new List<GameObject>(GameObject.FindGameObjectsWithTag("RightBlock"));

        for (int i = 0; i < block.Count; i++)
        {
            int blockNum = Convert.ToInt32(block[i].name.Replace("RightBlock", ""));
            blockList.Add(blockNum, block[i]);
        }

        List<GameObject> returnList = new List<GameObject>();
        foreach (var i in blockList.OrderBy(item => item.Key))
        {
            Debug.Log(i);
            returnList.Add(i.Value);
        }
        return returnList;
    }

    /// <summary>
    /// �÷��̾�� ���� ����� LeftBlock�� ã�´�
    /// </summary>
    /// <returns>GameObject(LeftBlock)</returns>
    public GameObject FindCloseLeftBlock()
    {
        List<GameObject> list = LeftBlockList();

        return list[0];
    }
    /// <summary>
    /// �÷��̾�� ���� ����� RightBlock�� ã�´�
    /// </summary>
    /// <returns>GameObject(RightBlock)</returns>
    public GameObject FindCloseRightBlock()
    {
        List<GameObject> list = RightBlockList();

        return list[0];
    }

    /// <summary>
    /// GameObject�� �±׸� ������ �� ���
    /// </summary>
    /// <param name="gameObject">Block�� �ٲ��</param>
    public void ChangeTag(GameObject gameObject)
    {
        gameObject.tag = "Block";
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
