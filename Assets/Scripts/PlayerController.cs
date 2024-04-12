using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovementLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovementRight();
        }
        if (Input.GetKeyDown(KeyCode.Space) && GamaManager.Instance.burnning)
        {
            MoveBurnning();
        }


    }

    /// <summary>
    /// Ű���� ���� Ű�� ������ ���� ������ �̵�
    /// </summary>
    public void MovementLeft()
    {
        GamaManager.Instance.StartCoroutine("ButtonChange", "L");
        if (GamaManager.Instance.MoveCheck(transform, "L"))
        {
            SoundManager.Instance.AudioPlayOneShot(0);
            transform.GetComponent<SpriteRenderer>().flipX = true;
            GameObject findCloseBlock = GamaManager.Instance.FindCloseLeftBlock();
            transform.position = new Vector2(findCloseBlock.transform.position.x, findCloseBlock.transform.position.y + 1);
            GamaManager.Instance.ChangeTag(findCloseBlock);
        }

    }
    /// <summary>
    /// Ű���� ������ Ű�� ������ ������ ������ �̵�
    /// </summary>
    public void MovementRight()
    {
        GamaManager.Instance.StartCoroutine("ButtonChange", "R");
        if (GamaManager.Instance.MoveCheck(transform, "R"))
        {
            SoundManager.Instance.AudioPlayOneShot(0);
            transform.GetComponent<SpriteRenderer>().flipX = false;
            GameObject findCloseBlock = GamaManager.Instance.FindCloseRightBlock();
            transform.position = new Vector2(findCloseBlock.transform.position.x, findCloseBlock.transform.position.y + 1);
            GamaManager.Instance.ChangeTag(findCloseBlock);
        }
    }

    /// <summary>
    /// ���� ����� ���� ���� �̵�
    /// </summary>
    public void MoveBurnning()
    {
        string move = GamaManager.Instance.CloseBlock(transform);
        switch (move)
        {
            case "L":
                MovementLeft(); break;
            case "R":
                MovementRight(); break;
            default: break;


        }
    }
}
