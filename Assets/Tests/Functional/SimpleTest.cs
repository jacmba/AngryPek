using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SimpleTest
{

  // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
  // `yield return null;` to skip a frame.
  [UnityTest]
  public IEnumerator SimpleTestWithEnumeratorPasses()
  {
    // Use the Assert class to test conditions.
    // Use yield to skip a frame.
    yield return null;

    Assert.IsTrue(true);
  }
}
