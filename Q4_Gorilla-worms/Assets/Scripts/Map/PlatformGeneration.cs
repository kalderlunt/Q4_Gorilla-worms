using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private int _width;
    [SerializeField] private int _minHeight, _maxHeight;
    
    [Header("Width platform to move")]
    [SerializeField] private int _repeatNum;
    
    [Header("platform height to be avoided")]
    [SerializeField] private int _spikeSpawnHeight;

    [Header("GameObject to Instantiate")]
    [SerializeField] private GameObject _dirt;
    [SerializeField] private GameObject _grass;
    [SerializeField] private GameObject _spike;

    private int _height;

    void Start()
    {
        Generation();
    }

    private void Generation()
    {
        int repeatValue = 0;
        for (int x = 0; x < _width; ++x) // x axis
        {
            if (repeatValue == 0)
            {
                _height = Random.Range(_minHeight, _maxHeight);
                GenerateFlatPlatform(x);
                repeatValue = _repeatNum;
            }
            else
            {
                GenerateFlatPlatform(x);
                repeatValue--;
            }
        }
    }

    private void GenerateFlatPlatform(int x)
    {
        for (int y = 0; y < _height; ++y) // y axis
        {
            SpawnObject(_dirt, x, y);
        }

        if (_height < _spikeSpawnHeight)
        {
            SpawnObject(_grass, x, _height);
            SpawnObject(_spike, x, _height + 1);
        }
        else
        {
            SpawnObject(_grass, x, _height);
        }
    }

    private void SpawnObject(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
