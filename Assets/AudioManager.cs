using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;
using AudioClasses;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private List<AudioConfig> _sfxConfigs;
    [SerializeField] private List<AudioConfig> _bgmConfigs;
    private List<Audio> _sfx;
    private List<Audio> _bgm;


    public void Load()
    {
        Debug.Log("Loading AudioManager");

        _sfx = new List<Audio>();
        _bgm = new List<Audio>();


        void LoadAudioConfigs(List<AudioConfig> configs, List<Audio> audios)
        {
            configs.ForEach(audioConfig =>
           {
               GameObject spawnedAudioObject = new GameObject
               {
                   name = audioConfig.Name,
                   transform = { parent = transform }
               };
               AudioSource audioSource = spawnedAudioObject.AddComponent<AudioSource>();

#if UNITY_EDITOR
               if (audioSource == null) Debug.LogError("audioSource is null");
               if (audioConfig == null) Debug.LogError("audioConfig is null");
#endif

               audios.Add(new Audio(audioSource, audioConfig));
           });
        }

        LoadAudioConfigs(_sfxConfigs, _sfx);
        LoadAudioConfigs(_bgmConfigs, _bgm);
    }

    public void PlaySFX(string name) => Play(name, _sfx);

    public Audio PlayBGM(string name)
    {
        StopAllBGM();
        return Play(name, _bgm);
    }

    public void StopAllBGM()
    {
        _bgm.ForEach(audio => audio.Stop());
    }





    private Audio Play(string audioName, List<Audio> audios)
    {
#if UNITY_EDITOR
        if (audios == null || audios.Count == 0)
        {
            Debug.LogError("audios is null when trying to play: " + audioName);
        }
#endif


        Audio audio = audios.Find(x => x.Name == audioName);

#if UNITY_EDITOR
        if (audio == null)
        {
            Debug.LogError("Aduio not found: " + audioName);
        }
#endif

        audio.Play();
        return audio;
    }
}

