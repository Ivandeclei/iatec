using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class PaginateFilterDto
    {
        /// <summary>
        /// Number of the page of the pagination
        /// </summary>
       
        [Range(0, int.MaxValue, ErrorMessage = "PageNumber field must be a positive integer")]
        public int? PageNumber { get; set; }

        /// <summary>
        /// Record number returned in the query
        /// </summary>
        
        [Range(0, int.MaxValue, ErrorMessage = "PageSize field must be a positive integer")]
        public int? PageSize { get; set; }
    }
}
