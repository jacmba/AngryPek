using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirachinasThrow : MonoBehaviour
{
  [SerializeField]
  private Transform[] positions;

  [SerializeField]
  private LineRenderer[] lines;

  public Transform testObj;

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < positions.Length; i++)
    {
      lines[i].SetPosition(0, positions[i].position);
    }
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 mousePos = Input.mousePosition;
    mousePos.z = Camera.main.nearClipPlane;
    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
    Ray ray = Camera.main.ScreenPointToRay(mousePos);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
      worldPosition = hit.point;
      worldPosition.z = 0;
      Debug.Log(worldPosition);
      testObj.position = new Vector3(worldPosition.x, worldPosition.y, 0);
      for (int i = 0; i < lines.Length; i++)
      {
        lines[i].SetPosition(1, worldPosition);
      }
    }
  }
}
