namespace BetterSubnautica.Components
{
    public class PowerRelaySource : AbstractEnergySource<PowerRelay>
    {
        public override bool HasEnergy()
        {
            if (Component != null)
            {
                return Component.GetPower() > 0f;
            }

            return false;
        }
        public override void ConsumeEnergy(float amount)
        {
            if (Component != null)
            {
                Component.ConsumeEnergy(amount, out var _);
            }
        }
    }
}
