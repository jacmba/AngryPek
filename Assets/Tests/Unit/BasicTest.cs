using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

public class BasicTest
{
  // A Test behaves as an ordinary method
  [Test]
  public void BasicTestSimplePasses()
  {
    // Use the Assert class to test conditions
    Console.WriteLine("Hello tests!");
    Assert.IsTrue(true, "The truth should be true");
  }
}
