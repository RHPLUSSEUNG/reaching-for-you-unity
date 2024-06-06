using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPredicateEvaluator
{
    bool? Evaluate(QuestPredicates predicate, string[] parameters);    
}
