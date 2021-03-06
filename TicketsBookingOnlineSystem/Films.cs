//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketsBookingOnlineSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Films
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Films()
        {
            this.Seances = new HashSet<Seances>();
        }
    
        public int Id { get; set; }
        public System.TimeSpan Duration { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<int> Creator_Id { get; set; }
        public Nullable<int> FilmGenre_Id { get; set; }
    
        public virtual Creators Creators { get; set; }
        public virtual FilmGenres FilmGenres { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seances> Seances { get; set; }
    }
}
