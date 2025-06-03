using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;



public class EffectsMoverRuntimeTest
{
    [UnityTest]
    public IEnumerator SetEffect_AppendStackableEffectAndCheckModifierOnNextFrame()
    {
        var newGameObject = new GameObject();
        var logic = newGameObject.AddComponent<EffectsMoveLogic>();

        // ADD a Rigidbody2D so that FixedUpdate runs and works
        var rb = newGameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Inject into private field via reflection
        typeof(EffectsMoveLogic)
            .GetField("rigidbody2D", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(logic, rb);

        logic.SetEffect(new SlowMovingEffect(1)
        {
            isStackable = true,
            slowFactor = 0.5f,
            timer = 2f
        });

        yield return new WaitForFixedUpdate(); // Now FixedUpdate should run

        var field = typeof(EffectsMoveLogic)
            .GetField("speedModifier", BindingFlags.NonPublic | BindingFlags.Instance);

        float result = (float)field.GetValue(logic);
        Debug.Log("Speed modifier after 1 FixedUpdate: " + result);

        Assert.AreEqual(0.5f, result, 0.01f);

        // Wait for expiration
        yield return new WaitForSeconds(2f);
        yield return new WaitForFixedUpdate();

        result = (float)field.GetValue(logic);
        Debug.Log("Speed modifier after expiration: " + result);

        Assert.AreEqual(1f, result, 0.01f);
    }
}
