using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirachinasThrow : MonoBehaviour
{
  private enum State
  {
    NOT_STARTED,
    IDLE,
    DRAGGING,
    RELEASED
  }

  [SerializeField]
  private Transform[] positions;

  [SerializeField]
  private LineRenderer[] lines;

  [SerializeField]
  private Transform pek;

  [SerializeField]
  private Transform[] stripPoints;

  [SerializeField]
  private float launchForce = 500f;

  [SerializeField]
  private float upForce = 200f;

  [SerializeField]
  private float launchTorque = 15f;

  private bool mouseDown;
  private State state;
  Rigidbody pekBody;
  private Vector3 origin;
  private AudioSource audioSource;
  private AudioClip stretchClip;
  private AudioClip releaseClip;

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < lines.Length; i++)
    {
      Vector3 pos = positions[i].position;
      lines[i].SetPosition(0, pos);
      lines[i].SetPosition(1, stripPoints[0].position);
    }

    state = State.NOT_STARTED;
    mouseDown = false;
    pekBody = pek.GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
    origin = positions[2].position;

    GameController.OnGameStart += OnGameStart;

    stretchClip = Resources.Load<AudioClip>("Sounds/stretch");
    releaseClip = Resources.Load<AudioClip>("Sounds/release");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    GameController.OnGameStart -= OnGameStart;
  }

  // Update is called once per frame
  void Update()
  {
    if (state == State.NOT_STARTED)
    {
      return;
    }

    if (Input.GetMouseButtonDown(0))
    {
      OnMouseDown();
    }
    else if (Input.GetMouseButtonUp(0))
    {
      OnMouseUp();
    }

    if (mouseDown)
    {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = Camera.main.nearClipPlane;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      Ray ray = Camera.main.ScreenPointToRay(mousePos);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit, 10))
      {
        if (state == State.DRAGGING)
        {
          worldPosition = hit.point;
          worldPosition.z = 0;
          worldPosition = origin + Vector3.ClampMagnitude(worldPosition - origin, 2f);
          if (worldPosition.y < 0.35f)
          {
            worldPosition.y = 0.35f;
          }
          pek.position = new Vector3(worldPosition.x, worldPosition.y, 0);
          for (int i = 0; i < lines.Length; i++)
          {
            lines[i].SetPosition(1, stripPoints[i].position);
          }
        }
      }
    }
    else
    {
      if (state == State.RELEASED)
      {
        foreach (LineRenderer line in lines)
        {
          Vector3 currentPos = line.GetPosition(1);
          Vector3 newPos = Vector3.Lerp(currentPos, origin, 10f * Time.deltaTime);
          line.SetPosition(1, newPos);
        }
      }
    }
  }

  /// <summary>
  /// OnMouseDown is called when the user has pressed the mouse button while
  /// over the GUIElement or Collider.
  /// </summary>
  void OnMouseDown()
  {
    mouseDown = true;
    if (state == State.IDLE)
    {
      state = State.DRAGGING;
      pekBody.isKinematic = true;
      GameController.startDrag();
      audioSource.PlayOneShot(stretchClip);
    }
  }

  /// <summary>
  /// OnMouseUp is called when the user has released the mouse button.
  /// </summary>
  void OnMouseUp()
  {
    mouseDown = false;
    if (state == State.DRAGGING)
    {
      state = State.RELEASED;
      pekBody.isKinematic = false;
      Vector3 delta = origin - pek.position;
      pekBody.AddForce((delta * launchForce) + (Vector3.up * upForce), ForceMode.Acceleration);
      pekBody.AddTorque(delta + (Vector3.back * launchTorque));
      GameController.launchPek();
      audioSource.PlayOneShot(releaseClip);
    }
  }

  void OnGameStart()
  {
    state = State.IDLE;
  }
}