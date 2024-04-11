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
            MovementLeft();
            MovementRight();
            MovementRight();
            MovementLeft();
        }


    }

    /// <summary>
    /// Ű���� ���� Ű�� ������ ���� ������ �̵�
    /// </summary>
    void MovementLeft()
    {
        if(GamaManager.Instance.MoveCheck(transform, "L"))
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            GameObject findCloseBlock = GamaManager.Instance.FindCloseLeftBlock();
            transform.position = new Vector2(findCloseBlock.transform.position.x, findCloseBlock.transform.position.y + 1);
            GamaManager.Instance.ChangeTag(findCloseBlock);
        }

    }
    /// <summary>
    /// Ű���� ������ Ű�� ������ ������ ������ �̵�
    /// </summary>
    void MovementRight()
    {   
        if(GamaManager.Instance.MoveCheck(transform, "R"))
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            GameObject findCloseBlock = GamaManager.Instance.FindCloseRightBlock();
            transform.position = new Vector2(findCloseBlock.transform.position.x, findCloseBlock.transform.position.y + 1);
            GamaManager.Instance.ChangeTag(findCloseBlock);
        }

    }
}
