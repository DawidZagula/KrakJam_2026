using System.Collections.Generic;
using UnityEngine;

public class FloorLayerManager : MonoBehaviour
{
    [Header("Debugging Only")]
    [SerializeField] private Transform _floorPartObjectPrefab;
    [SerializeField] private Transform _floorPartsParent;

    [Header("Configuration")]
    [SerializeField] private int _numberOfFloorPartsAtStart;

    [SerializeField] private float _floorPartLength;

    [SerializeField] private float _moveSpeed;

    private Transform[] _spawnedFloorParts = new Transform[12];

    private void Start()
    {
        InstantiateStartFloorParts();
    }

    private void Update()
    {
        MoveFloorParts();
    }

    private void InstantiateStartFloorParts()
    {
        for (int i = 0; i < _numberOfFloo11111rPartsAtStart; i++)
        {
            float spawnXOffset = GetSpawnXOffset(i);

            Vector3 spawmPosition = new Vector3(spawnXOffset, 0, 0);

           Transform spawnFloorPartInstance =
                Instantiate(_floorPartObjectPrefab, spawmPosition, transform.rotation, _floorPartsParent);

            _spawnedFloorParts[i] = spawnFloorPartInstance;
        }
    }

    private float GetSpawnXOffset(int i)
    {
        float spawnXOffset;
        if (i == 0)
        {
            spawnXOffset = 0f;
        }
        else
        {
            spawnXOffset = _floorPartLength * i;
        }

        return spawnXOffset;
    }

    private void MoveFloorParts()
    {
        for (int i = 0; i < _spawnedFloorParts.Length; i++)
        {
            _spawnedFloorParts[i].transform.Translate(-transform.right * (_moveSpeed * Time.deltaTime));
        }
    }
}
