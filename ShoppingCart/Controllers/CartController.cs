using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Products")]
        //add to cart
        public string ViewProducts(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CartService").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Products'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int productID = product.ID;
            foreach (Product pr in dt.Rows)
            {
                return pr.ID + pr.Name + pr.Price + pr.ExpirationDate + pr.Description;
            }
            if (dt.Rows.Count == 0)
            {
                return "No products to show";
            }
            else
            {
                return "";
            }

        }
        [HttpPost]
        [Route("AddToCart")]
        //add to cart
        public string addtocart(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CartService").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Cart(ProductID) VALUES('" + product.ID + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Added Successfully";
            }
            else
            {
                return " Error";
            }

        }
        [HttpPost]
        [Route("DeleteCartProducts")]
        //delete from cart
        public string DeleteFromcart(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CartService").ToString());
            SqlCommand cmd = new SqlCommand("DELETE Cart where (ProductID) = ('" + product.ID + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Deleted Successfully";
            }
            else
            {
                return " Error";
            }

        }
        [HttpPost]
        [Route("ShowCartProducts")]
        public string SeeCartProducts(Product product)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CartService").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Cart'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            /*List<string> products = new List<string>();*/

            int productID = product.ID;
            foreach (Product pr in dt.Rows)
            {
                return pr.Name + pr.Price + pr.ExpirationDate + pr.Description;
            }
            if (dt.Rows.Count == 0)
            {
                return "The cart is empty";
            }
            else
            {
                return "";
            }



        }
    }
}
