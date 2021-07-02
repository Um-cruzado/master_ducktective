using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndCutscene : MonoBehaviour
{

    public AudioSource BG;
    // Start is called before the first frame update
    void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VideoPlayer>().isPlaying)
         {
            GetComponent<VideoPlayer>().enabled = true;
            Time.timeScale = 0;
          
            // GetComponent<AudioSource>().enabled = !GetComponent<AudioSource>().enabled;
        }
        else
        {
            GetComponent<VideoPlayer>().enabled = false;
            Time.timeScale = 1;
            BG.volume = 1;
            
        }
       
    }
}
