using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TitleTests
{
  [SetUp]
  public void SetUp()
  {
    SceneManager.LoadScene(0);
  }

  // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
  // `yield return null;` to skip a frame.
  [UnityTest]
  public IEnumerator TitleTestsWithEnumeratorPasses()
  {
    // Use the Assert class to test conditions.
    // Use yield to skip a frame.
    yield return null;
  }

  [UnityTest]
  public IEnumerator TestRotatingPek()
  {
    Transform pek = GameObject.FindWithTag("Player").transform;
    Quaternion preRotation = pek.rotation;
    Debug.Log("Original rotation: " + preRotation);
    yield return new WaitForSeconds(1f);
    Quaternion postRotation = pek.rotation;
    Debug.Log("Final rotation: " + postRotation);
    Assert.AreNotEqual(preRotation, postRotation);
  }
}
