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
    /// 키보드 왼쪽 키를 누르면 왼쪽 블럭으로 이동
    /// </summary>
    void MovementLeft()
    {
        GamaManager.Instance.LeftBlockList();
        //transform.position = new Vector2(block[0].transform.position.x, block[0].transform.position.y + 1);
    }
    /// <summary>
    /// 키보드 오른쪽 키를 누르면 오른쪽 블럭으로 이동
    /// </summary>
    void MovementRight()
    {
        
        //transform.position = new Vector2(block[0].transform.position.x, block[0].transform.position.y + 1);
    }
}
