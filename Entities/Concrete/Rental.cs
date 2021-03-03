using Core.Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Rental:IEntity
    {
        // Rentals-->Id, CarId, CustomerId, RentDate(Kiralama Tarihi), ReturnDate(Teslim Tarihi)
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ? ReturnDate { get; set; }
    }
}
