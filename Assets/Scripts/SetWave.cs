using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWave : MonoBehaviour
{
    public Slider slider;
    public void waveSet()
    {

        //Messenger<float>.Broadcast(GameEvent.WAVE_CHANGED, slider.value);
        StartData.startWave = (int)slider.value;
        Debug.Log("wave set: " + (int)slider.value);
    }
}
