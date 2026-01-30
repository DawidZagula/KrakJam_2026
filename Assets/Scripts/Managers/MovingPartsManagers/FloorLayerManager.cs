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

    private List<Transform> _spawnedFloorParts = new List<Transform>();

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
        for (int i = 0; i < _numberOfFloorPartsAtStart; i++)
        {
            SpawnFloorPartInstance();
        }
    }

    private void SpawnFloorPartInstance()
    {
        float spawnXOffset = GetSpawnXOffset();

        Vector3 spawmPosition = new Vector3(spawnXOffset, 0, 0);

        Transform spawnFloorPartInstance =
             Instantiate(_floorPartObjectPrefab, spawmPosition, transform.rotation, _floorPartsParent);

        _spawnedFloorParts.Add(spawnFloorPartInstance);
    }

    private float GetSpawnXOffset()
    {
        float spawnXOffset;
        if (_spawnedFloorParts.Count == 0)
        {
            spawnXOffset = 0f;
        }
        else
        {
            //spawnXOffset = _floorPartLength * i;
            spawnXOffset = _spawnedFloorParts[_spawnedFloorParts.Count - 1].transform.position.x + _floorPartLength;
        }

        return spawnXOffset;
    }

    private void MoveFloorParts()
    {
        for (int i = 0; i < _spawnedFloorParts.Count; i++)
        {
            Transform spawnedFloorPartInstance = _spawnedFloorParts[i];
            spawnedFloorPartInstance.Translate(-transform.right * (_moveSpeed * Time.deltaTime));

            if (spawnedFloorPartInstance.position.x <= Camera.main.transform.position.x - _floorPartLength)
            {
                _spawnedFloorParts.Remove(spawnedFloorPartInstance);
                Destroy(spawnedFloorPartInstance.gameObject);
                SpawnFloorPartInstance();
            }
        }
    }
}
