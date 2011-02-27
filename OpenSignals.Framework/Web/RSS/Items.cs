using System.Xml.Serialization;
using System.Collections.Generic;
using System;
namespace OpenSignals.Framework.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [XmlElement("title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>
        /// The link.
        /// </value>
        [XmlElement("link")]
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [XmlElement("description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the pub date.
        /// </summary>
        /// <value>
        /// The pub date.
        /// </value>
        [XmlElement("pubDate")]
        public string PubDate
        {
            get { return _pubDate; }
            set { _pubDate = value; }
        }

        private string _title;
        private string _link;
        private string _description;
        private string _pubDate;
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRootAttribute(ElementName="item")] 
    public class Item : Element
    {
        //this ones for serialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="link">The link.</param>
        /// <param name="description">The description.</param>
        public Item(string title, string link, string description)
        {
            this.Title = title;
            this.Link = link;
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        [XmlElement("author")]
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        /// <summary>
        /// Gets or sets the commments.
        /// </summary>
        /// <value>
        /// The commments.
        /// </value>
       [XmlElement("comments")]
        public string Commments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        [XmlElement("source")]
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [XmlElement("category")]
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        /// <value>
        /// The GUID.
        /// </value>
        [XmlElement("guid")]
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [XmlElement("enclosure")]
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private string _author;
        private string _comments;
        private string _source;
        private string _category;
        private string _guid;
        private Image _image;
        
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRootAttribute(ElementName = "image")]
    public class Image : Element
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [XmlElement("url")]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _url;
    }

    /// <summary>
    /// 
    /// </summary>
    [XmlRootAttribute(ElementName = "channel")]
    public class Channel : Element
    {
        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        /// <value>
        /// The copyright.
        /// </value>
        [XmlElement("copyright")]
        public string Copyright
        {
            get { return _copyright; }
            set { _copyright = value; }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [XmlElement("language")]
        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [XmlElement("image")]
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [XmlElement(ElementName = "item", Type = typeof(Item))]
        public List<Item> Items
        {
            get { return _item; }
            set { _item = value; }
        }

        private string _copyright;
        private string _language;
        private Image _image;
        private List<Item> _item;
    }
}
