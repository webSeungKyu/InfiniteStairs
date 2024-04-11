using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;
    GameObject player;
    [Header("������ ������ Block")]
    public GameObject leftBlockPrefab;
    public GameObject rightBlockPrefab;

    [Header("������ Block ���� ����")]
    public int blockSetting = 50;

    [Header("������ Block ����")]
    public int leftBlockNum = 0;
    public int rightBlockNum = 0;
    public int totalBlockNum = 0;

    [Header("��� �̹���")]
    public GameObject background;
    public List<Sprite> imageLists;

    [Header("����")]
    public int score;
    public TextMeshProUGUI scoreText;
    [Header("������ ������")]
    public GameObject energyBar;
    public bool burnning = false;
    public bool fillAmountMax = false;



    /// <summary>
    /// �±װ� LeftBlock�� GameObject�� �ʿ��� �� ���
    /// </summary>
    /// <returns>GameObject�� ��� List�� ��ȯ</returns>
    public List<GameObject> LeftBlockList()
    {
        Dictionary<int, GameObject> blockList = new Dictionary<int, GameObject>();
        List<GameObject> block = new List<GameObject>(GameObject.FindGameObjectsWithTag("LeftBlock"));

        for (int i = 0; i < block.Count; i++)
        {
            int blockNum = Convert.ToInt32(block[i].name);
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
    /// �±װ� LeftBlock�� GameObject�� �ʿ��� �� ���
    /// </summary>
    /// <returns>GameObject�� ��� List�� ��ȯ</returns>
    public List<GameObject> RightBlockList()
    {
        Dictionary<int, GameObject> blockList = new Dictionary<int, GameObject>();
        List<GameObject> block = new List<GameObject>(GameObject.FindGameObjectsWithTag("RightBlock"));

        for (int i = 0; i < block.Count; i++)
        {
            int blockNum = Convert.ToInt32(block[i].name);
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
        Transform leftPos = FindCloseLeftBlock().transform;
        Transform rightPos = FindCloseRightBlock().transform;

        if (move.Equals("L"))
        {
            if ((transform.position.y + leftPos.position.y) < (transform.position.y + rightPos.position.y))
            {
                score++;
                if (!fillAmountMax)
                {
                    energyBar.GetComponent<Image>().fillAmount += 0.03f;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        else if (move.Equals("R"))
        {
            if ((transform.position.y + rightPos.position.y) < (transform.position.y + leftPos.position.y))
            {
                score++;
                if (!fillAmountMax)
                {
                    energyBar.GetComponent<Image>().fillAmount += 0.03f;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        //���߿� else if(����) ������ ������ �� �� �� ������ true�� ��ȯ�ϰ� �ϸ� �� ��

        return false;
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

    /// <summary>
    /// ó�� ���� �� ������ �� ����
    /// </summary>
    public void InstantiateBlock()
    {
        Vector2 pos = player.transform.position;


        for (int i = 1; i <= blockSetting; i++)
        {
            int ranNum = Random.Range(0, 2);
            if (ranNum > 0)
            {
                pos = new Vector2(pos.x - 1.5f, pos.y + 1.5f);
                Instantiate(leftBlockPrefab, pos, Quaternion.identity).name = i.ToString();
            }
            else
            {
                pos = new Vector2(pos.x + 1.5f, pos.y + 1.5f);
                Instantiate(rightBlockPrefab, pos, Quaternion.identity).name = i.ToString();
            }
            totalBlockNum++;

        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InstantiateBlock();
        InvokeRepeating("InfiniteBlockSetting", 1f, 1f);
        energyBar.GetComponent<Image>().fillAmount = 0;
    }

    /// <summary>
    /// ������ �� �������� ������ �� ����
    /// </summary>
    void InfiniteBlockSetting()
    {
        leftBlockNum = LeftBlockList().Count;
        rightBlockNum = RightBlockList().Count;

        if (blockSetting > leftBlockNum + rightBlockNum)
        {
            int temp = totalBlockNum + 1;
            Vector2 pos = MaxBlockReturn().position;
            for (int i = temp; i <= temp + blockSetting; i++)
            {
                int ranNum = Random.Range(0, 2);
                if (ranNum > 0)
                {
                    pos = new Vector2(pos.x - 1.5f, pos.y + 1.5f);
                    Instantiate(leftBlockPrefab, pos, Quaternion.identity).name = i.ToString();
                }
                else
                {
                    pos = new Vector2(pos.x + 1.5f, pos.y + 1.5f);
                    Instantiate(rightBlockPrefab, pos, Quaternion.identity).name = i.ToString();
                }
                totalBlockNum++;
            }
        }
    }

    /// <summary>
    /// ���� �ִ� ���� ��ġ�� Ȯ���� �� �ִ�.
    /// </summary>
    /// <returns>Transform���� ��ȯ��</returns>
    Transform MaxBlockReturn()
    {
        int MaxBlcok;
        Transform pos;
        int leftMaxBlockNum = Convert.ToInt32(LeftBlockList()[leftBlockNum - 1].name);
        int rigthMaxBlockNum = Convert.ToInt32(RightBlockList()[rightBlockNum - 1].name);
        MaxBlcok = Math.Max(leftMaxBlockNum, rigthMaxBlockNum);
        pos = GameObject.Find(MaxBlcok.ToString()).transform;
        return pos;
    }

    void Update()
    {
        scoreText.text = score.ToString();
        StartCoroutine("BurnningCheck");
        if (burnning)
        {
            StartCoroutine("Burning");
        }
    }

    /// <summary>
    /// ������ �̹����� fillAmount�� 1�� �Ǹ� �ʵ��� fillAmount true�� �ٲ��ش�
    /// </summary>
    /// <returns></returns>
    IEnumerator BurnningCheck()
    {
        if (energyBar.GetComponent<Image>().fillAmount == 1)
        {
            burnning = true;
            fillAmountMax = true;
        }
        yield return null;
    }

    /// <summary>
    /// burnning�� false�� �ٲٰ� ���� ���������� �����Ѵ�. 10�� �� �ٽ� ���� ���Ѵ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator Burning()
    {
        energyBar.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(10);
        energyBar.GetComponent<Image>().fillAmount = 0;
        energyBar.GetComponent<Image>().color = Color.white;
        burnning = false;
        fillAmountMax = false;
        

    }
}
