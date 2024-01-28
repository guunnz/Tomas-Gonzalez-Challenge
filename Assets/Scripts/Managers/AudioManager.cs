using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class Audio
{
    public float pitch;
    public float volume;
    public AudioClip clip;
    public AudioManager.AudioName AudioName;
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] private bool muteSound;

    [SerializeField] private int objectPoolLength = 20;

    private List<AudioSource> pool = new List<AudioSource>();
    public List<Audio> audioList = new List<Audio>();

    public enum AudioName
    {
        Pop
    }

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < objectPoolLength; i++)
        {
            GameObject soundObject = new GameObject();
            soundObject.transform.SetParent(instance.transform);
            soundObject.name = "Sound Effect";
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f;
            audioSource.gameObject.SetActive(false);
            pool.Add(audioSource);
        }
    }

    public static void Play2DSound(AudioName audioName)
    {
        try
        {
            Audio clip = instance.audioList.Single(x => x.AudioName == audioName);
            if (!instance)
            {
                Debug.LogError("No Audio Manager found in the scene, make sure to add one if you want sound");
                return;
            }

            if (instance.muteSound)
            {
                return;
            }

            if (!clip.clip)
            {
                Debug.LogError("Clip is null");
                return;
            }

            for (int i = 0; i < instance.pool.Count; i++)
            {
                if (!instance.pool[i].gameObject.activeInHierarchy)
                {
                    instance.pool[i].clip = clip.clip;
                    instance.pool[i].spatialBlend = 0;
                    instance.pool[i].pitch = clip.pitch;
                    instance.pool[i].rolloffMode = AudioRolloffMode.Linear;
                    instance.pool[i].gameObject.SetActive(true);
                    instance.pool[i].Play();
                    instance.StartCoroutine(instance.ReturnToPool(instance.pool[i].gameObject, clip.clip.length));
                    return;
                }
            }

            GameObject soundObject = new GameObject();
            soundObject.transform.SetParent(instance.transform);
            soundObject.name = "Sound Effect";
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f;
            instance.StartCoroutine(instance.playSound(audioSource, clip));
            instance.StartCoroutine(instance.ReturnToPool(soundObject, clip.clip.length));
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    IEnumerator playSound(AudioSource audioSource, Audio clip)
    {
        audioSource.spatialBlend = 0f;
        yield return null;
        audioSource.minDistance = 0f;
        audioSource.spatialBlend = 0f;
        audioSource.volume = clip.volume;
        audioSource.pitch = clip.pitch;
        instance.pool.Add(audioSource);
        audioSource.clip = clip.clip;
        audioSource.Play();
    }

    private IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}