using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Cards
{
    
    public enum WordCardType
    {
        Noun,
        Verb,
        Value,
        Condition,
    }
    
    public class BaseCardInformation
    {
        
        public string Name;
        public WordCardType WordCardType;
        
        private List<object> _objectList = new();


        public void AddObject(object obj)
        {
            _objectList.Add(obj);
        }

        public T GetObjectByType<T>()
        {
            foreach (object obj in _objectList)
            {
                if (obj is T)
                {
                    return (T)obj;
                }
            }

            return default(T);
        }

        public void RemoveObject(object obj)
        {
            _objectList.Remove(obj);
        }
    
        
        public virtual void Execute()
        {
            
        }

        
    }
}