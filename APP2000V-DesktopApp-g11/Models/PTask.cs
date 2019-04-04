//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APP2000V_DesktopApp_g11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PTask()
        {
            this.AssignedTasks = new HashSet<AssignedTask>();
        }
    
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public Nullable<System.DateTime> TaskCreationDate { get; set; }
        public Nullable<System.DateTime> TaskDeadline { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<int> TaskProjectId { get; set; }
        public Nullable<int> TaskListId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignedTask> AssignedTasks { get; set; }
        public virtual Project Project { get; set; }
        public virtual TaskList TaskList { get; set; }
    }
}