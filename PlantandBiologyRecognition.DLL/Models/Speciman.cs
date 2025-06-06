﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PlantandBiologyRecognition.DAL.Models;

public partial class Speciman
{
    public Guid Specimenid { get; set; }

    public string Scientificname { get; set; }

    public string Commonname { get; set; }

    public string Description { get; set; }

    public string Imageuri { get; set; }

    public Guid? Createby { get; set; }

    public Guid? Inmaterial { get; set; }

    public virtual Account CreatebyNavigation { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Educationmaterial InmaterialNavigation { get; set; }
}