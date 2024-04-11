using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;


    /// <summary>
    /// 태그가 LeftBlock인 List<GameObject>가 필요할 때 사용
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
    /// 태그가 RightBlock인 List<GameObject>가 필요할 때 사용
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
    /// 플레이어와 가장 가까운 LeftBlock를 찾는다
    /// </summary>
    /// <returns>GameObject(LeftBlock)</returns>
    public GameObject FindCloseLeftBlock()
    {
        List<GameObject> list = LeftBlockList();

        return list[0];
    }
    /// <summary>
    /// 플레이어와 가장 가까운 RightBlock를 찾는다
    /// </summary>
    /// <returns>GameObject(RightBlock)</returns>
    public GameObject FindCloseRightBlock()
    {
        List<GameObject> list = RightBlockList();

        return list[0];
    }

    /// <summary>
    /// GameObject의 태그를 변경할 때 사용
    /// </summary>
    /// <param name="gameObject">Block로 바뀐다</param>
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
            //이미 생성되어있다면 새로 만든 거 삭제
            Destroy(this.gameObject);
        }
        //씬이 넘어가도 오브젝트 유지
        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
