namespace BetterSubnautica.Components
{
    public class EnergyInterfaceSource : AbstractEnergySource<EnergyInterface>
    {
        public override bool HasEnergy()
        {
            if (Component != null)
            {
                return Component.hasCharge;
            }

            return false;
        }

        public override void ConsumeEnergy(float amount)
        {
            if (Component != null)
            {
                Component.ConsumeEnergy(amount);
            }
        }
    }
}
