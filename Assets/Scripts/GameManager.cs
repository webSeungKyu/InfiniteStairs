using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    GameObject player;
    [Header("생성할 프리팹 Block")]
    public GameObject leftBlockPrefab;
    public GameObject rightBlockPrefab;
    public GameObject goldPrefab;

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
    public int gold;
    public TextMeshProUGUI goldText;
    [Header("에너지 게이지")]
    public GameObject energyBar;
    public bool burnning = false;
    public bool fillAmountMax = false;
    [Header("버튼과 이미지")]
    public GameObject leftButton;
    public GameObject rightButton;
    public Sprite basicButtonImage;
    public Sprite clickButtonImage;
    [Header("일시정지 활성화")]
    public bool pause = false;
    public GameObject pauseImage;
    [Header("음소거 버튼 이미지")]
    public Sprite soundOnButtonImage;
    public Sprite soundOffButtonImage;
    [Header("종료 관련")]
    public GameObject imageQuit;






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
    /// transform을 받아 가장 가까운 블럭을 찾아 string의 L 또는 R 로 반환해준다
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public string CloseBlock(Transform transform)
    {
        Transform leftPos = FindCloseLeftBlock().transform;
        Transform rightPos = FindCloseRightBlock().transform;
        if ((transform.position.y + leftPos.position.y) < (transform.position.y + rightPos.position.y))
        {
            return "L";
        }
        else
        {
            return "R";
        }

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
                GoldRandomInstantiate(pos);
            }
            else
            {
                pos = new Vector2(pos.x + 1.5f, pos.y + 1.5f);
                Instantiate(rightBlockPrefab, pos, Quaternion.identity).name = i.ToString();
                GoldRandomInstantiate(pos);
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
        GameObject.FindGameObjectWithTag("Menu").SetActive(false);
        InvokeRepeating("RemoveBlock", 10f, 20f);
        imageQuit.SetActive(false);
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
                    GoldRandomInstantiate(pos);
                }
                else
                {
                    pos = new Vector2(pos.x + 1.5f, pos.y + 1.5f);
                    Instantiate(rightBlockPrefab, pos, Quaternion.identity).name = i.ToString();
                    GoldRandomInstantiate(pos);
                }
                totalBlockNum++;
            }
        }
    }

    /// <summary>
    /// 25% 확률로 골드 아이템을 생성
    /// </summary>
    /// <param name="pos">생성할 위치(위에 생성 예정)</param>
    void GoldRandomInstantiate(Vector2 pos)
    {
        if (Random.Range(0, 4) == 3)
        {
            Instantiate(goldPrefab, new Vector2(pos.x, pos.y + 1), Quaternion.identity);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TimeStop();
        }
    }
    /// <summary>
    /// 시간 정지 기능 호출할 때마다 pause를 true / false로 바꾼다
    /// </summary>
    public void TimeStop()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1f;
            pauseImage.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            pause = true;
            pauseImage.SetActive(true);
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
        energyBar.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(10);
        energyBar.GetComponent<Image>().fillAmount = 0;
        energyBar.GetComponent<Image>().color = Color.white;
        burnning = false;
        fillAmountMax = false;


    }

    
    /// <summary>
    /// 버튼 이미지를 잠시 바꾼다. string으로 "L" 또는 "R"을 줘야한다.
    /// </summary>
    /// <param name="click">L 또는 R</param>
    /// <returns></returns>
    IEnumerator ButtonChange(string click)
    {
        if (click.Equals("L"))
        {
            leftButton.GetComponent<Image>().sprite = clickButtonImage;
            yield return new WaitForSeconds(0.2f);
            leftButton.GetComponent<Image>().sprite = basicButtonImage;
        }
        else if(click.Equals("R"))
        {
            rightButton.GetComponent<Image>().sprite = clickButtonImage;
            yield return new WaitForSeconds(0.2f);
            rightButton.GetComponent<Image>().sprite = basicButtonImage;
        }

        

    }

    /// <summary>
    /// 먹은 Gold 아이템 +1 그리고 갱신한다
    /// </summary>
    public void GoldPlus()
    {
        gold++;
        goldText.text = gold.ToString();
    }

    /// <summary>
    /// 태그 이름이 Block인 것을 찾아 지운다.
    /// </summary>
    void RemoveBlock()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 사운드 버튼 누르면 onOff 및 이미지 변경
    /// </summary>
    public void SoundButtonChange()
    {
        if (SoundManager.Instance.soundOnOff)
        {
            SoundManager.Instance.AudioStop();
            SoundManager.Instance.soundOnOff = false;
            GameObject.FindGameObjectWithTag("SoundButton").GetComponent<Image>().sprite = soundOffButtonImage;
        }
        else
        {
            SoundManager.Instance.AudioPlay();
            SoundManager.Instance.soundOnOff = true;
            GameObject.FindGameObjectWithTag("SoundButton").GetComponent<Image>().sprite = soundOnButtonImage;
        }
        
    }

    /// <summary>
    /// 앱 종료 이미지 활성화 비활성화
    /// </summary>
    /// <param name="onOff">true는 활성화 / false는 비활성화</param>
    public void QuitImage(bool onOff)
    {
        if (onOff)
        {
            imageQuit.SetActive(true);
        }
        else
        {
            imageQuit.SetActive(false);
        }
        
    }

    /// <summary>
    /// 앱 종료
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
