using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Sound
{
    public string name;                 // name of sound to play
    public AudioClip clip;              // audio file

    [Range(0f, 1f)]
    public float volume = 0.7f;     // randomize the volume
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;        // Randomize the pitch

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;       // random volume
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;            // random pitch

    private AudioSource source;             // refeence the audio source

    public void SetSource(AudioSource _source)
    {
        source = _source;       
        source.clip = clip;
    }


    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));     // set the randomized volume
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));         // set the randomized pitch
        source.Play();                                                                          // play the sound
    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;                             // Array of sounds to play

    private void Awake()
    {
        if (instance != null)                   // check to make sure the audiomanager is int the game
        {
            Debug.LogError("More than one AudioManager in scene");
        }
        else
        {
            instance = this;                    // otherwise create one
        }

    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)                 // get a list of sounds to play
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }


    }

    public void PlaySound(string _name)         // play the sounds
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with name
        Debug.Log("AudioManager: Sound name not found, " + _name);      
    }

}
