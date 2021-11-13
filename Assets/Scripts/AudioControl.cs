using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isPlaying){
			PlayIdle();
		}
    }
	public void PlayIdle(){
		player.Stop();
		player.PlayOneShot(idle[Random.Range(0,idle.Length-1)]);
	}
	public void PlayBrust(){
		player.Stop();
		player.PlayOneShot(brust[Random.Range(0,brust.Length-1)]);
	}
	public void PlayDeath(){
		player.Stop();
		player.PlayOneShot(death);
	}
	public void PlayShoot(){
		player.Stop();
		player.PlayOneShot(shoot);
	}
	
	public AudioSource player;
	public AudioClip[] idle;
	public AudioClip[] brust;
	public AudioClip death;
	public AudioClip shoot;
	public AudioClip spawn;
}
