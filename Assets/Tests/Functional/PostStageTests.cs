using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostStageTests
{
  [SetUp]
  public void Setup()
  {
    SceneManager.LoadScene("PostStage");
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldBeInPostStageScene()
  {
    yield return null;
    Scene scene = SceneManager.GetActiveScene();
    Assert.AreEqual("PostStage", scene.name);
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldContainLevelClearedText()
  {
    yield return null;
    GameObject txtObj = GameObject.Find("ClearedText");

    Assert.IsNotNull(txtObj);

    Text txt = txtObj.GetComponent<Text>();
    Assert.IsNotNull(txt);
    Assert.AreEqual("Level cleared!", txt.text);
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldEnableAllStars()
  {
    GameController.achievedStars = 3;
    yield return new WaitForSeconds(3);
    GameObject star1 = GameObject.Find("star");
    GameObject star2 = GameObject.Find("star (1)");
    GameObject star3 = GameObject.Find("star (2)");

    Assert.IsTrue(star1.activeSelf);
    Assert.IsTrue(star2.activeSelf);
    Assert.IsTrue(star3.activeSelf);
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldHaveTwoEnabledStars()
  {
    GameController.achievedStars = 2;
    yield return new WaitForSeconds(3);
    GameObject star1 = GameObject.Find("star");
    GameObject star2 = GameObject.Find("star (1)");
    GameObject star3 = GameObject.Find("star (2)");

    Assert.IsTrue(star1.activeSelf);
    Assert.IsTrue(star2.activeSelf);
    Assert.IsNull(star3);
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldHaveOneActivePizzaBit()
  {
    GameController.achievedPieces = 1;
    yield return new WaitForSeconds(5);
    GameObject piece1 = GameObject.Find("PizzaBitPrefab");

    Assert.IsNull(piece1);

    yield return new WaitForSeconds(5);

    piece1 = GameObject.Find("PizzaBitPrefab");
    Assert.IsTrue(piece1.activeSelf);
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldHaveTwoActivePizzaBits()
  {
    GameController.achievedPieces = 2;
    yield return new WaitForSeconds(5);
    GameObject piece1 = GameObject.Find("PizzaBitPrefab");
    GameObject piece2 = GameObject.Find("PizzaBitPrefab (1)");

    Assert.IsTrue(piece1.activeSelf);
    Assert.IsNull(piece2);

    yield return new WaitForSeconds(5);

    piece2 = GameObject.Find("PizzaBitPrefab (1)");
    Assert.IsTrue(piece2.activeSelf);
  }
}