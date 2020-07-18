using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownWood : MonoBehaviour
{
    public float speed;
    public GameObject down;
    private bool isDown=false;
    private void Start()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            isDown = true;
                     
        }
    }

    IEnumerator WaitUp(float t)
    {
        yield return new WaitForSeconds(t);//运行到这，暂停t秒
        //t秒后，继续运行下面代码
        transform.position = down.transform.position;
        isDown = false;

    }

    public void Down()
    {
        if (isDown)
        {
            transform.Translate(0, -speed, 0);
            StartCoroutine(WaitUp(3f));
        }
    }
    public void Up()
    {
        
    }


    void Update()
    {
        Down();
    }
}
