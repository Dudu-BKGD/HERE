using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTools : MonoBehaviour
{

    public GameObject yinliang;
    public AudioSource audio;
    public GameObject SET;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audio.GetComponent<AudioSource>().volume = yinliang.GetComponent<Scrollbar>().value;
    }

    public void FanHui()
    {
        SceneManager.LoadScene("Scenes/Open");
    }
    public void settings()
    {
        SET.SetActive(true);
    }
}
