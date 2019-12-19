using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public List<AudioSource> BGM = new List<AudioSource>();

    Dictionary<string, AudioSource> BGMMap = new Dictionary<string, AudioSource>();
    

    private void Awake()
    {

        foreach (var item in BGM)
        {
            BGMMap.Add(item.gameObject.name, item);
        }
    }
    

    public void Play(string objName)
    {
        AudioSource audio;

        if (BGMMap.TryGetValue(objName,out audio))
        {
            audio.Play();
        }
    }

    public void Pause(string objName)
    {
        AudioSource audio;

        if (BGMMap.TryGetValue(objName, out audio))
        {
            audio.Pause();
        }
    }

    public void Stop(string objName)
    {
        AudioSource audio;

        if (BGMMap.TryGetValue(objName, out audio))
        {
            audio.Stop();
        }
    }
}
