using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adminBackground : MonoBehaviour
{
    public List<SpriteRenderer> backgrounds;
    public List<AudioClip> songs;
    public SpriteRenderer active;
    public AudioSource audioSource;
    private void Start() {
        callAdmin.admin = this;
    }
    
    public void changebackground(int newsbackground){
        StartCoroutine("cambiar", newsbackground);
    }

    IEnumerator cambiar(int id){
        if (backgrounds[id] != active) {
            while(backgrounds[id].color.a < 0.99f ){
                backgrounds[id].color =  new Color(1f,1f,1f, backgrounds[id].color.a + 0.3f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            backgrounds[id].color =  new Color(1f,1f,1f,1f);

            while(active.color.a > 0.01f ){
                active.color =  new Color(1f,1f,1f, active.color.a - 0.5f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            active.color =  new Color(1f,1f,1f,0f);

            active = backgrounds[id];
        }
    }

    public void changesong(int newSong)
    {
        if (newSong >= 0 && newSong < songs.Count)
        {
            StartCoroutine(FadeOutAndChangeSong(newSong));
        }
        else
        {
            Debug.LogWarning("Invalid song index: " + newSong);
        }
    }

    IEnumerator FadeOutAndChangeSong(int newSong)
    {
        float fadeDuration = 1.0f;
        float fadeSpeed = 1.0f / fadeDuration;

        while (audioSource.volume > 0.01f)
        {
            audioSource.volume -= fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        audioSource.Stop();
        audioSource.clip = songs[newSong];
        audioSource.Play();

        while (audioSource.volume < .6f)
        {
            audioSource.volume += fadeSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        audioSource.volume = .6f;
    }
    
}
