using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTools : MonoBehaviour
{
    public float speed;
    public GameObject EnemyDirection;
    public GameObject player;
    public GameObject Boom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed, 0, 0);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.GetComponent<FinalMovement>().groundCheck.position.y > transform.position.y)
        {
            Audio.Instance.PlaySound("Bao1");
            Destroy(gameObject);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 10);
            GameObject obj = Instantiate(Boom); obj.transform.position = transform.position;

            Destroy(obj, 0.4f);
        }
        if (collision.gameObject.tag == "Player" && player.GetComponent<FinalMovement>().groundCheck.position.y <= transform.position.y)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(20,player.GetComponent<Rigidbody2D>().velocity.y);
          //  player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 10);
        }
        if (collision.gameObject == EnemyDirection)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            speed = -speed;
        }
    }



}
