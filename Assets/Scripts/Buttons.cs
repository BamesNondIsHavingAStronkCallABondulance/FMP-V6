using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    AudioManager audioManager;
    public void L1Load()
    {
        SceneManager.LoadScene(1);
    }

    public void VictoryLoad()
    {
        SceneManager.LoadScene(4);
    }

    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
        audioManager.firstPass = true;
        audioManager.frontEnd = false;
    }

    public void MuteMusic()
    {
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
        s.musicSource.mute = !s.musicSource.mute;
    }

    public void PlayMusicClip(string name)
    {
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
        s.musicSource.Play();
    }

    public void PlaySFXClip(string name)
    {
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
        s.sfxSource.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
