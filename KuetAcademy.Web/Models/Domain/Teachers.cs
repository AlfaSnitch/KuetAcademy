﻿namespace KuetAcademy.Web.Models.Domain
{
    public class Teachers
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PreferredSubject { get; set; }

        public string ShortDescription { get; set; }

        public string? FacebookLink { get; set; }

        public string? InstagramLink { get; set; }

        public string? TwitterLink { get; set; }

        public string? ImageUrl { get; set; }
    }
}
