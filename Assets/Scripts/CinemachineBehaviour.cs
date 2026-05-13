using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineBehaviour : MonoBehaviour
{
    private CinemachineConfiner3D cineConf;

    private readonly List<CameraBoundZone> boundZones = new();

    private BoxCollider currentBoxCollider;

    private class CameraBoundZone
    {
        public float minY;
        public float maxY;
        public string tag;
        public BoxCollider boxCollider;

        public CameraBoundZone(float minY, float maxY, string tag)
        {
            this.minY = minY;
            this.maxY = maxY;
            this.tag = tag;
        }

        public bool Contains(float y)
        {
            return y >= minY && y < maxY;
        }
    }

    private void Start()
    {
        cineConf = GetComponent<CinemachineConfiner3D>();

        CreateBoundZones();
        FindBoundColliders();

        SetBoundingVolume(GetZoneByPlayerY(PlayerBehaviour.playerPosition.y));
    }

    private void Update()
    {
        CameraBoundZone targetZone = GetZoneByPlayerY(PlayerBehaviour.playerPosition.y);

        SetBoundingVolume(targetZone);
    }

    private void CreateBoundZones()
    {
        boundZones.Add(new CameraBoundZone(float.NegativeInfinity, 7.8f, "FirstFloor"));
        boundZones.Add(new CameraBoundZone(7.8f, 8.6f, "Transition_1_2"));
        boundZones.Add(new CameraBoundZone(8.6f, float.PositiveInfinity, "SecondFloor"));

        // Quando adicionar o terceiro andar, você pode trocar a zona do SecondFloor
        // e adicionar mais duas zonas assim:
        //
        // boundZones.Add(new CameraBoundZone(8.6f, 14.8f, "SecondFloor"));
        // boundZones.Add(new CameraBoundZone(14.8f, 15.6f, "Transition_2_3"));
        // boundZones.Add(new CameraBoundZone(15.6f, float.PositiveInfinity, "ThirdFloor"));
    }

    private void FindBoundColliders()
    {
        foreach (CameraBoundZone zone in boundZones)
        {
            GameObject boundObject = GameObject.FindGameObjectWithTag(zone.tag);

            zone.boxCollider = boundObject.GetComponent<BoxCollider>();
        }
    }

    private CameraBoundZone GetZoneByPlayerY(float playerY)
    {
        foreach (CameraBoundZone zone in boundZones)
        {
            if (zone.Contains(playerY))
            {
                return zone;
            }
        }

        return null;
    }

    private void SetBoundingVolume(CameraBoundZone zone)
    {
        if (zone == null || zone.boxCollider == null)
        {
            return;
        }

        if (zone.boxCollider == currentBoxCollider)
        {
            return;
        }

        currentBoxCollider = zone.boxCollider;
        cineConf.BoundingVolume = currentBoxCollider;
    }
}