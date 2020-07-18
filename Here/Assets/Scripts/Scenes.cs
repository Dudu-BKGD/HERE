using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scenes : MonoBehaviour
{
    public GameObject UI;
    public float speed;
    public float howTall;
    public float howShort;
    public bool up = false;
    public bool down = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        Debug.Log(UI.gameObject.GetComponent<RectTransform>().position.y);
        if (Input.GetKeyDown(KeyCode.Return) && up == false)
        {
            if (UI.gameObject.GetComponent<RectTransform>().position.y < howTall)
            {
                Debug.Log(1);
                up = true;
            }

            //SceneManager.LoadScene("Scenes/Main");
        }
        if (up)
        {
            UI.gameObject.GetComponent<RectTransform>().transform.Translate(0, speed, 0);
            if (UI.gameObject.GetComponent<RectTransform>().position.y >= howTall)
            {
                up = false;
            }
        }
        if (down)
        {
            UI.gameObject.GetComponent<RectTransform>().transform.Translate(0, -speed, 0);
            if (UI.gameObject.GetComponent<RectTransform>().position.y <= howShort)
            {
                down = false;
            }
        }

    }
    public void isdown()
    {
        down = true;
    }
    public void Open()
    {
        SceneManager.LoadScene("Scenes/Main");
    }
}
