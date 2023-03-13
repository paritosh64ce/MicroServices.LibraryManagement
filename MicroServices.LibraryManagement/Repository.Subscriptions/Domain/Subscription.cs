using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Subscriptions.Domain
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; internal set; }

        [Required]
        [Column("SUBSCRIBER_NAME")]
        [MaxLength(500)]
        public string SubscriberName { get; set; }

        [Required]
        [Column("DATE_SUBSCRIBED")]
        public DateTime DateSubscribed { get; set; }

        [Column("DATE_RETURNED")]
        public DateTime? DateReturned { get; set; }

        [Required]
        [Column("BOOK_ID")]
        public int BookId { get; set; }

        public Subscription()
        {

        }
        internal Subscription(string subscriberName, int bookId) 
        {
            SubscriberName = subscriberName;
            BookId = bookId;
            DateSubscribed = DateTime.UtcNow;
        }

        public void EndSubscription()
        {
            DateReturned = DateTime.UtcNow;
        }
    }
}
