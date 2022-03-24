using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirachinasThrow : MonoBehaviour
{
  private enum State
  {
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

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < lines.Length; i++)
    {
      lines[i].SetPosition(0, positions[i].position);
    }

    state = State.IDLE;
    mouseDown = false;
    pekBody = pek.GetComponent<Rigidbody>();
    origin = positions[2].position;
  }

  // Update is called once per frame
  void Update()
  {
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
      if (state == State.IDLE)
      {
        for (int i = 0; i < lines.Length; i++)
        {
          lines[i].SetPosition(1, stripPoints[0].position);
        }
      }
      else
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
    }
  }
}