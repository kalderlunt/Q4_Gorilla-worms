using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private int _startX;
    [SerializeField] private int _startY;
    [SerializeField] private int _width;
    private int _height;
    [SerializeField] private int _minHeight, _maxHeight;
    
    [Header("Width platform to move")]
    [SerializeField] int _maxVariation;
    [SerializeField] private int _repeatNum;
    
    [Header("platform height to be avoided")]
    [SerializeField] private int _spikeSpawnHeight;

    [Header("GameObject to Instantiate")]
    [SerializeField] private GameObject _dirt;
    [SerializeField] private GameObject _grass;
    [SerializeField] private GameObject _spike;


    void Start()
    {
        PreGeneration();
        Generation();
        PostGeneration();
    }
    void PreGeneration()
    {
        int x = _startX - 16;
        _height = _maxHeight + 30;
        while (x < _startX)
        {
            GenerateFlatPlatform(x);
            x++;
        }
    }

    private void Generation()
    {
        int repeatValue = 0;
        _height = Random.Range(_minHeight, _maxHeight);
        for (int x = _startX; x < _width; ++x) // x axis
        {
            if (repeatValue == 0)
            {
                _height = Random.Range(Mathf.Clamp(_height - _maxVariation, _maxHeight, _minHeight), Mathf.Clamp(_height + _maxVariation, _maxHeight, _minHeight));
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
    void PostGeneration()
    {
        int x = _width;
        _height = _maxHeight + 30;
        while (x < _width + 16)
        {
            GenerateFlatPlatform(x);
            x++;
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
