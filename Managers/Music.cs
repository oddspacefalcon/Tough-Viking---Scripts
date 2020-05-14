using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{

    public Object[] myMusic; // declare this as Object array
    public AudioSource audio;

    private int counter = 0;

    public static Music instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
       // PlayerPrefs.SetInt("SoundOn", 0);
        audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
        audio.Play();

        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            audio.UnPause();

        }
        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {

            audio.Pause();
        }
        if (SpawnMenu.inGame == true)
            audio.Stop();
    }

    public void pauseMusic()
    {
        audio.Pause();
    }
    public void playMusic(){
        audio.UnPause();
    }
   
    // antalet gånger man dött, körs från player script (här ty denna metod dör ej vid load)
    public int NumberOfDeath()
    {
        counter++;
        return counter;
    }
  

    /* Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
            playRandomMusic();
    }*/
    /*
    void playRandomMusic()
    {
        if(PlayerPrefs.GetInt("SoundOn") == 0 && SpawnMenu.inGame == false)
        {
            audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
            audio.Play();

        }

    }*/
}