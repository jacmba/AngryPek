using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BasicTest
{
  // A Test behaves as an ordinary method
  [Test]
  public void BasicTestSimplePasses()
  {
    // Use the Assert class to test conditions
    Assert.IsTrue(true, "The truth should be true");
  }
}
