public class Speed
{
    private float _speedMultilplier = 1f;

    public float DefaultTimeToTravelOneCell { get; private set; } = 0.5f;
    public float CellsPerSecond => DefaultTimeToTravelOneCell * _speedMultilplier;

    public Speed(float defaultTimeToTravelOneCell)
    {
        DefaultTimeToTravelOneCell = defaultTimeToTravelOneCell;
    }

}
