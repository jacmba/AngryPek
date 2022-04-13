using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GameController.Clean();
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
    if (Input.anyKeyDown)
    {
      GotoTitle();
    }
  }

  void GotoTitle()
  {
    SceneManager.LoadScene("TitleScreen");
  }
}
