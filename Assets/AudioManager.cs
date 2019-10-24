using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private int max_sources;
    [SerializeField] private float volume_reduction;
    [SerializeField] private AudioClip bgm_clip;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup[] mixer_groups;
    [SerializeField] private float rolloff_distance;

    private List<AudioSource> reserved_sources;

    private Dictionary<string, AudioClip> clip_map;

    private Stack<GameObject> obj_pool;
    private Stack<AudioSource> source_pool;
    private GameObject cam;

    private AudioSource narration;
    private AudioSource bgm;


    public float[] mixer_group_volume;

    // Start is called before the first frame update

    private void Start()
    {
        InitMixerGroups();
        ChangeBGM(bgm_clip);
        StartBGM(true);
    }
    void Awake()
    {
        InitSources();
        InitBGM();
        InitNarration();
        narration.priority = 0;
        bgm.priority = 1;
    }

    void InitMixerGroups()
    {
        mixer.SetFloat("MasterVol", mixer_group_volume[0]);
        mixer.SetFloat("Sound EffectsVol", mixer_group_volume[1]);
        mixer.SetFloat("PlayerVol", mixer_group_volume[2]);
        mixer.SetFloat("EnemyVol", mixer_group_volume[3]);
        mixer.SetFloat("GenericVol", mixer_group_volume[4]);
        mixer.SetFloat("Background MusicVol", mixer_group_volume[5]);
        mixer.SetFloat("NarrationVol", mixer_group_volume[6]);
    }

    void InitBGM()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        bgm = cam.GetComponents<AudioSource>()[1];
        bgm.priority = 1;
        bgm.spatialize = false;
        bgm.volume = 0.2f;
        bgm.outputAudioMixerGroup = mixer_groups[5];
    }

    void InitNarration()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        narration = cam.GetComponents<AudioSource>()[0];
        narration.priority = 0;
        narration.spatialize = false;
        narration.volume = 1.0f;
        narration.outputAudioMixerGroup = mixer_groups[6];
    }

    void InitSources()
    {

        obj_pool = new Stack<GameObject>();
        source_pool = new Stack<AudioSource>();
        clip_map = new Dictionary<string, AudioClip>();
        reserved_sources = new List<AudioSource>();

        //mixer = Resources.Load("ToyWarsMixer", typeof(AudioMixer)) as AudioMixer;
        mixer_groups = mixer.FindMatchingGroups("Master");

        AudioSource src;
        GameObject audio_object;
        for (int i = 0; i < max_sources; i++)
        {
            audio_object = new GameObject("Audio Object");
            audio_object.transform.SetParent(gameObject.transform);
            obj_pool.Push(audio_object);
            src = obj_pool.Peek().AddComponent(typeof(AudioSource)) as AudioSource;
            src.outputAudioMixerGroup = mixer_groups[4];
            src.rolloffMode = AudioRolloffMode.Linear;
            src.maxDistance = rolloff_distance;
            src.dopplerLevel = 0;
            source_pool.Push(src);
        }
    }

    public void LoadClip(string path)
    {
        string key = path.Split('\\').Last<string>();

        if (clip_map.ContainsKey(key))
        {
            return;
        }

        AudioClip clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;

        if(clip == null)
        {
            return;
        }

        clip_map.Add(key, clip);
    }

    public void DeleteClip(string key)
    {
        if (!clip_map.ContainsKey(key))
        {
            return;
        }

        clip_map.Remove(key);
    }

    public void RemoveClips(int key)
    {
        clip_map.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneshot(AudioClip clip, Vector3 coord, bool occluding = false, float volume = 1.0f, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if (clip != null)
        {
            if(source_pool.Peek() == null)
            {
                return;
            }
            AudioSource src = source_pool.Pop();
            GameObject audio_obj = src.gameObject;
            audio_obj.transform.position = coord;
            src.spatialize = occluding;
            src.spatialBlend = spacial_blend;
            src.clip = clip;
            src.enabled = true;
            src.Play();
            StartCoroutine(EndClip(src, clip.length));
        }
    }

    public void PlayOneshot(AudioClip clip, Transform source_trans, bool occluding = false, float volume = 1.0f, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if (clip != null)
        {
            if (source_pool.Peek() == null)
            {
                return;
            }
            AudioSource src = source_pool.Pop();
            GameObject audio_obj = src.gameObject;
            audio_obj.transform.SetParent(source_trans);
            audio_obj.transform.position = source_trans.position;
            src.spatialize = occluding;
            src.spatialBlend = spacial_blend;
            src.clip = clip;
            src.enabled = true;
            src.Play();
            StartCoroutine(EndClip(src, clip.length));
        }
    }

    public void PlayOneshot(string key, Transform source_trans, bool occluding = false, float volume = 1.0f, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if (key != null)
        {
            if (source_pool.Peek() == null)
            {
                return;
            }
            AudioSource src = source_pool.Pop();
            GameObject audio_obj = src.gameObject;
            AudioClip clip;
            clip_map.TryGetValue(key, out clip);
            if(clip == null)
            {
                return;
            }
            audio_obj.transform.SetParent(source_trans);
            audio_obj.transform.position = source_trans.position;
            src.spatialize = occluding;
            src.spatialBlend = spacial_blend;
            src.clip = clip_map[key];
            src.enabled = true;
            src.Play();
            StartCoroutine(EndClip(src, src.clip.length));
        }
    }

    public void PlayOneshot(string key, Vector3 coord, bool occluding = false, float volume = 1.0f, float spacial_blend = 0.0f, float pitch = 1.0f)
    {
        if (key != null)
        {
            if (source_pool.Peek() == null)
            {
                return;
            }
            AudioSource src = source_pool.Pop();
            GameObject audio_obj = src.gameObject;
            AudioClip clip;
            clip_map.TryGetValue(key, out clip);
            if (clip == null)
            {
                return;
            }
            audio_obj.transform.position = coord;
            src.spatialize = occluding;
            src.spatialBlend = spacial_blend;
            src.clip = clip_map[key];
            src.enabled = true;
            src.Play();
            StartCoroutine(EndClip(src, src.clip.length));
        }
    }

    IEnumerator EndClip(AudioSource src, float time)
    {
        yield return new WaitForSeconds(time);
        src.enabled = false;
        src.gameObject.transform.SetParent(gameObject.transform);
        source_pool.Push(src);
    }

    public int ReserveSource(string key, bool occluding = false, float spacial_blend = 0.0f, float pitch = 1.0f, bool looping = false)
    {
        if(source_pool.Peek() == null)
        {
            print("No Audio Sources Available");
            return -1;
        }
        AudioSource src = source_pool.Pop();
        AudioClip clip;
        clip_map.TryGetValue(key, out clip);
 
        if (clip == null)
        {
            print("Clip path is null");
            return -1;
        }
        int index = reserved_sources.Count;
        src.clip = clip;
        src.spatialize = occluding;
        src.spatialBlend = spacial_blend;
        src.loop = looping;
        reserved_sources.Add(src);
        return index;
    }

    public void PlayReserved(int source_id, Vector3 coord, float volume = 1.0f)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.gameObject.transform.Translate(coord);
        reserved_sources.ElementAt(source_id).Play();
    }

    public void PlayReserved(int source_id, Transform source_trans, float volume = 1.0f)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.gameObject.transform.SetParent(source_trans);
        src.gameObject.transform.position = source_trans.position;
        reserved_sources.ElementAt(source_id).Play();
    }

    public void PlayReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        reserved_sources.ElementAt(source_id).Play();
    }

    public void BindReserved(int source_id, Transform source_trans)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.gameObject.transform.SetParent(source_trans);
        src.gameObject.transform.position = source_trans.position;
    }

    public void UnbindReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.Stop();
        src.loop = false;
        src.gameObject.transform.SetParent(gameObject.transform);
    }

    public void StopReserved(int source_id)
    {
        reserved_sources.ElementAt(source_id).Stop();
    }

    public void FreeReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        reserved_sources.RemoveAt(source_id);
        src.enabled = false;
        src.gameObject.transform.SetParent(gameObject.transform);
        source_pool.Push(src);
    }

    public void PlayNarration(AudioClip clip, float volume = 1.0f)
    {
        narration.clip = clip;
        mixer.SetFloat("Background MusicVol", mixer_group_volume[5] + volume_reduction);
        mixer.SetFloat("Sound EffectsVol", mixer_group_volume[1] + volume_reduction);
        narration.Play();
        StartCoroutine(EndNarration(clip.length));
    }
    IEnumerator EndNarration(float time)
    {
        yield return new WaitForSeconds(time);
        mixer.SetFloat("Background MusicVol", mixer_group_volume[5]);
        mixer.SetFloat("Sound EffectsVol", mixer_group_volume[1]);
    }

    public void StartBGM(AudioClip clip, bool loop = true)
    {
        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void StartBGM(bool loop = true)
    {
        bgm.loop = loop;
        bgm.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void ChangeBGM(AudioClip clip)
    {
        bgm.clip = clip;
    }

    public void StopAll()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource src in sources)
        {
            src.Stop();
        }
    }
}
