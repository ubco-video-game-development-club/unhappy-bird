using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    public Image cross;

    private bool isMuted;

    void Start() {
        isMuted = AudioListener.pause;
        SetCrossActive(isMuted);
    }

    public void ToggleMuted() {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        SetCrossActive(isMuted);
    }

    private void SetCrossActive(bool isActive) {
        cross.enabled = isActive;
    }
}
