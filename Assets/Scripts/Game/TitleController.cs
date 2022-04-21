using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
  private bool canTouch;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    canTouch = false;
  }

  // Update is called once per frame
  void Update()
  {
    if (EventSystem.current.currentSelectedGameObject != null)
    {
      return;
    }

    if (Input.GetMouseButtonDown(0) && canTouch)
    {
      SceneManager.LoadScene(1);
    }

    if (!Input.GetMouseButton(0))
    {
      canTouch = true;
    }
  }

  public void OnExitClick()
  {
    Debug.Log("Bye!");
    Application.Quit();
  }
}
