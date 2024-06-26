using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("# BGM")] // 배경음
    public AudioClip bgmClip; // 클립
    public float bgmVolume; // 볼륨
    public AudioSource bgmPlayer; // 오디오 소스
    
    [Header("# SFX")] // 효과음 
    public AudioClip[] sfxClips;  // 클립
    public float sfxVolume; // 볼륨
    public int channels; // 채널 : 다량의 효과음 낼 수 있도록 
    public AudioSource[] sfxPlayers; // 오디오 소스
    private int channelIndex;
    
    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer"); // 배경음 담당 자식 오브젝트 생성
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>(); // AddComponent로 오디오소스 생성하고, 변수에 저장
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 플레이어 초기화 
        GameObject sfxObject = new GameObject("SfxPlayer"); // 효과음 담당 자식 오브젝트 생성 
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // 채널 값을 사용해 오디오 소스 배열 초기화 

        for (int index = 0; index < sfxPlayers.Length; index++) // 모든 효과음 오디오소스 생성하면서 저장 
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }
}
