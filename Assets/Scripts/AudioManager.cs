using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private int max_sources = default;
    [SerializeField] private float volume_reduction = default;
    [SerializeField] private AudioMixer mixer = default;
    [SerializeField] private AudioMixerGroup[] mixer_groups = default;
    [SerializeField] private float rolloff_distance = default;

    private Dictionary<string, AudioClip> clip_map;
    private Stack<GameObject> obj_pool;
    private Stack<AudioSource> source_pool;
    private List<AudioSource> reserved_sources;

    private GameObject cam;
    private AudioSource narration;
    private AudioSource bgm;
    private AudioSource ui;
    private bool narr_blocking = false;
    public float[] mixer_group_volume;

    private static AudioManager audioManager;

    void Awake()
    {
        InitSources();
        InitBGM();
        InitNarration();
        InitUI();
        LoadSoundEffects();
    }

    private void Start()
    {
        //ChangeBGM(bgm_clip);
        InitMixerGroups();
    }

    public static AudioManager GetAudioManager()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        return audioManager;
    }

    void InitUI()
    {
        ui = cam.GetComponents<AudioSource>()[2];
        ui.priority = 1;
        ui.spatialize = false;
        ui.volume = 1.0f;
        ui.outputAudioMixerGroup = mixer_groups[7];
    }

    void LoadSoundEffects()
    {
        ILevelManager lvl_mgr = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ILevelManager>();
        AudioClip[] fx;
        lvl_mgr.GetSoundEffects(out fx);
        foreach (AudioClip clip in fx)
        {
            LoadClip(clip);
        }
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
        mixer.SetFloat("UIVol", mixer_group_volume[7]);
    }

    void InitBGM()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        bgm = cam.GetComponents<AudioSource>()[1];
        bgm.priority = 2;
        bgm.spatialize = false;
        bgm.volume = 1f;
        bgm.outputAudioMixerGroup = mixer_groups[5];
    }

    void InitNarration()
    {
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
        audio_object = new GameObject("Audio Object");
        audio_object.transform.SetParent(transform, false);
        audio_object.transform.localPosition = Vector3.zero;
        src = audio_object.AddComponent(typeof(AudioSource)) as AudioSource;
        src.spatialize = true;
        src.spatialBlend = 1.0f;
        src.playOnAwake = false;
        src.outputAudioMixerGroup = mixer_groups[4];
        src.rolloffMode = AudioRolloffMode.Linear;
        src.maxDistance = rolloff_distance;
        src.dopplerLevel = 1.0f;

        for (int i = 0; i < max_sources; i++)
        {
            GameObject temp = GameObject.Instantiate(audio_object, transform);
            obj_pool.Push(temp);
            source_pool.Push(temp.GetComponent<AudioSource>());
        }
    }

    public void LoadClip(string path)
    {
        string key = path.Split('\\').Last<string>();
        AudioClip clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;
        clip_map.Add(key, clip);
    }

    public void LoadClip(AudioClip clip)
    {
        clip_map.Add(clip.name, clip);
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

    public void PlayOneshot(string key, Vector3 coord)
    {
        if(source_pool.Count < 1)
        {
            print("no sources");
            return;
        }
        AudioSource src = source_pool.Pop();
        GameObject audio_obj = src.gameObject;
        src.clip = clip_map[key];
        audio_obj.transform.position = coord;
        src.enabled = true;
        src.Play();
        StartCoroutine(EndClipPoint(src, src.clip.length));
    }

    public void PlayOneshot(string key, Vector3 coord, float pitch)
    {
        if (source_pool.Count < 1)
        {
            print("no sources");
            return;
        }
        AudioSource src = source_pool.Pop();
        GameObject audio_obj = src.gameObject;
        src.clip = clip_map[key];
        audio_obj.transform.position = coord;
        src.enabled = true;
        src.pitch = pitch;
        src.Play();
        StartCoroutine(EndClipPoint(src, src.clip.length));
    }

    public void PlayOneshot(string key, Transform source_trans)
    {
        if (source_pool.Count < 1)
        {
            print("no sources");
            return;
        }
        AudioSource src = source_pool.Pop();
        GameObject audio_obj = src.gameObject;
        src.clip = clip_map[key];
        audio_obj.transform.SetParent(source_trans,false);
        src.enabled = true;
        src.Play();
        StartCoroutine(EndClipPoint(src, src.clip.length));
    }

    public void PlayOneshot(string key, Transform source_trans, float pitch)
    {
        if (source_pool.Count < 1)
        {
            print("no sources");
            return;
        }
        AudioSource src = source_pool.Pop();
        GameObject audio_obj = src.gameObject;
        src.clip = clip_map[key];
        audio_obj.transform.SetParent(source_trans, false);
        src.enabled = true;
        src.pitch = pitch;
        src.Play();
        StartCoroutine(EndClipPoint(src, src.clip.length));
    }

    IEnumerator EndClipTransform(AudioSource src, float time)
    {
        yield return new WaitForSeconds(time);
        src.enabled = false;
        src.gameObject.transform.SetParent(gameObject.transform, false);
        source_pool.Push(src);
    }

    IEnumerator EndClipPoint(AudioSource src, float time)
    {
        yield return new WaitForSeconds(time);
        src.gameObject.transform.localPosition = Vector3.zero;
        source_pool.Push(src);
        src.enabled = false;
    }

    public int ReserveSource(string key, bool occluding = false, float spacial_blend = 0.0f, float pitch = 1.0f, bool looping = false)
    {
        if (source_pool.Peek() == null)
        {
            print("No Audio Sources Available");
            return -1;
        }
        AudioSource src = source_pool.Pop();
        AudioClip clip = clip_map[key];
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
        src.enabled = true;
        src.gameObject.transform.position = coord;
        src.Play();
    }

    public void PlayReserved(int source_id, Transform source_trans, float volume = 1.0f)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.enabled = true;
        src.gameObject.transform.SetParent(source_trans, false);
        src.Play();
    }

    public void PlayReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.enabled = true;
        src.Play();
    }

    public void BindReserved(int source_id, Transform source_trans)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.gameObject.transform.SetParent(source_trans, false);
    }

    public void UnbindReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.Stop();
        src.loop = false;
        src.gameObject.transform.SetParent(transform, false);
    }

    public void StopReserved(int source_id)
    {
        reserved_sources.ElementAt(source_id).Stop();
    }

    public void FreeReserved(int source_id)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.enabled = false;
        src.loop = false;
        src.outputAudioMixerGroup = mixer_groups[4];
        src.gameObject.transform.SetParent(transform, false);
        src.gameObject.transform.position = Vector3.zero;
        reserved_sources.RemoveAt(source_id);
        source_pool.Push(src);
    }

    public void SetReservedMixer(int source_id, int mixer_num)
    {
        AudioSource src = reserved_sources.ElementAt(source_id);
        src.outputAudioMixerGroup = mixer_groups[mixer_num];
    }

    public void PlayNarration(AudioClip clip, float volume = 1.0f)
    {
        narration.clip = clip;
        narration.Play();
    }

    public void PlayUI(string key, float volume = 1.0f)
    {
        ui.clip = clip_map[key];
        ui.Play();
    }

    public void NarrateSequence(AudioClip[] clips, float delay = 0.0f, bool blocking = false)
    {
        if(narr_blocking == true)
        {
            return;
        }

        narr_blocking = blocking;
        StartCoroutine(NarrateSequence(clips, clips.Length, delay));
    }

    IEnumerator NarrateSequence(AudioClip[] clips, int pos, float delay)
    {
        pos++;
        AudioClip clip = clips[pos];
        narration.clip = clip;
        narration.Play();
        if(pos < clips.Length-1)
        {
            yield return new WaitForSeconds(clip.length + delay);
            NarrateSequence(clips, pos, delay);
        }
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
