namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name; // Dit zorgt dat de ComboBox correct weergeeft
        }
    }
}
