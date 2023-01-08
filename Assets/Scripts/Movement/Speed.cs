public class Speed
{
    private float _speedMultilplier = 1f;

    public float DefaultValue { get; private set; }
    public float Value => DefaultValue * _speedMultilplier;

    public Speed(float defaultTimeToTravelOneCell)
    {
        DefaultValue = defaultTimeToTravelOneCell;
    }

}
