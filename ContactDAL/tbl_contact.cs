namespace ContactDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_contact: IEqualityComparer<tbl_contact>,IEquatable<tbl_contact>
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

        public static bool operator ==(tbl_contact x, tbl_contact y) => x.Equals(y);

        public static bool operator !=(tbl_contact x, tbl_contact y) => !x.Equals(y);

        public bool Equals(tbl_contact x, tbl_contact y)
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

        public bool Equals(tbl_contact other)
        {
            return Equals(this, other);
        }

        public int GetHashCode(tbl_contact obj)
        {
            unchecked
            {
                int hash = 17;
                if (obj != null)
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
