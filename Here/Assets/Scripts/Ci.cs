using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ci : MonoBehaviour
{
    
    public GameObject player;
    public GameObject die;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Audio.Instance.PlaySound("Bao2");
            GameObject obj = Instantiate(die);
            obj.transform.position=collision.transform.position;
            Destroy(obj,0.3f);
            //collision.gameObject.SetActive(false);

            GameObject obj2 = Instantiate(die);
            obj2.transform.position = Flag.birth.position;
            Destroy(obj2, 0.3f);
            collision.gameObject.transform.position = Flag.birth.position;         
            //collision.gameObject.SetActive(true);
        }
    }


}
