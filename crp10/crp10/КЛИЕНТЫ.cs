//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace crp10
{
    using System;
    using System.Collections.Generic;
    
    public partial class КЛИЕНТЫ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public КЛИЕНТЫ()
        {
            this.АРЕНДА = new HashSet<АРЕНДА>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ID_user { get; set; }
        public string ФИО { get; set; }
        public string Паспорт_номер { get; set; }
        public string Права_номер { get; set; }
        public string Телефон { get; set; }
        public string Email { get; set; }
        public byte[] Фото { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<АРЕНДА> АРЕНДА { get; set; }
        public virtual ПОЛЬЗОВАТЕЛИ ПОЛЬЗОВАТЕЛИ { get; set; }
    }
}
