using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{

  public void OnPlayClick()
  {
    SceneManager.LoadScene(1);
  }

  public void OnExitClick()
  {
    Debug.Log("Bye!");
    Application.Quit();
  }
}
