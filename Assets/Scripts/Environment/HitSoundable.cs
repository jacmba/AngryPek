using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HitSoundable : MonoBehaviour
{
  private AudioSource source;

  // Start is called before the first frame update
  void Start()
  {
    source = GetComponent<AudioSource>();
    source.playOnAwake = false;
    source.spatialBlend = 1f;
    source.loop = false;
  }

  /// <summary>
  /// OnCollisionEnter is called when this collider/rigidbody has begun
  /// touching another rigidbody/collider.
  /// </summary>
  /// <param name="other">The Collision data associated with this collision.</param>
  void OnCollisionEnter(Collision other)
  {
    source.Play();
  }
}
