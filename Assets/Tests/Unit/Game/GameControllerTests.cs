using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameControllerTests
{
  private GameData data;

  [SetUp]
  public void Setup()
  {
    data = Resources.Load<GameData>("Data/Game");
  }

  [TearDown]
  public void TearDown()
  {
    data.Clean();
  }

  [Test]
  public void GameControllerTestDataClean()
  {
    data.attempts = 10;
    data.level = 5;

    data.Clean();
    Assert.AreEqual(data.attempts, 3);
    Assert.AreEqual(1, data.level);
  }
}
