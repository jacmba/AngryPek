using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
  [SerializeField] GameData data;
  private bool canTouch;

  // Start is called before the first frame update
  void Start()
  {
    canTouch = false;
    data.Clean();
    StartCoroutine(DeferGotoTitle());
  }

  IEnumerator DeferGotoTitle()
  {
    yield return new WaitForSeconds(5f);
    GotoTitle();
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    if (Input.GetMouseButtonDown(0) && canTouch)
    {
      GotoTitle();
    }

    if (!Input.GetMouseButton(0))
    {
      canTouch = true;
    }
  }

  void GotoTitle()
  {
    SceneManager.LoadScene("TitleScreen");
  }
}
