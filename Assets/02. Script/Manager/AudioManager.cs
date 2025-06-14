using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    /// <summary>
    /// ���� ����ϴ� �Ҹ��� �̸� ����ϰ� ����ϱ� ���� ����
    /// 0 : UI Btn Click Sound
    /// 1 : Event fail Sound
    /// </summary>
    [SerializeField] private List<AudioClip> staticSound;
    [SerializeField] private int numberOfSources = 10; // ���� ��� ���� ��
    [SerializeField] private GameObject audioSourcePrefab; // ����� �ҽ��� �ִ� ������
    [SerializeField] private Transform spawnParent; // ����� �ҽ��� �ν��Ͻ��ص� ��ġ
    
    // ���� �ҽ� ����Ʈ
    private List<AudioSource> availableSources;

    protected override void Awake() {
        base.Awake();
        availableSources = new List<AudioSource>();

        // �̸� ���� �ҽ� �����
        for (int i = 0; i < numberOfSources; i++) {
            GameObject go = Instantiate(audioSourcePrefab, spawnParent);
            go.name = "AudioSource_" + i;
            availableSources.Add(go.GetComponent<AudioSource>());
            go.SetActive(true);
        }
    }

    // �ߺ� ������� �ʴ� ���带 ���
    public void PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f) {
        AudioSource source = GetAvailableSource();
        if (source != null && clip != null) {
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }

    // �ߺ���� �Ǵ� ���带 1ȸ �����
    public void PlayOneShotSound(AudioClip clip, float volume = 1f, float pitch = 1f) {
        AudioSource source = GetAvailableSource();
        if (source != null && clip != null) {
            source.pitch = pitch;
            source.PlayOneShot(clip, volume);
            source.pitch = 1f;
        }
    }

    /// <summary>
    /// Ŭ���� ���ο����� ����ϴ� �Լ�.
    /// ��������� ���� ���� �ҽ��� ��ȯ�Ѵ�.
    /// </summary>
    private AudioSource GetAvailableSource() {
        foreach (AudioSource source in availableSources)
            if (!source.isPlaying) return source;

        return null;
    }
}