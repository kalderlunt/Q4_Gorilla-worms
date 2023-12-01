using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private int _minStoneHeight, _maxStoneHeight;
    [SerializeField] private GameObject _dirt, _grass, _stone;

    void Start()
    {
        Generation();
    }

    private void Generation()
    {
        for (int x = 0; x < _width; ++x) // x axis
        {
            int minHeight = _height - 1;
            int maxHeight = _height + 2;
            _height = Random.Range(minHeight, maxHeight);

            int minStoneSpawnDistance = _height - _minStoneHeight;
            int maxStoneSpawnDistance = _height - _maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);

            // Perlin noise
            for (int y = 0; y < _height; ++y) // y axis
            {
                if (y < totalStoneSpawnDistance)
                {
                    SpawnObject(_stone, x, y);
                }
                else
                {

                    SpawnObject(_dirt, x, y);
                }
            }

            if (totalStoneSpawnDistance == _height)
            {
                SpawnObject(_stone, x, _height);
            }
            else
            {
                SpawnObject(_grass, x, _height);
            }
        }
    }

    private void SpawnObject(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent= this.transform;
    }
}
