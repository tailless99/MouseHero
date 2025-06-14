using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    /// <summary>
    /// 자주 사용하는 소리를 미리 등록하고 사용하기 위한 변수
    /// 0 : UI Btn Click Sound
    /// 1 : Event fail Sound
    /// </summary>
    [SerializeField] private List<AudioClip> staticSound;
    [SerializeField] private int numberOfSources = 10; // 동시 재생 가능 수
    [SerializeField] private GameObject audioSourcePrefab; // 오디오 소스가 있는 프리팹
    [SerializeField] private Transform spawnParent; // 오디오 소스를 인스턴스해둘 위치
    
    // 사운드 소스 리스트
    private List<AudioSource> availableSources;

    protected override void Awake() {
        base.Awake();
        availableSources = new List<AudioSource>();

        // 미리 사운드 소스 만들기
        for (int i = 0; i < numberOfSources; i++) {
            GameObject go = Instantiate(audioSourcePrefab, spawnParent);
            go.name = "AudioSource_" + i;
            availableSources.Add(go.GetComponent<AudioSource>());
            go.SetActive(true);
        }
    }

    // 중복 재생되지 않는 사운드를 재생
    public void PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f) {
        AudioSource source = GetAvailableSource();
        if (source != null && clip != null) {
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }

    // 중복재생 되는 사운드를 1회 재생함
    public void PlayOneShotSound(AudioClip clip, float volume = 1f, float pitch = 1f) {
        AudioSource source = GetAvailableSource();
        if (source != null && clip != null) {
            source.pitch = pitch;
            source.PlayOneShot(clip, volume);
            source.pitch = 1f;
        }
    }

    /// <summary>
    /// 클래스 내부에서만 사용하는 함수.
    /// 사용중이지 않은 사운드 소스를 반환한다.
    /// </summary>
    private AudioSource GetAvailableSource() {
        foreach (AudioSource source in availableSources)
            if (!source.isPlaying) return source;

        return null;
    }
}