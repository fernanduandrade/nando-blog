using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace my_blog.Models
{
    public class Post
    {
        public int Id {get; set;}
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Image {get; set;} = "";
        public string Category {get; set;} = "";
        public string Tags {get; set;} = "";
        public string Description {get; set;} = "";
        public string Slug { get => SlugFy(Title); set => SlugFy(Title); } 
        public DateTime Created { get; set; } = DateTime.Now;

        public string RemoveAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            char[] chars = text
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c)
                != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public string SlugFy(string slugValue)
        {
            // removendo acentos
            string slug = RemoveAccents(slugValue).ToLower();

            // remover caracter especiais
            slug = Regex.Replace(slug, @"[^A-Za-z0-9\s-]", "");

            // removendo espa�os em branco
            slug = Regex.Replace(slug, @"\s+", " ").Trim();

            // replace nos espa�os e adicionando tra�os
            slug = Regex.Replace(slug, @"\s", "-");

            return slug;
        }
    }
}
