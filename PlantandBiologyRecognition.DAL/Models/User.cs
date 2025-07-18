﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PlantandBiologyRecognition.DAL.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public string Avatar { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Recognitionhistory> Recognitionhistories { get; set; } = new List<Recognitionhistory>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Savedsample> Savedsamples { get; set; } = new List<Savedsample>();

    public virtual ICollection<Userrole> Userroles { get; set; } = new List<Userrole>();
}