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
    }

    /// <summary>
    /// Ű���� ���� Ű�� ������ ���� ������ �̵�
    /// </summary>
    void MovementLeft()
    {
        GameObject findCloseBlock = GamaManager.Instance.FindCloseLeftBlock();
        transform.position = new Vector2(findCloseBlock.transform.position.x, findCloseBlock.transform.position.y + 1);
        GamaManager.Instance.ChangeTag(findCloseBlock);
    }
    /// <summary>
    /// Ű���� ������ Ű�� ������ ������ ������ �̵�
    /// </summary>
    void MovementRight()
    {
        GameObject findCloseBlock = GamaManager.Instance.FindCloseRightBlock();
        transform.position = new Vector2(findCloseBlock.transform.position.x, findCloseBlock.transform.position.y + 1);
        GamaManager.Instance.ChangeTag(findCloseBlock);
    }
}
