using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AviBlog.Core.Entities;

namespace AviBlog.Core.ViewModel
{
    public class BlogSiteViewModel : BaseViewModel
    {
        public int BlogId { get; set; }

        [Display(Name = "Blog Name")]
        [Required]
        public string BlogName { get; set; }

        [Display(Name = "Sub-heading")]
        public string SubHead { get; set; }

        [Display(Name = "Host Name")]
        public string HostName { get; set; }

        [Display(Name = "Is Primary Blog")]
        public bool IsPrimary { get; set; }

        [Display(Name = "Is blog Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Html Fragments")]
        public virtual IList<HtmlFragment> HtmlFragments { get; set; }


    }
}