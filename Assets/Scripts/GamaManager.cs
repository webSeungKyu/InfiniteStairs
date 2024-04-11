using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;
    GameObject player;
    [Header("생성할 프리팹 Block")]
    public GameObject leftBlockPrefab;
    public GameObject rightBlockPrefab;
    [Header("생성한 Block 개수")]
    public int leftBlockNum = 0;
    public int rightBlockNum = 0;
    public int totalBlockNum = 0;


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
        foreach (var i in blockList.OrderBy(item => item.Key))
        {
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
    /// 플레이어 위치와 클릭한 버튼(L, R)의 string 값을 주면 맞게 이동했는지 return
    /// </summary>
    /// <param name="transform">플레이어의 위치</param>
    /// <param name="move">L 또는 R string으로</param>
    /// <returns></returns>
    public bool MoveCheck(Transform transform, string move)
    {
        Transform leftPos = FindCloseLeftBlock().transform; // 1.5 // 만약 트랜스폼은 0
        Transform rightPos = FindCloseRightBlock().transform; // 3

        if (move.Equals("L"))
        {
            if ((transform.position.y + leftPos.position.y) < (transform.position.y + rightPos.position.y))
            {
                return true;
            }else
            {
                return false;
            }
        }
        else if(move.Equals("R"))
        {
            if ((transform.position.y + rightPos.position.y) < (transform.position.y + leftPos.position.y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        



        return true;
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
        else if (Instance != this)
        {
            //이미 생성되어있다면 새로 만든 거 삭제
            Destroy(this.gameObject);
        }
        //씬이 넘어가도 오브젝트 유지
        DontDestroyOnLoad(this.gameObject);

    }


    public void InstantiateBlock()
    {
        int ranStartNum = Random.Range(0, 2);
        Vector2 pos = player.transform.position;


        for (int i = 0; i < 20; i++)
        {
            int ranNum = Random.Range(0, 2);
            if (ranNum > 0)
            {
                leftBlockNum++;
                pos = new Vector2(pos.x - 1.5f, pos.y + 1.5f);
                Instantiate(leftBlockPrefab, pos, Quaternion.identity).name = leftBlockPrefab.name + $"{i}";

            }
            else
            {
                rightBlockNum++;
                pos = new Vector2(pos.x + 1.5f, pos.y + 1.5f);
                Instantiate(rightBlockPrefab, pos, Quaternion.identity).name = rightBlockPrefab.name + $"{i}";
            }


        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InstantiateBlock();

    }


    void Update()
    {

    }
}
