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
    StartCoroutine(GotoTitle());
  }

  IEnumerator GotoTitle()
  {
    yield return new WaitForSeconds(5f);
    SceneManager.LoadScene("TitleScreen");
  }
}
