using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetVolume()
    {
        GameObject.Find("Volume Slider").GetComponent<Slider>().value = this.GetComponent<AudioSource>().volume;
    }

    public void ReceiveVolume()
    {
        this.GetComponent<AudioSource>().volume = GameObject.Find("Volume Slider").GetComponent<Slider>().value;
    }
}
