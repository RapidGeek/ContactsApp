using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactBLL
{
    public class Contact : IEqualityComparer<Contact>, IEquatable<Contact>
    {
        [Key]
        public int ContactID { get; set; }

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public static bool operator ==(Contact x, Contact y) => x.Equals(y);

        public static bool operator !=(Contact x, Contact y) => !(x.Equals(y));

        public bool Equals(Contact x, Contact y)
        {
            // if both null or either of them are null
            var xIsNull = Object.ReferenceEquals(x, null);
            var yIsNull = Object.ReferenceEquals(y, null);
            if (xIsNull && yIsNull) return true;
            if (xIsNull || yIsNull) return false;

            return x.FirstName == y.FirstName &&
                x.LastName == y.LastName &&
                x.Address == y.Address &&
                x.PhoneNumber.Trim() == y.PhoneNumber.Trim() &&
                x.Birthday == y.Birthday;
        }

        public bool Equals(Contact other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(Contact obj)
        {
            unchecked
            {
                int hash = 17;
                if (Object.ReferenceEquals(obj, null))
                {
                    hash = hash * 23 + obj.FirstName.GetHashCode();
                    hash = hash * 23 + obj.LastName.GetHashCode();
                    hash = hash * 23 + obj.Address.GetHashCode();
                    hash = hash * 23 + obj.PhoneNumber.GetHashCode();
                    hash = hash * 23 + obj.Birthday.GetHashCode();
                }
                else
                {
                    hash = 0;
                }
                return hash;
            }
        }
    }
}
