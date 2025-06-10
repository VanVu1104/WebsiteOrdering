namespace WebsiteOrdering.ViewModels
{
    public class CartItem
    {
        public string IDMONAN {  get; set; }
        public string IDMONAN2 { get; set; }
        public string TENSANPHAM { get; set; }
        public string ANHSANPHAM { get; set; }
        public string Size {  get; set; }
        public string DeBanh { get; set; }
        public string GhiChu { get; set; }
        public int SoLuong { get; set; }
        public int GiaCoBan {  get; set; }
        public int GiaSize { get; set; }
        public int GiaDeBanh { get;set; }
        public List<ToppingViewModel> Topping { get; set; } = new List<ToppingViewModel>();

        public int TongTien
        {
            get
            {
                int tongTopping = Topping.Sum(t=>t.GIATOPPING);
                return(GiaCoBan +GiaSize + GiaDeBanh + tongTopping) * SoLuong;
            }
        }
        public List<SanPhamViewModel> SanPham { get; set; }
    }
}
