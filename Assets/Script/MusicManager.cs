using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;

    [SerializeField] private AudioSource MainMenu;
    [SerializeField] private AudioSource GameStage;
    [SerializeField] private AudioSource BossStage;

    public static MusicManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            return null;
        }
    }

    private void Start()
    {
        Instance = this;
        PlayMenu();
    }

    public void PlayMenu()
    {
        StopAll();
        MainMenu.Play();
    }

    public void PlayStage()
    {
        StopAll();
        GameStage.Play();
    }

    public void PlayBoss()
    {
        StopAll();
        BossStage.Play();
    }

    private void StopAll()
    {
        MainMenu.Stop();
        GameStage.Stop();
        BossStage.Stop();
    }
}
