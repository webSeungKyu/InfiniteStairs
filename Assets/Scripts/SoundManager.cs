using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;
    public List<AudioClip> clipList;

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
    void Start()
    {
        GetComponent<AudioSource>();
    }

    /// <summary>
    /// 효과음 재생 : [0]점프 [1]실패 [2] 획득
    /// </summary>
    /// <param name="num">int</param>
    public void AudioPlayOneShot(int num)
    {
        switch (num)
        {
            case 0:
                audioSource.PlayOneShot(clipList[num]);
                break;
            case 1:
                audioSource.PlayOneShot(clipList[num]);
                break;
            case 2:
                audioSource.PlayOneShot(clipList[num]);
                break;
            default: break;
        }

    }
}
