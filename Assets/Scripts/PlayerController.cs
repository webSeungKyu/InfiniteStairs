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
        GamaManager.Instance.LeftBlockList();
        //transform.position = new Vector2(block[0].transform.position.x, block[0].transform.position.y + 1);
    }
    /// <summary>
    /// Ű���� ������ Ű�� ������ ������ ������ �̵�
    /// </summary>
    void MovementRight()
    {
        
        //transform.position = new Vector2(block[0].transform.position.x, block[0].transform.position.y + 1);
    }
}
