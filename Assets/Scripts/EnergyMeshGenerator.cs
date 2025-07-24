using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnergyMeshGenerator : MonoBehaviour
{
    public TextAsset jsonFile; // назначь файл вручную или загрузи через Resources
    public float heightMultiplier = 0.01f;
    public float widthSpacing = 1f;

    void Start()
    {
        if (jsonFile == null)
        {
            Debug.LogError("JSON-файл не назначен!");
            return;
        }

        string wrappedJson = "{\"data\":" + jsonFile.text + "}";
        EnergyDataPointList dataList = JsonUtility.FromJson<EnergyDataPointList>(wrappedJson);

        GenerateMesh(dataList.data);
    }

    void GenerateMesh(EnergyDataPoint[] data)
    {
        Vector3[] vertices = new Vector3[data.Length * 2];
        int[] triangles = new int[(data.Length - 1) * 6];

        float min = float.MaxValue;
        float max = float.MinValue;
        foreach (var point in data)
        {
            if (point.PJME_MW < min) min = point.PJME_MW;
            if (point.PJME_MW > max) max = point.PJME_MW;
        }

        for (int i = 0; i < data.Length; i++)
        {
            float x = i * widthSpacing;
            float y = (data[i].PJME_MW - min) / (max - min) * heightMultiplier * 100f;

            vertices[i * 2] = new Vector3(x, 0, 0);     // нижняя точка
            vertices[i * 2 + 1] = new Vector3(x, y, 0); // верхняя точка
        }

        int triIndex = 0;
        for (int i = 0; i < data.Length - 1; i++)
        {
            int v = i * 2;
            // первая треугольная грань
            triangles[triIndex++] = v;
            triangles[triIndex++] = v + 1;
            triangles[triIndex++] = v + 2;

            // вторая треугольная грань
            triangles[triIndex++] = v + 1;
            triangles[triIndex++] = v + 3;
            triangles[triIndex++] = v + 2;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
