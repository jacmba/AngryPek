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
    data.attempts = 5;
    data.level = 10;

    data.Clean();
    Assert.AreEqual(data.attempts, 3);
    data.level = 1;
  }
}
