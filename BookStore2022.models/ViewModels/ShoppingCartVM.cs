using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2022.Models.ViewModels
{
	public class ShoppingCartVM
	{


	public	IEnumerable<ShoppingCart> ListCart { get; set; }
		public double CartTotal { get; set; }
	}
}
