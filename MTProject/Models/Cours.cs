//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            this.Trainee_Course = new HashSet<Trainee_Course>();
        }
    
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public System.DateTime CreateAt { get; set; }
    
        public virtual CourseCategory CourseCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trainee_Course> Trainee_Course { get; set; }
    }
}
