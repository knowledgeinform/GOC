using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform DfinalPoint;
    private Transform pointAB;
    private Transform pointBC;
    private Transform pointCD;
    private Transform pointAB_BC;
    private Transform pointBC_CD;
    [SerializeField] private Transform guide;
    private float interpolateAmmount;

    private void Update()
    {
        interpolateAmmount = (interpolateAmmount + Time.deltaTime) % 1f;
        guide.position = CubicLerp(pointA.position, pointB.position, pointC.position, DfinalPoint.position, interpolateAmmount);
    }
    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, interpolateAmmount);
    }
    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(ab_bc, bc_cd, interpolateAmmount);
    }

}
