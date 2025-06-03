namespace PRA_B4_FOTOKIOSK.models
{
    public class OrderedProduct
    {
        public string FotoId { get; set; }
        public string ProductNaam { get; set; }
        public int Aantal { get; set; }
        public decimal TotaalPrijs { get; set; }

        public override string ToString()
        {
            return $"Foto: {FotoId} - {ProductNaam} ({Aantal}x) = €{TotaalPrijs:0.00}";
        }
    }
}