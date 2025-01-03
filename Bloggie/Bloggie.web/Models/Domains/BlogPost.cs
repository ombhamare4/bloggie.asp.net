﻿namespace Bloggie.web.Models.Domains
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public String Heading { get; set; }
        public String PageTitle { get; set; }
        public String Content { get; set; }
        public String ShortDescription {  get; set; }
        public String FeaturedImageUrl { get; set; }  
        public String URLHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public String Author {  get; set; }
        public bool Visible { get; set; }

        //Navigation Property
        public ICollection<Tag> Tags { get; set; }
    }
}
