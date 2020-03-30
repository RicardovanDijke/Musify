using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Model
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; private set; }

        public string Title { get; private set; }

        public Artist Artist { get; set; }

        public int Duration { get; set; }


    }
}
