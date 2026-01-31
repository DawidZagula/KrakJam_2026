using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class FloorLayerManager : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private Transform _floorPartsParent;

    [SerializeField] private Transform _movablePartObjectPrefab;
   // [SerializeField] private Transform _foregroundPartPrefab;
   // [SerializeField] private Transform _backgroundPartPrefab;

    [Header("Configuration")]
    [SerializeField] private int _numberOfPartsAtStart;

    [SerializeField] private float _floorPartLength;
    [SerializeField] private float _backgroundPartLength;
    [SerializeField] private float _foregroundPartLength;

    [SerializeField] private float _floorMoveSpeed;
    [SerializeField] private float _foregroundMoveSpeed;
    [SerializeField] private float _backgroundMoveSpeed;

    private List<Transform> _spawnedFloorParts = new List<Transform>();

    private void Start()
    {
        InstantiateStartParts();
    }

    private void Update()
    {
        MoveFloorParts();
    }

    private void InstantiateStartParts()
    {
        for (int i = 0; i < _numberOfPartsAtStart; i++)
        {
            SpawnPartInstance(MovableParts.Floor);
        }
    }

    private void SpawnPartInstance(MovableParts movablePartType)
    {
        float spawnXOffset = GetSpawnXOffset();

        Vector3 spawmPosition = new Vector3(spawnXOffset, 0, 0);

        Transform spawnFloorPartInstance =
             Instantiate(_movablePartObjectPrefab, spawmPosition, transform.rotation, _floorPartsParent);

        _spawnedFloorParts.Add(spawnFloorPartInstance);

        spawnFloorPartInstance.GetComponent<MovablePartObject>().SetRandomSprite(movablePartType);
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
            spawnedFloorPartInstance.Translate(-transform.right * (_floorMoveSpeed * Time.deltaTime));

            if (spawnedFloorPartInstance.position.x <= Camera.main.transform.position.x - _floorPartLength)
            {
                _spawnedFloorParts.Remove(spawnedFloorPartInstance);
                Destroy(spawnedFloorPartInstance.gameObject);
                SpawnPartInstance(MovableParts.Floor);
            }
        }
    }

    // For the difficulty increasing manager
    public float GetFloorMoveSpeed() => _floorMoveSpeed;
    public float GetBackgroundMoveSpeed() => _backgroundMoveSpeed;
    public float GetForegroundMoveSpeed() => _foregroundMoveSpeed;

    public void SetFloorMoveSpeed(float newSpeed) => _floorMoveSpeed = newSpeed;
    public void SetBackgroundMoveSpeed(float newSpeed) => _backgroundMoveSpeed = newSpeed;
    public void SetForegroundMoveSpeed(float newSpeed) => _foregroundMoveSpeed = newSpeed;
}
