﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ContactAPI.Core.Entities
{
    public partial class Email
    {
        public long Id { get; set; }
        public long? ContactId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public byte? RowStatusId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}