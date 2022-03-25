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
  private float rotSpeed = 15f;

  [SerializeField]
  private float spawnTime = 3f;

  private Quaternion noRotation;
  private Vector3 origin;
  private State state;
  private float timer;
  private Rigidbody body;

  // Start is called before the first frame update
  void Start()
  {
    noRotation = transform.rotation;
    origin = transform.position;
    state = State.IDLE;

    body = GetComponent<Rigidbody>();

    GameController.OnDragStart += OnDragStart;
    GameController.OnLaunchPek += OnLaunchPek;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    GameController.OnDragStart -= OnDragStart;
    GameController.OnLaunchPek -= OnLaunchPek;
  }

  // Update is called once per frame
  void Update()
  {
    switch (state)
    {
      case State.IDLE:
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        break;
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
          transform.rotation = noRotation;
          transform.position = origin;
          body.velocity = Vector3.zero;
          GameController.startGame();
        }
        break;
      default:
        break;
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
  }
}