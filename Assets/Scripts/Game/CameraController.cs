using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  private enum State
  {
    TRAVELLING,
    UNTRAVELLING,
    IDLE,
    LAUNCH
  }

  [SerializeField]
  private Transform pek;

  [SerializeField]
  private Transform end;

  [SerializeField]
  private float chaseSpeed = 20f;

  [SerializeField]
  private float travellingSpeed = 0.5f;

  [SerializeField]
  private float untravellingSpeed = 2f;

  private State state;
  private Vector3 origin;

  // Start is called before the first frame update
  void Start()
  {
    origin = transform.position;
    state = State.TRAVELLING;

    GameController.OnLaunchPek += OnLaunchPek;
    GameController.OnGameStart += OnGameStart;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    GameController.OnLaunchPek -= OnLaunchPek;
    GameController.OnGameStart -= OnGameStart;
  }

  // Update is called once per frame
  void Update()
  {
    float pos = transform.position.x;

    switch (state)
    {
      case State.LAUNCH:
        if (Vector3.Distance(transform.position, pek.position) > chaseSpeed)
        {

          pos = Mathf.Lerp(pos, pek.position.x, chaseSpeed * Time.deltaTime);
          move(pos);
        }
        break;
      case State.TRAVELLING:
        pos = Mathf.Lerp(pos, end.position.x, travellingSpeed * Time.deltaTime);
        move(pos);
        if (Mathf.Abs(transform.position.x - end.position.x) <= travellingSpeed)
        {
          state = State.UNTRAVELLING;
        }
        break;
      case State.UNTRAVELLING:
        pos = Mathf.Lerp(pos, origin.x, untravellingSpeed * Time.deltaTime);
        move(pos);
        if (Mathf.Abs(transform.position.x - origin.x) < untravellingSpeed * Time.deltaTime)
        {
          GameController.startGame();
        }
        break;
      default:
        break;
    }
  }

  void OnLaunchPek()
  {
    state = State.LAUNCH;
  }

  void OnGameStart()
  {
    transform.position = origin;
    state = State.IDLE;
  }

  void move(float pos)
  {
    transform.position = new Vector3(pos, transform.position.y, transform.position.z);
  }
}
