using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public int max_sources = 30;
    public float volume_reduction = -20f;

    private List<AudioSource> reserved_sources;

    private Stack<GameObject> obj_pool;
    private Stack<AudioSource> source_pool;
    private GameObject cam;

    private AudioSource narration;
    private AudioSource bgm;

    public AudioMixer mixer;
    public AudioMixerGroup[] mixer_groups;

    // Start is called before the first frame update
    void Start()
    {
        obj_pool = new Stack<GameObject>();
        source_pool = new Stack<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        narration = cam.GetComponents<AudioSource>()[0];
        bgm = cam.GetComponents<AudioSource>()[1];

        narration.priority = 0;
        bgm.priority = 1;
    }

    void InitSources()
    {
        mixer = Resources.Load("ToyWarsMixer") as AudioMixer;
        mixer_groups = mixer.FindMatchingGroups("Master");
        AudioSource src;
        for (int i = 0; i < max_sources; i++)
        {
            obj_pool.Push(new GameObject());
            src = obj_pool.Peek().AddComponent<AudioSource>();
            src.outputAudioMixerGroup = mixer_groups[5];
            source_pool.Push(src);
        }

        narration.outputAudioMixerGroup = mixer.FindMatchingGroups("Narration")[0];
        bgm.outputAudioMixerGroup = mixer.FindMatchingGroups("Background Music")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayOneshot(AudioClip clip, Vector3 coord, bool occluding = false, float volume = 1.0f, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if (clip != null)
        {
            if(source_pool.Peek() == null)
            {
                return;
            }
            AudioSource src = source_pool.Pop();
            GameObject audio_obj = src.gameObject;
            audio_obj.transform.Translate(coord);
            src.spatialize = occluding;
            src.spatialBlend = spacial_blend;
            src.clip = clip;
            src.enabled = true;
            src.Play();
            StartCoroutine(EndClip(src, clip.length));
        }
    }

    void PlayOneshot(AudioClip clip, Transform source_trans, bool occluding = false, float volume = 1.0f, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if (clip != null)
        {
            if (source_pool.Peek() == null)
            {
                return;
            }
            AudioSource src = source_pool.Pop();
            GameObject audio_obj = src.gameObject;
            audio_obj.transform.position = source_trans.position;
            audio_obj.transform.parent = source_trans;
            src.spatialize = occluding;
            src.spatialBlend = spacial_blend;
            src.clip = clip;
            src.enabled = true;
            src.Play();
            StartCoroutine(EndClip(src, clip.length));
        }
    }

    IEnumerator EndClip(AudioSource src, float time)
    {
        yield return new WaitForSeconds(time);
        src.enabled = false;
        src.gameObject.transform.parent = null;
        source_pool.Push(src);
    }

    int ReserveSource(AudioClip clip, bool occluding = false, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if(source_pool.Peek() == null)
        {
            return -1;
        }
        AudioSource src = source_pool.Pop();
        int index = reserved_sources.Count;
        src.clip = clip;
        src.spatialize = occluding;
        src.spatialBlend = spacial_blend;
        reserved_sources.Add(src);
        return index;
    }

    void PlayReserved(int source_id, Vector3 coord)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.gameObject.transform.Translate(coord);
        reserved_sources.ElementAt(source_id).Play();
    }

    void StopReserved(int source_id)
    {
        reserved_sources.ElementAt(source_id).Stop();
    }

    void FreeReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        reserved_sources.RemoveAt(source_id);
        src.enabled = false;
        source_pool.Push(src);
    }

    void PlayNarration(AudioClip clip, float volume = 1.0f)
    {
        narration.clip = clip;
        mixer.SetFloat("Background MusicVol", volume_reduction);
        mixer.SetFloat("Sound EffectsVol", volume_reduction);
        narration.Play();
        StartCoroutine(EndNarration(clip.length));
    }
    IEnumerator EndNarration(float time)
    {
        yield return new WaitForSeconds(time + 0.2f);
        mixer.SetFloat("Background MusicVol", 0f);
        mixer.SetFloat("Sound EffectsVol", 0f);
    }

    void StartBGM(AudioClip clip, bool loop = true)
    {
        bgm.clip = clip;
        bgm.loop = loop;
    }

    void StopAll()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource src in sources)
        {
            src.Stop();
        }
    }
}
