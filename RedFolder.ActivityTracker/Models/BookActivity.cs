using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedFolder.ActivityTracker.Models
{
    public class BookActivity
    {
        [JsonProperty("books")]
        public List<Book> Books;
    }
}
