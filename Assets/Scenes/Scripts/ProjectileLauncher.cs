using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab; 
    public void FireProjectile()
    {
        Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
    }
}
