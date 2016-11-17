using System.Collections.Generic;
using System.Linq;
using SportsStore.Models.Domain;

namespace SportsStore.Tests.Data
{
    public class DummyApplicationDbContext
    {
        private readonly City _gent;
        private readonly City _antwerpen;

        private readonly Category _watersports;
        private readonly Category _soccer;
        private readonly Category _chess;
        private readonly IList<Product> _products;
        
        public DummyApplicationDbContext()
        {
            _products=new List<Product>();
            _gent = new City { Name = "Gent", Postalcode = "9000" };
            _antwerpen = new City { Name = "Antwerpen", Postalcode = "3000" };
           _watersports = new Category( "WaterSports");
            _soccer = new Category("Soccer" );
            _chess = new Category( "Chess");
            _soccer.AddProduct("Football", 25, "WK colors");
            Football.ProductId = 1;
            _soccer.AddProduct("Corner flags", 34, "Give your playing field that professional touch");
            _soccer.AddProduct("Stadium", 79500, "Flat-packed 35000-seat stadium");
            _soccer.AddProduct("Running shoes", 95, "Protective and fashionable");
            RunningShoes.ProductId = 4;
            _watersports.AddProduct("Surf board", 275, "A boat for one person");
            _watersports.AddProduct("Kayak", 170, "High quality");
            _watersports.FindProduct("Kayak").Availability = Availability.ShopOnly;
            _watersports.AddProduct("Lifejacket", 49, "Protective and fashionable");
            _chess.AddProduct("Thinking cap", 16, "Improve your brain efficiency by 75%");
            _chess.AddProduct("Unsteady chair", 30, "Secretly give your opponent a disadvantage");
            _chess.AddProduct("Human chess board", 75, "A fun game for the whole extended family!");
            _chess.AddProduct("Bling-bling King", 1200, "Gold plated, diamond-studded king");
            foreach (Category c in Categories)
                foreach (Product p in c.Products)
                    _products.Add(p);
        }

        public IEnumerable<Category> Categories => new List<Category>
        {
            _watersports,
            _soccer,
            _chess
        };

        public IEnumerable<City> Cities => new List<City> { _gent, _antwerpen };

        public Customer Customer => new Customer("jan", "Janneman",  "Jan",  "Nieuwstraat 100",  _gent );

        public IEnumerable<Product> Products => _products;

        public IEnumerable<Product> ProductsOnline => _products.Where(p=>p.Availability==Availability.OnlineOnly || p.Availability==Availability.ShopAndOnline);

        public Product Football => _soccer.FindProduct("Football");

        public Product RunningShoes => _soccer.FindProduct("Running shoes");
    }
}
