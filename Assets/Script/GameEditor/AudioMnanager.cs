using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor {
    public class AudioMnanager
    {
        private AudioSource AudioSource { get; set; }
        private string AudioSourceName { get; } = "AudioSource";
        public AudioMnanager()
        {
            FindParent();
        }

        public void Play(string name)
        {
            var clip = GameManager.Instance.ResourcesManager.GetAudio(name);
            if (clip == null)
                return;

            AudioSource.PlayOneShot(clip);
        }

        private void FindParent()
        {
            GameObject obj = GameObject.Find(AudioSourceName);
            if (obj == null)
                obj = new GameObject(AudioSourceName);

            AudioSource = obj.AddComponent<AudioSource>();
            AudioSource.transform.position = Vector2.zero;
        }
    }
}