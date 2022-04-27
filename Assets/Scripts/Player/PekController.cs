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
  private Rigidbody body;
  private AudioSource audioSource;
  private AudioClip yumClip;
  private AudioClip ooyClip;
  private AudioClip bounceClip;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    noRotation = transform.rotation;
    origin = transform.position;
    state = State.IDLE;

    body = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();

    eventBus = EventBus.GetInstance();
    eventBus.OnDragStart += OnDragStart;
    eventBus.OnLaunchPek += OnLaunchPek;
    eventBus.OnPizzaCollected += OnPizzaCollected;
    eventBus.OnBoundaryEntered += OnBoundaryEntered;

    yumClip = Resources.Load<AudioClip>("Sounds/yumyum");
    ooyClip = Resources.Load<AudioClip>("Sounds/ooy");
    bounceClip = Resources.Load<AudioClip>("Sounds/bounce");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventBus.OnDragStart -= OnDragStart;
    eventBus.OnLaunchPek -= OnLaunchPek;
    eventBus.OnPizzaCollected -= OnPizzaCollected;
    eventBus.OnBoundaryEntered -= OnBoundaryEntered;
  }

  // Update is called once per frame
  void Update()
  {
    switch (state)
    {
      case State.TOUCHED:
        if (body.velocity.magnitude < 0.2f)
        {
          StartCoroutine(ResetRequest());
          state = State.STOPPED;
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

  void OnBoundaryEntered()
  {
    if (state != State.STOPPED)
    {
      state = State.STOPPED;
      StartCoroutine(ResetRequest());
    }
  }

  void PekReset()
  {
    transform.rotation = noRotation;
    transform.position = origin;
    body.velocity = Vector3.zero;
    body.isKinematic = true;
    state = State.IDLE;
    eventBus.StartGame();
  }

  IEnumerator ResetRequest()
  {
    yield return new WaitForSeconds(spawnTime);
    PekReset();
  }
}
