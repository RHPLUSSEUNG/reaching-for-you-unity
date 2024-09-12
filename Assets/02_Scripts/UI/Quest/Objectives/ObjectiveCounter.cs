using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ObjectiveCounter : MonoBehaviour, IPredicateEvaluator
{
    Dictionary<string, int> counts = new Dictionary<string, int>();

    public event System.Action OnCountChanged;
    
    //increase Counter
    public int AddToCount(string token, int amount, bool onlyIfExists = false)
    {
        if (!counts.ContainsKey(token))
        {
            if (onlyIfExists)
            {
                return 0;
                counts[token] = amount;
                OnCountChanged?.Invoke();
                return amount;
            }
        }
        counts[token] += amount;
        OnCountChanged?.Invoke();
        return counts[token];
    }

    public int RegisterCounter(string token, bool overwrite = false)
    {
        if(!counts.ContainsKey(token) || overwrite)
        {
            counts[token] = 0;
            OnCountChanged?.Invoke();
        }
        return counts[token];
    }

    public int GetCounterValue(string token)
    {
        if(!counts.ContainsKey(token))
        {
            return 0;
        }
        return counts[token];
    }

    public object CaptureState()
    {
        return counts;
    }

    public void RestoreState(object state)
    {
        counts = (Dictionary<string, int>)state;
        OnCountChanged?.Invoke();
    }

    public bool? Evaluate(QuestPredicates predicate, string[] parameters)
    {
        if (predicate == QuestPredicates.HASKILLED)
        {
            if (int.TryParse(parameters[1], out int intParameter))
            {
                RegisterCounter(parameters[0]);
                return counts[parameters[0]] >= intParameter;
            }
            return false;
        }
        return null;
    }

    public JToken CaptureAsJToken()
    {
        JObject state = new JObject();
        IDictionary<string, JToken> stateDict = state;
        foreach (KeyValuePair<string, int> keyValuePair in counts)
        {
            stateDict[keyValuePair.Key] = JToken.FromObject(keyValuePair.Value);
        }
        return state;
    }

    public void RestoreFromJToken(JToken state)
    {
        if (state is JObject stateObject)
        {
            IDictionary<string, JToken> stateDict = stateObject;
            counts.Clear();
            foreach (KeyValuePair<string, JToken> keyValuePair in stateDict)
            {
                counts[keyValuePair.Key] = keyValuePair.Value.ToObject<int>();
            }
            OnCountChanged?.Invoke();
        }
    }
}
