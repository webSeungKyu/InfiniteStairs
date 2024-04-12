using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GamaManager.Instance.GoldPlus();
        MoveFall();
        Destroy(gameObject);
    }

    /// <summary>
    /// 중력을 0으로 하여 낙하
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveFall()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        GetComponent<CircleCollider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        yield return new WaitForSeconds(0.1f);
        rb.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);

        

    }
}
