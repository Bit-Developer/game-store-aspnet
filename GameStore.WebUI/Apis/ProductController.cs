using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GameStore.Domain.Infrastructure;
using GameStore.Domain.Model;
using System.Net.Http;
using System.Net;
using Microsoft.AspNet.Identity;
using GameStore.WebUI.Models;
using GameStore.WebUI.Areas.Admin.Models;
using GameStore.WebUI.Areas.Admin.Models.DTO;

namespace GameStore.WebUI.Apis
{
    public class ProductController : BaseApiController
    {
        // GET api/<controller>
        public List<ProductDTO> Get([FromUri] CategoryViewModel value)
        {
            if (value.CategoryId == 0)
            {
                if (HttpContext.Current.Cache["ProductList"] != null)
                    return (List<ProductDTO>)HttpContext.Current.Cache["ProductList"];
            }
            else
            {
                if (HttpContext.Current.Cache["ProductList" + value.CategoryId] != null)
                    return (List<ProductDTO>)HttpContext.Current.Cache["ProductList" + value.CategoryId];
            }
                
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                if (value.CategoryId == 0)
                {
                    var query = from product in context.Products
                                join category in context.Categories
                                  on product.CategoryId equals category.CategoryId
                      select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };

                    List<ProductDTO> products = query.ToList();
                    HttpContext.Current.Cache["ProductList"] = products;
                    return products;
                }
                else
                {
                    var query = from product in context.Products
                                where product.CategoryId == value.CategoryId
                                join category in context.Categories
                                  on product.CategoryId equals category.CategoryId
                                select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                    List<ProductDTO> products = query.ToList();
                    HttpContext.Current.Cache["ProductList" + value.CategoryId] = products;
                    return products;                 
                }
            }
        }

        [Authorize(Roles = "Admin, Advanced")]
        [Route("api/Product/GetByUserId")]
        public List<ProductDTO> GetByUserId([FromUri] CategoryViewModel value)
        {
            String userid = User.Identity.GetUserId();
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                if (value.CategoryId == 0)
                {
                    var query = from product in context.Products
                               where product.UserId == userid
                                join category in context.Categories
                                  on product.CategoryId equals category.CategoryId
                              select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                    List<ProductDTO> products = query.ToList();
                    return products;
                }
                else
                {
                    var query = from product in context.Products
                               where product.CategoryId == value.CategoryId 
                                  && product.UserId == userid
                                join category in context.Categories
                                  on product.CategoryId equals category.CategoryId
                              select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                    List<ProductDTO> products = query.ToList();
                    return products;
                }
            }
        }

        // GET api/<controller>/5
        public ProductDTO Get(int id)
        {
            if (HttpContext.Current.Cache["Product" + id] != null)
                return (ProductDTO)HttpContext.Current.Cache["Product" + id];
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                Product product = context.Products.Find(id);
                if (product == null)
                {
                    return null;
                }
                else
                {
                    ProductDTO productDTO = new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };
                    HttpContext.Current.Cache["Product" + id] = productDTO;
                    return productDTO;
                }
            }

        }

        // GET: api/Product/GetCount/
        [Route("api/Product/GetCount")]
        public int GetCount()
        {
            if (HttpContext.Current.Cache["ProductList"] != null)
            {
                List<ProductDTO> list = (List<ProductDTO>)HttpContext.Current.Cache["ProductList"];
                return list.Count();
            }
            else
            {
                using (GameStoreDBContext context = new GameStoreDBContext())
                {
                    var query = from product in context.Products
                                join category in context.Categories
                                  on product.CategoryId equals category.CategoryId
                              select new ProductDTO { ProductId = product.ProductId, ProductName = product.ProductName, CategoryId = product.CategoryId, CategoryName = category.CategoryName, Price = product.Price, Image = product.Image, Condition = product.Condition, Discount = product.Discount, UserId = product.UserId };

                    List<ProductDTO> products = query.ToList();
                    HttpContext.Current.Cache["ProductList"] = products;
                    return products.Count();
                }
            }
        }

        [Route("api/Product/GetAutoComplete")]
        public List<String> GetAutoComplete([FromUri] string name)
        {
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                var query = from product in context.Products
                           where product.ProductName.ToLower().Contains(name.ToLower())
                          select product.ProductName;

                List<String> names = query.ToList();
                return names;
            }
        }

        [Authorize(Roles = "Admin, Advanced")]
        [Route("api/Product/Create")]
        public HttpResponseMessage Create([FromBody]ProductViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.Discount < 0 || value.Discount > 100)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Discount must between 0 ~ 100.");
                }
                using (GameStoreDBContext context = new GameStoreDBContext())
                {
                    bool exist = context.Products.Any(c => c.ProductName.Equals(value.ProductName, StringComparison.OrdinalIgnoreCase));
                    if (exist)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product [" + value.ProductName + "] is already existed, please try another name!");
                    }
                    //if (file != null)
                    //{
                    //    string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

                    //    // linq from Henrik Stenbæk
                    //    bool isimage = formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));

                    //    if (!isimage)
                    //    {
                    //        return Request.CreateResponse(HttpStatusCode.OK, "The image format is not valid, must be .jpg, .png, .gif, or .jpeg");
                    //    }
                    //}
                    Product newProduct = context.Products.Create();
                    newProduct.ProductName = value.ProductName;
                    newProduct.CategoryId = value.CategoryId;
                    newProduct.Price = value.Price;
                    newProduct.Image = value.Image;
                    newProduct.Condition = value.Condition;
                    newProduct.Discount = value.Discount;
                    newProduct.UserId = User.Identity.GetUserId();
                    //string root = System.Web.Hosting.HostingEnvironment.MapPath("~/images/");
                    //string filename = string.Format(@"{0}.{1}", DateTime.Now.Ticks, System.IO.Path.GetExtension(file.FileName));
                    //file.SaveAs(System.IO.Path.Combine(root, filename));
                    //newProduct.Image = filename;
                    context.Products.Add(newProduct);
                    context.SaveChanges();
                    HttpContext.Current.Cache.Remove("ProductList");
                    HttpContext.Current.Cache.Remove("ProductList" + newProduct.CategoryId);
                    return Request.CreateResponse(HttpStatusCode.OK, "Okay");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ModelState.IsValid=false");
            }
        }

        [Authorize(Roles = "Admin, Advanced")]
        public HttpResponseMessage Post([FromBody]ProductViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value == null || String.IsNullOrEmpty(value.ProductName))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Product Name can't be empty!");
                }

                if (value.Discount < 0 || value.Discount > 100)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Discount must between 0 ~ 100.");
                }

                using (GameStoreDBContext context = new GameStoreDBContext())
                {
                    bool exist = context.Products.Any(c => c.ProductId == value.ProductId);
                    if (!exist)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product [" + value.ProductId + "] does not exist!");
                    }

                    exist = context.Products.Where(c => c.ProductId != value.ProductId).Any(c => c.ProductName.Equals(value.ProductName, StringComparison.OrdinalIgnoreCase));
                    if (exist)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Product [" + value.ProductName + "] is already existed, please try another name!");
                    }
                    var product = context.Products.Find(value.ProductId);
                    if (product == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "No such product!");
                    }

                    bool isAdvanced = HttpContext.Current.User.IsInRole("Advanced");
                    if (isAdvanced && product.UserId != HttpContext.Current.User.Identity.GetUserId())
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "You have no authorization to update this product!");
                    }

                    HttpContext.Current.Cache.Remove("ProductList" + product.CategoryId);
                    product.ProductName = value.ProductName;
                    product.CategoryId = value.CategoryId;
                    product.Price = value.Price;
                    product.Image = value.Image;
                    product.Condition = value.Condition;
                    product.Discount = value.Discount;
                    context.SaveChanges();
                    //context.Entry(product).CurrentValues.SetValues(value);
                    //context.SaveChanges();
                    HttpContext.Current.Cache.Remove("ProductList");
                    HttpContext.Current.Cache.Remove("ProductList" + value.CategoryId);
                    HttpContext.Current.Cache.Remove("Product" + product.ProductId);
                    return Request.CreateResponse(HttpStatusCode.OK, "Okay");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ModelState.IsValid=false");
            }
        }

        //PUT REMOVED

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Advanced")]
        public HttpResponseMessage Delete(int id)
        {
            using (GameStoreDBContext context = new GameStoreDBContext())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No such product!");
                }
                bool isAdvanced = HttpContext.Current.User.IsInRole("Advanced");
                if (isAdvanced && product.UserId != HttpContext.Current.User.Identity.GetUserId())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "You have no authorization to delete this product!");
                }
                context.Products.Remove(product);
                context.SaveChanges();
                HttpContext.Current.Cache.Remove("ProductList");
                HttpContext.Current.Cache.Remove("ProductList" + product.CategoryId);
                HttpContext.Current.Cache.Remove("Product" + id);
                return Request.CreateResponse(HttpStatusCode.OK, "Okay");
            }
        }
    }
}