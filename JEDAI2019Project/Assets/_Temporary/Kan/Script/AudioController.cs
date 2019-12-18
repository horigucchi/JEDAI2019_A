using System.Collections.Generic;

using UnityEngine;



public static class AudioController

{
    public static Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();


    //<param name ="dir"> SEソースルート, Resources</param>

    //<param name ="name">SEの名前</param>

    public static void PlaySnd(string name, Vector3 position, float volume)

    {
        AudioClip clip = LoadClip(name);

        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
           

        else
        { 
            Debug.LogError("Clip is Missing" + name); 
        }
            

    }


    static AudioClip LoadClip(string name)

    {

        if (!audioDic.ContainsKey(name))
        {

            string dirSound = "Sounds/" + name;

            AudioClip clip = Resources.Load(dirSound) as AudioClip;

            if (clip != null)

                audioDic.Add(clip.name, clip);

        }

        return audioDic[name];

    }

}

