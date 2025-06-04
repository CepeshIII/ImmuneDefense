using NUnit.Framework;
using UnityEngine;

public class MoveByPathDistributedModeTest
{
    [Test]
    public void GetTargetPosition_WhenPathIsEmpty_ReturnsCurrentPosition()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        moveByPathDistributed.SetPath(new Vector3[] { });

        Assert.AreEqual(gameObject.transform.position, moveByPathDistributed.GetTargetPosition(),
            "If path is empty, method should return current position of GameObject.");
    }

    [Test]
    public void GetTargetPosition_WhenPathHasOnePoint_ReturnsThatPoint()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        var pathPoint = new Vector3(0, 0, 1);
        moveByPathDistributed.SetPath(new Vector3[] { pathPoint });

        Assert.AreEqual(pathPoint, moveByPathDistributed.GetTargetPosition(),
            "If path has one point, method should return that point.");
    }

    [Test]
    public void GetTargetPosition_WhenPathHasTwoPoints_ReturnsCorrectPointsAfterCheck()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        var pathPoint1 = new Vector3(0, 0, 0.01f);
        var pathPoint2 = new Vector3(0, 0, 0.02f);

        moveByPathDistributed.SetPath(new Vector3[] { pathPoint1, pathPoint2 });
        moveByPathDistributed.SetThresholdDistance(1f);

        Assert.AreEqual(pathPoint1, moveByPathDistributed.GetTargetPosition(),
            "Initially, should return first path point.");

        moveByPathDistributed.CheckDistanceToNexPathPoint();

        Assert.AreEqual(pathPoint2, moveByPathDistributed.GetTargetPosition(),
            "After passing threshold, should return second path point.");
    }

    [Test]
    public void GetProjectedTargetPosition_WhenUnderFirstPoint_ReturnsCorrectProjection()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        var pathPoint1 = new Vector3(0f, 0f, 0f);
        var pathPoint2 = new Vector3(1f, 0f, 0f);

        moveByPathDistributed.SetPath(new Vector3[] { pathPoint1, pathPoint2 });
        moveByPathDistributed.SetPathRadius(10f);

        gameObject.transform.position = new Vector3(-1f, -1f, 0f);
        Assert.AreEqual(new Vector3(0f, -1f, 0f), moveByPathDistributed.GetProjectedTargetPosition());
    }

    [Test]
    public void CheckDistanceToNextPathPoint_WhenCloserThanThreshold_IncrementsIndex()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        var pathPoint1 = new Vector3(0f, 0f, 0f);
        var pathPoint2 = new Vector3(1f, 0f, 0f);

        gameObject.transform.position = new Vector3(-1f, -10f, 0f);
        moveByPathDistributed.SetPath(new Vector3[] { pathPoint1, pathPoint2 });
        moveByPathDistributed.SetThresholdDistance(2f);
        moveByPathDistributed.SetPathRadius(15f);

        Assert.AreEqual(0, moveByPathDistributed.GetIndex());
        moveByPathDistributed.CheckDistanceToNexPathPoint();
        Assert.AreEqual(1, moveByPathDistributed.GetIndex());
    }

    [Test]
    public void CheckDistanceToNextPathPoint_WhenFarFromOrigin_StillSwitchesIndexCorrectly()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        var pathPoint1 = new Vector3(-13f, 11f, 0f);
        var pathPoint2 = new Vector3(-14f, 10.5f, 0f);

        gameObject.transform.position = new Vector3(-13.5f, 10.5f, 0f);
        moveByPathDistributed.SetPath(new Vector3[] { pathPoint1, pathPoint2 });
        moveByPathDistributed.SetThresholdDistance(1f);
        moveByPathDistributed.SetPathRadius(2f);

        Assert.AreEqual(0, moveByPathDistributed.GetIndex());
        moveByPathDistributed.CheckDistanceToNexPathPoint();
        Assert.AreEqual(1, moveByPathDistributed.GetIndex());
    }

    [Test]
    public void GetProjectedTargetPosition_WhenFarFromPath_ReturnsPathPointNotProjection()
    {
        var gameObject = new GameObject();
        var moveByPathDistributed = gameObject.AddComponent<MoveByPathDistributed>();
        var pathPoint1 = new Vector3(16f, 0f, 0f);
        var pathPoint2 = new Vector3(17f, 0f, 0f);

        gameObject.transform.position = new Vector3(14f, -10f, 0f);
        moveByPathDistributed.SetPath(new Vector3[] { pathPoint1, pathPoint2 });
        moveByPathDistributed.SetThresholdDistance(0.2f);
        moveByPathDistributed.SetPathRadius(10f);

        Assert.AreNotEqual(moveByPathDistributed.GetPath()[moveByPathDistributed.GetIndex()],
                           moveByPathDistributed.GetProjectedTargetPosition());
    }
}
