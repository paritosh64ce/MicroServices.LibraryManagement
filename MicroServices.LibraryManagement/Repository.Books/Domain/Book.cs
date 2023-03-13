using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Books.Domain
{
    public class Book
    {
        [Key]
        [Required]
        [Column("BOOK_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("BOOK_NAME")]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [Column("AUTHOR")]
        [MaxLength(500)]
        public string Author { get; set; }

        [Required]
        [Column("AVAILABLE_COPIES")]
        public int AvailableCopies { get; set; }

        [Required]
        [Column("TOTAL_COPIES")]
        public int TotalCopies { get; set; }

        public Book() { }

        public void Subscribe()
        {
            if (AvailableCopies == 0)
            {
                throw new Exception("No copied left to subscribe");
            }
            AvailableCopies--;
        }
        public void Unsubscribe()
        {
            if (AvailableCopies > TotalCopies)
            {
                throw new Exception("Trying to unsubscribe a book which is not subscribed");
            }
            AvailableCopies++;
        }
    }
}
