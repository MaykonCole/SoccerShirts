using SoccerShirts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerShirts.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Camisa> camisasMaisVendidas { get; set; }
    }
}
