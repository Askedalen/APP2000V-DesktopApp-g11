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
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Notifications = new HashSet<Notification>();
        }
    
        public int EventId { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public int ProjectId { get; set; }
        public int CreatorId { get; set; }
        public string Type { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> TaskId { get; set; }
        public Nullable<int> TaskListId { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual PTask PTask { get; set; }
        public virtual TaskList TaskList { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
