using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PekController : MonoBehaviour
{
  private enum State
  {
    IDLE,
    DRAG,
    LAUNCH,
    TOUCHED,
    STOPPED
  }

  [SerializeField]
  private float spawnTime = 3f;

  private Quaternion noRotation;
  private Vector3 origin;
  private State state;
  private float timer;
  private Rigidbody body;
  private AudioSource audioSource;
  private AudioClip yumClip;
  private AudioClip ooyClip;
  private AudioClip bounceClip;

  // Start is called before the first frame update
  void Start()
  {
    noRotation = transform.rotation;
    origin = transform.position;
    state = State.IDLE;

    body = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();

    GameController.OnDragStart += OnDragStart;
    GameController.OnLaunchPek += OnLaunchPek;
    GameController.OnPizzaCollected += OnPizzaCollected;

    yumClip = Resources.Load<AudioClip>("Sounds/yumyum");
    ooyClip = Resources.Load<AudioClip>("Sounds/ooy");
    bounceClip = Resources.Load<AudioClip>("Sounds/bounce");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    GameController.OnDragStart -= OnDragStart;
    GameController.OnLaunchPek -= OnLaunchPek;
    GameController.OnPizzaCollected -= OnPizzaCollected;
  }

  // Update is called once per frame
  void Update()
  {
    switch (state)
    {
      case State.TOUCHED:
        if (body.velocity.magnitude < 0.2f)
        {
          timer = spawnTime;
          state = State.STOPPED;
        }
        break;
      case State.STOPPED:
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
          PekReset();
        }
        break;
      default:
        break;
    }

    if (transform.position.y < -10f)
    {
      PekReset();
    }
  }

  /// <summary>
  /// OnCollisionEnter is called when this collider/rigidbody has begun
  /// touching another rigidbody/collider.
  /// </summary>
  /// <param name="other">The Collision data associated with this collision.</param>
  void OnCollisionEnter(Collision other)
  {
    if (state == State.LAUNCH)
    {
      state = State.TOUCHED;
    }

    if (other.gameObject.tag == "Ground")
    {
      audioSource.PlayOneShot(bounceClip);
    }
  }

  void OnDragStart()
  {
    transform.rotation = noRotation;
    transform.position = origin;
    state = State.DRAG;
  }

  void OnLaunchPek()
  {
    state = State.LAUNCH;
    audioSource.PlayOneShot(ooyClip);
  }

  void OnPizzaCollected()
  {
    audioSource.PlayOneShot(yumClip);
  }

  void PekReset()
  {
    transform.rotation = noRotation;
    transform.position = origin;
    body.velocity = Vector3.zero;
    body.isKinematic = true;
    state = State.IDLE;
    GameController.startGame();
  }
}
