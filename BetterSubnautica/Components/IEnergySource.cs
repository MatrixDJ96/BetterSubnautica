namespace BetterSubnautica.Components
{
    public interface IEnergySource
    {
        bool HasEnergy();

        void ConsumeEnergy(float amount);
    }
}