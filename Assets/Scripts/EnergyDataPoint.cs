[System.Serializable]
public class EnergyDataPoint
{
    public string Date;
    public float PJME_MW;
}

[System.Serializable]
public class EnergyDataPointList
{
    public EnergyDataPoint[] data;
}
