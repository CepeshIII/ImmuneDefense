using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;



public class EffectsMoverEditorModeTest
{
    class TestHandler : MonoBehaviour, IEffectsHandler, IEffectsModifier
    {
        public List<float> receivedModifiers = new List<float>();

        public IEnumerable<Effect> GetEffects(int sourceId)
        {
            return new List<Effect>();
        }

        public EffectStorage GetStorage()
        {
            return null;
        }

        public void SetEffect(Effect effect) { }
        public void UpdateModifier(float value) => receivedModifiers.Add(value);
    }



    [Test]
    public void SlowMovingEffect_CallsUpdateModifier()
    {
        var handler = new GameObject().AddComponent<TestHandler>();
        var effect = new SlowMovingEffect(1) { slowFactor = 0.7f };

        effect.Process(handler, handler, Time.deltaTime);

        Assert.AreEqual(1, handler.receivedModifiers.Count);
        Assert.AreEqual(0.7f, handler.receivedModifiers[0]);
    }


    [Test]
    public void SetEffect_AddsNewEffectToDictionary()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var effect = new Effect(42);

        logic.SetEffect(effect);

        Assert.AreEqual(1, logic.GetStorage().GetCountById(42));
        Assert.IsTrue(logic.GetEffects(42).Contains(effect));
    }


    [Test]
    public void SetEffect_AddsTwoNewEffectsToDictionary()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var effect1 = new Effect(42);
        var effect2 = new Effect(56);

        logic.SetEffect(effect1);
        logic.SetEffect(effect2);

        Assert.AreEqual(1, logic.GetEffects(42).Count());
        Assert.AreEqual(1, logic.GetEffects(56).Count());

        Assert.IsTrue(logic.GetEffects(42).Contains(effect1));
        Assert.IsTrue(logic.GetEffects(56).Contains(effect2));
    }


    [Test]
    public void SetEffect_AppendStackableEffectToExistingList()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var effect1 = new Effect(1) {isStackable = true};
        var effect2 = new Effect(1) {isStackable = true};

        logic.SetEffect(effect1);
        logic.SetEffect(effect2);

        Assert.AreEqual(1, logic.GetEffects(1).Count());
    }


    [Test]
    public void SetEffect_AppendNotStackableEffectToExistingList()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var effect1 = new Effect(1) { isStackable = false };
        var effect2 = new Effect(1) { isStackable = false };

        logic.SetEffect(effect1);
        logic.SetEffect(effect2);

        Assert.AreEqual(2, logic.GetEffects(1).Count());
    }


    [Test]
    public void SetEffect_AppendThreeStackableEffectToExistingList()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var effect1 = new Effect(1) { isStackable = true };
        var effect2 = new Effect(1) { isStackable = true };
        var effect3 = new Effect(1) { isStackable = true };

        logic.SetEffect(effect1);
        logic.SetEffect(effect2);
        logic.SetEffect(effect3);

        Assert.AreEqual(1, logic.GetEffects(1).Count());
    }


    [Test]
    public void SetEffect_AddThreeStackableEffectWithDifferentIdToExistingList()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var effect1 = new Effect(1) {isStackable = true };
        var effect2 = new Effect(2) {isStackable = true };
        var effect3 = new Effect(3) {isStackable = true };

        logic.SetEffect(effect1);
        logic.SetEffect(effect2);
        logic.SetEffect(effect3);

        Assert.AreEqual(1, logic.GetEffects(1).Count());
        Assert.AreEqual(1, logic.GetEffects(2).Count());
        Assert.AreEqual(1, logic.GetEffects(3).Count());

        Assert.AreEqual(3, logic.GetStorage().GetAll().Count());
    }


    [Test]
    public void UpdateModifier_MultipliesModifier()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();

        logic.UpdateModifier(0.5f);
        logic.UpdateModifier(0.5f);

        var field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float result = (float)field.GetValue(logic);

        Assert.AreEqual(0.25f, result);
    }


    [Test]
    public void AddEffectAndCallProcessForEffects()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 0.1f });

        storage.EffectsProcess(logic, logic, 1f);

        var field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float result = (float)field.GetValue(logic);

        Assert.AreEqual(0.5f, result);
    }

    [Test]
    public void AddTwoNonStackableEffectAndCallProcessForEffects()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 0.1f });
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 0.1f });

        storage.EffectsProcess(logic, logic, 1f);

        var field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float result = (float)field.GetValue(logic);

        Assert.AreEqual(0.25f, result);
    }

    [Test]
    public void AddTwoStackableEffectAndCallProcessForEffects()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 0.1f });
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 0.1f });

        storage.EffectsProcess(logic, logic, 1f);

        var field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float result = (float)field.GetValue(logic);

        Assert.AreEqual(0.5f, result);
    }


    [Test]
    public void SetEffect_AppendNonStackableEffectToEffectsHandlerWithNullEffectInStorage()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 0.1f });

        storage.EffectsProcess(logic, logic, 1f);
        storage.EffectsProcess(logic, logic, 1f);

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 0.1f });

        Assert.AreEqual(1, storage.GetCountByType(1, typeof(SlowMovingEffect)));
        Assert.IsFalse(storage.GetAll().Any((e) => e == null));
    }

    [Test]
    public void SetEffect_AppendStackableEffectToEffectsHandlerWithNullEffectInStorage()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 0.1f });

        storage.EffectsProcess(logic, logic, 1f);
        storage.EffectsProcess(logic, logic, 1f);

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 0.1f });

        Assert.AreEqual(1, storage.GetCountByType(1, typeof(SlowMovingEffect)));
    }


    [Test]
    public void SetEffect_AppendStackableAndNonStackableEffectsAndCheckEffectEnd()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.ResetModifier();
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 1f });
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 1f });
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 10f });
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 10f });

        storage.EffectsProcess(logic, logic, 1f);
        var field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        float result = (float)field.GetValue(logic);
        Assert.AreEqual(0.5f * 0.5f * 0.5f, result);
        logic.ResetModifier();

        storage.EffectsProcess(logic, logic, 5f);
        field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        result = (float)field.GetValue(logic);
        Assert.AreEqual(0.5f, result);
        logic.ResetModifier();

        storage.EffectsProcess(logic, logic, 5f);
        field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        result = (float)field.GetValue(logic);
        Assert.AreEqual(0.5f, result);
        logic.ResetModifier();

        storage.EffectsProcess(logic, logic, 0.01f);

        field = typeof(EffectsMoveLogic).GetField("speedModifier", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        result = (float)field.GetValue(logic);

        Assert.AreEqual(1, result);
    }


    [Test]
    public void SetEffect_AppendStackableEffectToNonStackable()
    {
        var logic = new GameObject().AddComponent<EffectsMoveLogic>();
        var storage = logic.GetStorage();

        logic.SetEffect(new SlowMovingEffect(1) { isStackable = true, slowFactor = 0.5f, timer = 0.1f });
        logic.SetEffect(new SlowMovingEffect(1) { isStackable = false, slowFactor = 0.5f, timer = 0.1f });

        storage.EffectsProcess(logic, logic, 1f);
        storage.EffectsProcess(logic, logic, 1f);

        foreach (var item in logic.GetStorage().GetAll())
        {
            Assert.AreEqual(null, item);
        }
    }

}
