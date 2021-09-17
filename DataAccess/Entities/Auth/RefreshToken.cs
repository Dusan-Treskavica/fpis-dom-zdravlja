using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Auth
{
    public class RefreshToken
    {
        public int Id { get; set; }
        // Value of the Token
        public string Value { get; set; }
        // Get the Token Creation Date
        public DateTime CreatedDate { get; set; }
        // The UserId it was issued to
        public string UserId { get; set; }
        public DateTime ExpiryTime { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
