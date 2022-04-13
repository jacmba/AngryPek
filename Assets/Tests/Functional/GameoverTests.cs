using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverTests
{
  [UnityTest]
  public IEnumerator GameoverTestShouldResetData()
  {
    GameController.maxAttempts = 10;
    SceneManager.LoadScene("Gameover");
    yield return new WaitForSeconds(.1f);

    Assert.AreEqual(3, GameController.maxAttempts);
  }

  [UnityTest]
  public IEnumerator GameoverTestShouldGoToTitleScreenAfterFiveSeconds()
  {
    SceneManager.LoadScene("Gameover");
    yield return new WaitForSeconds(6);
    Scene scene = SceneManager.GetActiveScene();
    Assert.AreEqual(0, scene.buildIndex);
    Assert.AreEqual("TitleScreen", scene.name);
  }

  [UnityTest]
  public IEnumerator GameoverTestShouldHaveGameOverText()
  {
    SceneManager.LoadScene("Gameover");
    yield return null;

    Text txt = Transform.FindObjectOfType<Text>();
    Assert.AreEqual("Game Over", txt.text);
  }
}
