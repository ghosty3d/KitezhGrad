using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	private AudioSource m_AudioSource;

	void Awake() {
		m_AudioSource = GetComponent<AudioSource>();
	}

	public void PlaySound() {
		
	}
}
