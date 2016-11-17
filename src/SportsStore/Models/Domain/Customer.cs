using System;
using System.Collections.Generic;

namespace SportsStore.Models.Domain
{
    public class Customer
    {
        #region Properties

        public int CustomerId { get; private set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public City City { get; set; }
        public ICollection<Order> Orders { get; set; }
        #endregion

        protected Customer()
        {
          Orders = new List<Order>();
        }

        public Customer(string customerName, string name, string firstName, string street, City city) : this()
        {
            CustomerName = customerName;
            Name = name;
            FirstName = firstName;
            Street = street;
            City = city;
        }

        public Customer(int customerId, string customerName, string name, string firstName, string street, City city) : this(customerName, name, firstName, street, city)
        {
            CustomerId = customerId;
        }

        #region Methods
        public void PlaceOrder(Cart cart, DateTime? deliveryDate, bool giftwrapping, string shippingStreet, City shippingCity)
        {
            Orders.Add(new Order(cart, deliveryDate, giftwrapping, shippingStreet, shippingCity));
        }
        #endregion
    }
}