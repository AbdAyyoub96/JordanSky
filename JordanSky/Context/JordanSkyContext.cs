using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using JordanSky.Entity;

namespace JordanSky.Context
{
    public class JordanSkyContext : DbContext
    {
        public JordanSkyContext() : base("name = Sky")
        {

        }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Cate_Mazra3a> Cate_Mazra3s  { get; set; }
        public virtual DbSet<Category> Categories  { get; set; }
        public virtual DbSet<City> Cities  { get; set; }
        public virtual DbSet<Type_Product> Type_s  { get; set; }
        public virtual DbSet<Contact> Contacts  { get; set; }
        public virtual DbSet<Image> Images  { get; set; }
        public virtual DbSet<Mazr3a> Mazrs  { get; set; }
        public virtual DbSet<Message> Messages  { get; set; }
        public virtual DbSet<Register>  Registers { get; set; }
        public virtual DbSet<Type_user> Type_Users  { get; set; }
        public virtual DbSet<User> Users  { get; set; }
        public virtual DbSet<Internal_Package> Packages  { get; set; }
        public virtual DbSet<Booking_package> Booking_Packages { get; set; }
        public virtual DbSet<Starting_locations> Startings { get; set; }
        public virtual DbSet<Image_Package> Image_Packages { get; set; }
        public virtual DbSet<Type_packge> Type_Packges{ get; set; }
        public virtual DbSet<Hotel> Hotels  { get; set; }
        public virtual DbSet<Type_Hotel> Type_Hotels   { get; set; }
        public virtual DbSet<Image_Hotel> Image_Hotels { get; set; }
        public virtual DbSet<Booking_Hotel> Booking_Hotels { get; set; }
        public virtual DbSet<Cate_Hotel> Cate_Hotels { get; set; }
    }
}
