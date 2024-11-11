
using Meny_To_Meny_Relationship_in_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meny_To_Meny_Relationship_in_MVC.ViewModel
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; } 

        public List<Tag>? Tags { get; set; }

        [Required]
        public string Description { get; set; }

        public List<int>? SelectedTagsIds { get; set; }
    }
}
