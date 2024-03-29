﻿using UI.Areas.Admin.Models.Base;

namespace UI.Areas.Admin.Features.Pages.Create
{
    public class CreatePageViewModel : PageViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool? IsActive { get; set; }
    }
}
