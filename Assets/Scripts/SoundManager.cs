using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;
    public List<AudioClip> clipList;
    public bool soundOnOff = true;
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
    void Start()
    {
        soundOnOff = true;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ȿ���� ��� : [0]���� [1]���� [2] ȹ��
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


    /// <summary>
    /// ���Ұ�
    /// </summary>
    public void AudioStop()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// ���Ұ� Ǯ��
    /// </summary>
    public void AudioPlay()
    {
        audioSource.Play();
    }


    
}
