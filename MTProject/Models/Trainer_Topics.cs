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
    
    public partial class Trainer_Topics
    {
        public int Id { get; set; }
        public Nullable<int> TopicsId { get; set; }
        public Nullable<int> TrainerID { get; set; }
    
        public virtual Topic Topic { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
