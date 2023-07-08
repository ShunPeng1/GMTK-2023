using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _centerParent;
    [SerializeField] private List<NumberSlot> _numberSlots;

    [SerializeField] private float _arcSpacing = 0.1f;
    [SerializeField] private float _numberSlotRange = 10f;
    [SerializeField] private float _numberSlotThickness = 3f;
    [SerializeField] private float _numberSlotTextSize = 36f;
    void Awake()
    {
        GenerateWheel();

    }
    

    private void GenerateWheel()
    {
        float currentDegree = 0;
        float arcDegree = CalculateArcAngleInDegree(_numberSlots.Count, _arcSpacing); 
        foreach (var numberSlot in _numberSlots)
        {
            var disc = Instantiate(ResourceManager.Instance.WheelNumberSlot, transform);
            WheelArc wheelArc = new WheelArc(
                currentDegree,
                 arcDegree,
                _numberSlotRange,
                _numberSlotThickness,
                _numberSlotTextSize);
            disc.Init(numberSlot, wheelArc);
            
            currentDegree += arcDegree + _arcSpacing;

        }
        
    }
    
    float CalculateArcAngleInDegree(int numArcs, float spacing)
    {
        // Calculate the angle for each arc
        float arcAngle = (360f / numArcs) - spacing;
        return arcAngle;
    }
    
    
    public bool removeExistingColliders = true;
 
    public void CreateInvertedMeshCollider()
    {
        if (removeExistingColliders)
            RemoveExistingColliders();
 
        InvertMesh();
 
        gameObject.AddComponent<MeshCollider>();
    }
 
    private void RemoveExistingColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
            DestroyImmediate(colliders[i]);
    }
 
    private void InvertMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
}
