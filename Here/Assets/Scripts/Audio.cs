using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    private AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        player = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        player.PlayOneShot(clip);
    }

    public void StopSound()
    {
        player.Stop();
    }

    public void QieGe(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        this.GetComponent<AudioSource>().clip = clip;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
