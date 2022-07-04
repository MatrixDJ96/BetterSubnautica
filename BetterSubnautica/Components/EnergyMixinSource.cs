namespace BetterSubnautica.Components
{
    public class EnergyMixinSource : AbstractEnergySource<EnergyMixin>
    {
        public override bool HasEnergy()
        {
            if (Component != null)
            {
                return Component.charge > 0f;
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
