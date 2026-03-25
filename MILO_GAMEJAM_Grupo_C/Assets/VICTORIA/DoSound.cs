using UnityEngine;

public class DoSound : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip loseMusic;

    public AudioSource musicSource;

    public AudioSource regar;
    public AudioSource crecer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PlayGameMusic(){
        musicSource.Stop();
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void PlayLoseMusic(){
        musicSource.Stop();
        musicSource.clip = loseMusic;
        musicSource.Play();
    }

    public void RegarCrecerPlanta(){
        regar.time = 0f;
        crecer.time = 0f;
        regar.Play();
        crecer.Play();
    }

    public void StopCrecer(){
        regar.Stop();
        crecer.Stop();
    }


}
