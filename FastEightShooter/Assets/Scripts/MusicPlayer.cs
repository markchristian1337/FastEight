using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    [SerializeField] AudioSource mainBackgroundMusic;
    [SerializeField] AudioSource transitionSoundClip;
    [SerializeField] AudioSource bossBackgroundMusic;
    [SerializeField] AudioSource victoryBackgroundMusic;
	// Use this for initialization
	void Awake () {
        SetUpSingleton(mainBackgroundMusic);
	}
	
    private void SetUpSingleton(AudioSource soundClip)
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }   

	
    // Update is called once per frame
	void Update () {
		
	}
}
