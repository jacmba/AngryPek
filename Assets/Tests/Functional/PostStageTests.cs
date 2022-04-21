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
    Game game = new Game(0, 3);
    data.FinishStage(game);
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
    Game game = new Game(0, 2);
    data.FinishStage(game);
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
    Game game = new Game(0, 1);
    data.FinishStage(game);
    //yield return new WaitForSeconds(5);
    GameObject piece1 = GameObject.Find("PizzaBitPrefab");
    GameObject piece2 = GameObject.Find("PizzaBitPrefab (1)");

    Assert.IsNull(piece1);

    yield return new WaitForSeconds(7);

    piece1 = GameObject.Find("PizzaBitPrefab");
    Assert.IsTrue(piece1.activeSelf);
    Assert.IsNull(piece2);
  }

  [UnityTest]
  public IEnumerator PostStageTestShouldHaveTwoActivePizzaBits()
  {
    Game game = new Game(0, 2);
    data.pieces = 1;
    data.FinishStage(game);
    yield return new WaitForSeconds(5);
    GameObject piece1 = GameObject.Find("PizzaBitPrefab");
    GameObject piece2 = GameObject.Find("PizzaBitPrefab (1)");
    GameObject piece3 = GameObject.Find("PizzaBitPrefab (2)");

    Assert.IsTrue(piece1.activeSelf);
    Assert.IsNull(piece2);
    Assert.IsNull(piece3);

    yield return new WaitForSeconds(5);

    piece2 = GameObject.Find("PizzaBitPrefab (1)");
    piece3 = GameObject.Find("PizzaBitPrefab (2)");
    Assert.IsTrue(piece2.activeSelf);
    Assert.IsNull(piece3);
  }
}