namespace Example_ElifIsik.Dtos
{
    public class MaterialModel
    {
   
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string MainUom { get; set; }
        public int AdR_Status { get; set; }
        public string ProductBarcode { get; set; }
        public string ProductBarcodeCS { get; set; }
        public int UnitWeight { get; set; }
        public int Width { get; set; }
        public int Length_ { get; set; }
        public int Height { get; set; }
        public string BrandCode { get; set; }
        public List<Unit2Model> Unit2 { get; set; }
    }
}
