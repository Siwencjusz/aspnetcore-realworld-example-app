﻿using Conduit.Infrastructure;
using Conduit.Infrastructure.Security;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Conduit.Features.Articles.Application.Commands.Create;

namespace Conduit.Entities
{
    public class Article
    {
        [JsonIgnore]
        public int Id { get; set; }
        //To trzeba żeby z wyszukiwania działało
        public string? Slug { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; } 
        public string? Body { get; private set; }
        public Person? Author { get; private set; }
        public bool? Favorited { get; private set; }
        public int FavoriteCount { get; private set; }
        [NotMapped]
        public List<string> TagList => ArticleTags.Where(x => x.TagId is not null).Select(x => x.TagId!).ToList();
        [JsonIgnore]
        public List<ArticleTag> ArticleTags { get; set; } = new();
        //stowrzyć komentarze
        //public List<Comment> Comments { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        private Article(ArticleCreateRequest request, Person autor)
        {
            Title = request.Title;
            Description = request.Description;
            Body = request.Body;
            Author = autor;
            Slug = request.Title.GenerateSlug();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public Article()
        {
        }

        public static Article CreateArticle(ArticleCreateRequest request, Person autor)
        {
            Article article = new(request, autor);
            return article;

        }
    }
}
