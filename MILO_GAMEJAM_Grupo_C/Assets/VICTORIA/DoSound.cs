using UnityEngine;

public class DoSound : MonoBehaviour
{
    public AudioClip gameMusic;
    public AudioClip loseMusic;

    public AudioSource musicSource;

    public AudioSource regar;
    public AudioSource crecer;

    public UI ui;
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

        if(ui.menu == 3){
            if(!regar.isPlaying) regar.Play();
            if(!crecer.isPlaying) crecer.Play();
        }

    }

    public void StopCrecer(){
        if(regar.isPlaying) regar.Stop();
        if(crecer.isPlaying) crecer.Stop();
    }


}
