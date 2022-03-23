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
  private float launchForce = 500f;

  [SerializeField]
  private float upForce = 200f;

  private bool mouseDown;
  private State state;
  Rigidbody pekBody;
  private Vector3 origin;

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < positions.Length; i++)
    {
      lines[i].SetPosition(0, positions[i].position);
    }

    state = State.IDLE;
    mouseDown = false;
    pekBody = pek.GetComponent<Rigidbody>();
    origin = pek.position;
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
          pek.position = new Vector3(worldPosition.x, worldPosition.y, 0);
          pek.LookAt(origin, Vector3.forward * 5f);
          for (int i = 0; i < lines.Length; i++)
          {
            lines[i].SetPosition(1, worldPosition);
          }
        }
      }
    }
    else
    {
      if (state == State.IDLE)
      {
        foreach (LineRenderer line in lines)
        {
          line.SetPosition(1, pek.position);
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
      pekBody.AddForce(((origin - pek.position) * launchForce) + (Vector3.up * upForce), ForceMode.Acceleration);
    }
  }
}