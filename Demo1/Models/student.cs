//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class student
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> Gendor { get; set; }
        public string Email { get; set; }
        public string CourseId { get; set; }
    
        public virtual Course Cours { get; set; }
    }
}