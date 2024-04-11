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
    [Header("������ ������ Block")]
    public GameObject leftBlockPrefab;
    public GameObject rightBlockPrefab;
    [Header("������ Block ����")]
    public int leftBlockNum = 0;
    public int rightBlockNum = 0;
    public int totalBlockNum = 0;


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
        foreach (var i in blockList.OrderBy(item => item.Key))
        {
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
    /// �÷��̾� ��ġ�� Ŭ���� ��ư(L, R)�� string ���� �ָ� �°� �̵��ߴ��� return
    /// </summary>
    /// <param name="transform">�÷��̾��� ��ġ</param>
    /// <param name="move">L �Ǵ� R string����</param>
    /// <returns></returns>
    public bool MoveCheck(Transform transform, string move)
    {
        Transform leftPos = FindCloseLeftBlock().transform; // 1.5 // ���� Ʈ�������� 0
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
        else if (Instance != this)
        {
            //�̹� �����Ǿ��ִٸ� ���� ���� �� ����
            Destroy(this.gameObject);
        }
        //���� �Ѿ�� ������Ʈ ����
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
