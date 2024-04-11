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
    [Header("생성할 프리팹 Block")]
    public GameObject leftBlockPrefab;
    public GameObject rightBlockPrefab;

    [Header("생성할 Block 개수 설정")]
    public int blockSetting = 50;

    [Header("생성한 Block 개수")]
    public int leftBlockNum = 0;
    public int rightBlockNum = 0;
    public int totalBlockNum = 0;

    [Header("배경 이미지")]
    public GameObject background;
    public List<Sprite> imageLists;

    [Header("점수")]
    public int score;
    public TextMeshProUGUI scoreText;
    [Header("에너지 게이지")]
    public GameObject energyBar;
    public bool burnning = false;
    public bool fillAmountMax = false;



    /// <summary>
    /// 태그가 LeftBlock인 GameObject가 필요할 때 사용
    /// </summary>
    /// <returns>GameObject가 담긴 List를 반환</returns>
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
    /// 태그가 LeftBlock인 GameObject가 필요할 때 사용
    /// </summary>
    /// <returns>GameObject가 담긴 List를 반환</returns>
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


        //나중에 else if(버닝) 등으로 게이지 꽉 찰 시 무조건 true만 반환하게 하면 될 듯

        return false;
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

    /// <summary>
    /// 처음 시작 시 랜덤한 블럭 생성
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
    /// 마지막 블럭 기준으로 랜덤한 블럭 생성
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
    /// 끝에 있는 블럭의 위치를 확인할 수 있다.
    /// </summary>
    /// <returns>Transform으로 반환함</returns>
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
    /// 에너지 이미지의 fillAmount가 1이 되면 필드의 fillAmount true로 바꿔준다
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
    /// burnning을 false로 바꾸고 색을 빨간색으로 변경한다. 10초 뒤 다시 색이 변한다.
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
