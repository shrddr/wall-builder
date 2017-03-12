using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource Source;
    public AudioClip[] GirlClips;
    public AudioClip[] BoyClips;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayGirl()
    {
        int randomIndex = Random.Range(0, GirlClips.Length);
        Source.clip = GirlClips[randomIndex];
        Source.Play();
    }

    public void PlayBoy()
    {
        int randomIndex = Random.Range(0, BoyClips.Length);
        Source.clip = BoyClips[randomIndex];
        Source.Play();
    }
}