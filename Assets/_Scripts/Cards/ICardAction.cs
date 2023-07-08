using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface ICardAction<TResult> 
{
    T Execute<T>();
    
}
