using UnityEngine;

public class EnergyMeshRaycaster : MonoBehaviour
{
    public Tooltip tooltip;
    public Camera cam;
    public Transform meshObject;

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == meshObject)
            {
                Vector3 closest = hit.point;

                // Можно округлить или интерполировать
                string info = $"Энергия: {closest.y:F2}";
                tooltip.Show(info, hit.point);
            }
        }
        else
        {
            tooltip.Hide();
        }
    }
}
