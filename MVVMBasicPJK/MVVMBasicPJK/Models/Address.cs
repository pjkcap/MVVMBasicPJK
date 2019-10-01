using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMBasicPJK.Models
{
    /// <summary>
    /// 주소 정보 Class
    /// </summary>
    public class Address
    {
        /// <summary>
        /// get or set Street
        /// </summary>
        public Street Street { get; set; }
        /// <summary>
        /// get or set City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// get or set State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// get or set Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// get or set Post Code
        /// </summary>
        public string PostCode { get; set; }
            
        /// <summary>
        /// get or set Coordinates
        /// </summary>
        public Coordinate Coordinates { get; set; }

        /// <summary>
        /// get or set Timezone
        /// </summary>
        public Timezone Timezone { get; set; }
    }

    public class Street
    {
        /// <summary>
        /// get or set Number
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// get or set Name
        /// </summary>
        public string Name { get; set; }
    }

    public class Coordinate
    {
        /// <summary>
        /// get or set Latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// get or set Longitude
        /// </summary>
        public string Longitude { get; set; }
    }

    public class Timezone
    {
        /// <summary>
        /// get or set Offset
        /// </summary>
        public string Offset { get; set; }
        /// <summary>
        /// get or set Description
        /// </summary>
        public string Description { get; set; }
    }
}
