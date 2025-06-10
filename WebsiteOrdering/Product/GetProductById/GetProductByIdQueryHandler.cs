using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebsiteOrdering.Models;
using WebsiteOrdering.Product.GetAllProducts;
using WebsiteOrdering.ViewModels;

namespace WebsiteOrdering.Product.GetProductById
{
    public class GetProductByIdQueryHandler :IRequestHandler<GetProductsByIdQuery, ProductsViewModel>
    {
        private readonly AppDbContext _appDbContext;
        public GetProductByIdQueryHandler (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ProductsViewModel?> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.SanPhams
                .Include(p => p.Category)
                .Where(p => p.IDMONAN == request.Id)
                .Select(p => new ProductsViewModel
                {
                IDMONAN = p.IDMONAN,
                IDMONAN2 = p.IDMONAN2,
                TENMONAN = p.TENMONAN,
                MOTAMONAN = p.MOTAMONAN,
                GIACOBAN = p.GIACOBAN,
                TRANGTHAI = p.TRANGTHAI,
                ANHMONAN = p.ANHMONAN,
                
                SoLuong = 1,
                IDLoaiMonAn = p.IDLoaiMonAn,
                    //Hiển thị size theo loại món ăn
                    ListGiaSizes = _appDbContext.ListGiaSizes
                 .Where(g => g.IDLOAIMONAN == p.IDLoaiMonAn)
                 .Include(g => g.Size)
                 .Select(g=> new ListGiaSizeViewModel
                 {
                        IDSIZE = g.IDSIZE,
                        GIA = g.GIA,
                        Size = new SizeViewModel
                        {
                            IDSIZE = g.Size.IDSIZE,
                            TENSIZE = g.Size.TENSIZE
                        }
                    }).ToList(),

                  //Hiển thị đế bánh
                     DeBanh = _appDbContext.debanh
                    .Select(d => new DeBanhViewModel
                        {
                            IDDEBANH = d.IDDEBANH,
                            TENDEBANH = d.TENDEBANH,
                            GIADEBANH = d.GIADEBANH
                        }).ToList(),
                     

                     //Hiển thị topping theo loại món ăn
                     Toppings = _appDbContext.Topping
                     .Where(t=> t.IDLOAIMONAN == p.IDLoaiMonAn)
                     .Select(t=> new ToppingViewModel
                     {
                         IDTOPPING = t.IDTOPPING,
                         TENTOPPING = t.TENTOPPING,
                         GIATOPPING= t.GIATOPPING
                     }).ToList(),
                })
                .FirstOrDefaultAsync(cancellationToken);

            //Hàm để lấy được pizza ghép
            if (product != null && product.IDLoaiMonAn == "LMA01" && product.IDMONAN2?.Trim() == "1")
            {
                Console.WriteLine("Product ID: " + product.IDMONAN);

                var pizzaGhepList = (await _appDbContext.SanPhams
                    .Where(sp => sp.IDMONAN == product.IDMONAN && sp.IDMONAN2 != "1")
                    .Join(
                        _appDbContext.SanPhams,
                        sp => sp.IDMONAN2,
                        original => original.IDMONAN,
                        (sp, original) => new SanPhamViewModel
                        {
                            IDMONAN = sp.IDMONAN,
                            IDMONAN2 = sp.IDMONAN2,
                            GIACOBAN = sp.GIACOBAN,
                            TENMONAN = original.TENMONAN,
                            ANHMONAN = original.ANHMONAN
                        }
                    )
                    .ToListAsync(cancellationToken))  // Đưa dữ liệu về client để xử lý tiếp
                    .DistinctBy(p => p.IDMONAN2)       // Loại trùng theo IDMONAN
                    .ToList();

                product.PizzaGhep = pizzaGhepList;
            }

       

            return product;
        }
    }
}
