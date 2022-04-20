using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostStageTests
{
  private GameData data;

  [SetUp]
  public void Setup()
  {
    data = Resources.Load<GameData>("Data/Game");
    SceneManager.LoadScene("PostStage");
  }

  [TearDown]
  public void TearDown()
  {
    data.Clean();
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
    data.stars = 3;
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
    data.stars = 2;
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
    data.pieces = 1;
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
    data.pieces = 2;
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