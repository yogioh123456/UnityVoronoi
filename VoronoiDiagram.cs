using UnityEngine;
using UnityEngine.UI;

public class VoronoiDiagram : MonoBehaviour
{
    public int gridSize = 10;
    private int imgSize;
    private RawImage image;
    private int pixelsPerCell;
    private Vector2Int[,] pointsPositions;
    private Color[,] colors;

    void Start()
    {
        image = GetComponent<RawImage>();
        imgSize = Mathf.RoundToInt(image.GetComponent<RectTransform>().sizeDelta.x);
        GnerateDiagram();
    }

    void GnerateDiagram()
    {
        Texture2D texture = new Texture2D(imgSize, imgSize);
        texture.filterMode = FilterMode.Point;
        pixelsPerCell = imgSize / gridSize;
        GeneratePoints();

        for (int i = 0; i < imgSize; i++)
        {
            for (int j = 0; j < imgSize; j++)
            {
                int gridX = i / pixelsPerCell;
                int gridY = j / pixelsPerCell;

                float nearestDistance = Mathf.Infinity;
                Vector2Int nearestPoint = new Vector2Int();

                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                        int X = gridX + a;
                        int Y = gridY + b;
                        if (X < 0 || Y < 0 || X >= gridSize || Y >= gridSize)
                        {
                            continue;
                        }

                        float distance = Vector2Int.Distance(new Vector2Int(i, j), pointsPositions[X, Y]);
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestPoint = new Vector2Int(X, Y);
                        }
                    }
                }

                texture.SetPixel(i, j, colors[nearestPoint.x, nearestPoint.y]);
            }
        }

        texture.Apply();
        image.texture = texture;
    }

    private void GeneratePoints()
    {
        pointsPositions = new Vector2Int[gridSize, gridSize];
        colors = new Color[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                pointsPositions[i, j] = new Vector2Int(i * pixelsPerCell + Random.Range(0, pixelsPerCell),
                    j * pixelsPerCell + Random.Range(0, pixelsPerCell));

                float r = Random.Range(0, 1f);
                float g = Random.Range(0, 1f);
                float b = Random.Range(0, 1f);
                Color c = new Color(r, g, b, 1);
                colors[i, j] = c;
            }
        }
    }
}
