using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Effect
{

    public float timer;
    public bool isStackable;

    private int sourceId;
    private bool isEnded = false;

    public bool IsEnded => isEnded;
    public int SourceId => sourceId;


    public Effect(int sourceId)
    {
        this.sourceId = sourceId;
    }

    public virtual void Process(IEffectsHandler effectsHandler, IEffectsModifier effectsModifier, float deltaTime) 
    {
        timer -= deltaTime;
        if(timer <= 0)
        {
            isEnded = true;
        }
    }

    public void Stack(Effect newEffect)
    {
        if (this.timer < newEffect.timer)
        {
            this.timer = newEffect.timer;
        }
        this.sourceId = newEffect.sourceId;
        this.isStackable = newEffect.isStackable;
        this.isEnded = false;
    }
}


public class MovingEffect: Effect
{
    public MovingEffect(int sourceId) : base(sourceId)
    {
    }

    public override void Process(IEffectsHandler effectsHandler, IEffectsModifier effectsModifier, float deltaTime)
    {
        base.Process(effectsHandler, effectsModifier, deltaTime);
    }
}

[Serializable]
public class SlowMovingEffect: MovingEffect
{
    public float slowFactor = 0.5f;

    public SlowMovingEffect(int sourceId) : base(sourceId)
    {
    }

    public override void Process(IEffectsHandler effectsHandler, IEffectsModifier effectsModifier, float deltaTime)
    {
        base.Process(effectsHandler, effectsModifier, deltaTime);
        effectsModifier.UpdateModifier(Mathf.Min(slowFactor, 1f));
    }
}


public interface IEffectsHandler
{
    public EffectStorage GetStorage();

    public void SetEffect(Effect effect);
    public IEnumerable<Effect> GetEffects(int sourceId);
}


public interface IEffectsModifier
{
    public void UpdateModifier(float value);
}


public class EffectStorage
{
    private readonly Dictionary< int, Dictionary<Type, List<Effect>>> effectsDictionary = new();
    //private readonly Dictionary<Type, List<Effect>> effectsDictionary = new();


    public void Add(Effect effect)
    {       
        if (effectsDictionary.TryGetValue(effect.SourceId, out var effectsDictionaryByType) && effectsDictionaryByType != null)
        {
            if (effectsDictionaryByType.TryGetValue(effect.GetType(), out var effectsList) && effectsList != null)
            {
                bool wasAddToNullPlace = false;
                for (var i = 0; i < effectsList.Count; i++)
                {
                    var effectInList = effectsList[i];
    
                    if (effectInList == null)
                    {
                        effectsList[i] = effect;
                        wasAddToNullPlace = true;
                        continue;
                    }
                    else if (effect.isStackable && effectInList.isStackable)
                    {
                        effectInList.Stack(effect);
                        wasAddToNullPlace = true;
                    }
                }
    
                if (!wasAddToNullPlace)
                {
                    effectsList.Add(effect);
                }
    
                //effectsDictionaryByType[effect.GetType()] = effectsList;
            }
            else
            {
                effectsList = new List<Effect>();
                effectsList.Add(effect);
                Debug.Log("Add to new Creating list");

                effectsDictionaryByType.Add(effect.GetType(), effectsList);
            }
            //effectsDictionary[effect.sourceId] = effectsDictionaryByType;
        }
        else
        {
            effectsDictionaryByType = new Dictionary<Type, List<Effect>>();
            var effectsList = new List<Effect>();
            effectsList.Add(effect);
            effectsDictionaryByType.Add(effect.GetType(), effectsList);
            effectsDictionary.Add(effect.SourceId, effectsDictionaryByType);
        }
    }

    public void EffectsProcess(IEffectsHandler effectsHandler, IEffectsModifier IEffectsModifier, float deltaTime)
    {
        foreach(var effectsDictionaryKey in effectsDictionary.Keys)
        {
            var effectsDictionaryByType = effectsDictionary[effectsDictionaryKey];
            foreach (var effectsDictionaryByTypeKey in effectsDictionaryByType.Keys)
            {
                var list = effectsDictionaryByType[effectsDictionaryByTypeKey];

                for(int i = 0; i < list.Count; i++)
                {
                    var effect = list[i];
                    if(effect == null)
                    {
                        continue;
                    }
                    else if (effect.IsEnded)
                    {
                        list[i] = null;
                    }
                    else
                    {
                        effect.Process(effectsHandler, IEffectsModifier, deltaTime);
                    }
                }
            }
        }
    }

    public int GetCountById(int id)
    {
        if(effectsDictionary.TryGetValue(id, out var effectsDictionaryByType))
        {
            return effectsDictionaryByType.Count;
        }

        return -1;
    }

    public int GetCountByType(int id, Type type)
    {
        if (effectsDictionary.TryGetValue(id, out var effectsDictionaryByType))
        {
            if (effectsDictionaryByType.TryGetValue(type, out var effectsList))
            {
                return effectsList.Count;
            }
        }

        return -1;
    }

    public IEnumerable<Effect> GetEffects(int sourceId)
    {
        if (effectsDictionary.ContainsKey(sourceId))
            return effectsDictionary[sourceId].Values.SelectMany((e => e));
        return null;
    }

    public void DeleteEffect(Effect effect)
    {
        if (effectsDictionary.TryGetValue(effect.SourceId, out var effectsDictionaryByType))
        {
            if (effectsDictionaryByType.TryGetValue(effect.GetType(), out var ListOfEffects))
            {
                ListOfEffects.Remove(effect);
            }
        }
    }

    public IEnumerable<Effect> GetAll() => effectsDictionary.Values.SelectMany(x => x.Values.SelectMany(e => e));
}




