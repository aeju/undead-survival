using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("# BGM")] // 배경음
    public AudioClip bgmClip; // 클립
    public float bgmVolume; // 볼륨
    private AudioSource bgmPlayer; // 오디오 소스
    private AudioHighPassFilter bgmEffect;
    
    [Header("# SFX")] // 효과음 
    public AudioClip[] sfxClips;  // 클립
    public float sfxVolume; // 볼륨
    public int channels; // 채널 : 다량의 효과음 낼 수 있도록 
    private AudioSource[] sfxPlayers; // 오디오 소스
    private int channelIndex;
    
    private void Awake()
    {
        instance = this;
        Init();
    } 
    
    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win } // 문자열 대신, enum -> 오타없이 o

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
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // 효과음 플레이어 초기화 
        GameObject sfxObject = new GameObject("SfxPlayer"); // 효과음 담당 자식 오브젝트 생성 
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // 채널 값을 사용해 오디오 소스 배열 초기화 

        for (int index = 0; index < sfxPlayers.Length; index++) // 모든 효과음 오디오소스 생성하면서 저장 
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(bool isPlay) // 배경음 재생 함수 
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }
    
    public void EffectBgm(bool isPlay) // 필터 켜고 끄는 함수  
    {
        bgmEffect.enabled = isPlay;
    }
    
    public void PlaySfx(Sfx sfx) // 효과음 재생 함수
    {
        for (int index = 0; index < sfxPlayers.Length; index++) // 채널 개수(채널인덱스 변수)만큼 순회하도록
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length; // sfxPlayer.Length값 넘어가지 않도록 -> 나눠줌 
            
            if (sfxPlayers[loopIndex].isPlaying) // 재생하고 있는 효과음이 있다면 -> 건너뜀 (그냥 재생되도록)
                continue; // 반복문 도중 다음 루프로 건너뛰는 키워드 

            // 효과음이 2개 이상인 것 : 랜덤 인덱스 더하기
            int ranIndex = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee) // 2개, 3개라면 -> switch 
            {
                ranIndex = Random.Range(0, 2);
            }
            
            channelIndex = loopIndex;
            sfxPlayers[0].clip = sfxClips[(int)sfx]; // 오디오 소스 클립 변경
            sfxPlayers[0].Play(); // Play 함수 호출 
            
            break; // 반복문 종료 (똑같은 클립 재생시킬 수 있는 문제)
        }
    }
}
