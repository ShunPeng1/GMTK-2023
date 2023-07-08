using _Scripts.DataWrapper;
using UnityEngine;

namespace _Scripts.Actor
{
    public enum ActorRole
    {
        Ally,
        Enemy,
        Other
    }
    
    public class ActorBehavior : MonoBehaviour
    {
        public ObservableData<ActorRole> Role = new ObservableData<ActorRole>(ActorRole.Ally);
        public ObservableData<float> Health = new ObservableData<float>(10);
        public ObservableData<float> Strength = new (5); 
        public ObservableData<float> Gold = new (5);
        public ObservableData<float> Turns;

        public void Attack(ActorBehavior beingAttackedActor)
        {
            beingAttackedActor.Health.Value -= Strength.Value;
        }
        
    }
}