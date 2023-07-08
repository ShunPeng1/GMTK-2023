using _Scripts.DataWrapper;
using UnityEngine;

namespace _Scripts.Actor
{
    public class ActorBehavior : MonoBehaviour
    {
        public ObservableData<float> Health;
        public ObservableData<float> Strength; 
        public ObservableData<float> Gold;
        public ObservableData<float> Turns;
    }
}