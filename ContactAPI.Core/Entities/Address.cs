// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using ContactAPI.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace ContactAPI.Core.Entities
{
    public partial class Address: BaseEntity
    {
        public long Id { get; set; }
        public long? ContactId { get; set; }
        public string Href { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string StateOrProvince { get; set; }
        public string StreetPrefix { get; set; }
        public string StreetSuffix { get; set; }
        public string Type { get; set; }
        public byte? RowStatusId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}