﻿namespace AviBlog.Web.Areas.Manage.Views.Index
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AviBlog.Core.Entities;
    using AviBlog.Core.Services.Search;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Snowball;
    using Lucene.Net.Store;

    using NUnit.Framework;

    [TestFixture]
    public class SearchEngineServiceTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            stopWords = new string[StopAnalyzer.ENGLISH_STOP_WORDS_SET.Values.Count + 1];
            stopWords[0] = "into";
            int i = 1;
            foreach (string value in StopAnalyzer.ENGLISH_STOP_WORDS_SET.Values) stopWords[i++] = value;
            _service = new SearchEngineService(new RAMDirectory(), new SnowballAnalyzer("English", stopWords));
        }

        [TearDown]
        public void DestroySearchEngine()
        {
            _service.Dispose();
        }

        #endregion

        private SearchEngineService _service;

        private string[] stopWords;

        [Test]
        public void Should_be_able_to_add_a_post_entry_to_the_index()
        {
            _service.AddPost(
                new Post
                    {
                        Id = 1,
                        PostContent = "This is a sample post",
                        Title = "This is the title",
                        Tags = new List<Tag> { new Tag { TagName = "Title" } },
                        IsPublished = true,
                        DatePublished = DateTime.UtcNow,
                        Slug = "this-is-the-title"
                    });

            _service.AddPost(
                new Post
                    {
                        Id = 1,
                        PostContent = "This is a sample post",
                        Title = "This is another title",
                        Tags = new List<Tag> { new Tag { TagName = "Title" } },
                        IsPublished = true,
                        DatePublished = DateTime.UtcNow,
                        Slug = "this-is-another-title"
                    });

            IEnumerable<SearchEngineResult> result = _service.Search("sample", 100);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Should_be_able_to_convert_post_to_search_result()
        {
            _service.AddPost(new Post()
                {
                    Id = 1,
                    PostContent = "This is a sample post",
                    Title = "This is the title",
                    Tags = new List<Tag> { new Tag { TagName = "Title" } },
                    IsPublished = true,
                    DatePublished = DateTime.UtcNow,
                    Slug = "this-is-the-title"
                });

            var result = _service.Search("sample", 100, 0) as List<SearchEngineResult>;

            Assert.AreEqual("This is the title", result[0].Title);
            Assert.AreEqual(1, result[0].PostId);
        }

        [Test]
        public void Should_be_able_to_remove_a_post()
        {
            _service.AddPost(new Post()
            {
                Id = 1,
                PostContent = "This is a sample post",
                Title = "This is the title",
                Tags = new List<Tag> { new Tag { TagName = "Title" } },
                IsPublished = true,
                DatePublished = DateTime.UtcNow,
                Slug = "this-is-the-title"
            });

            _service.RemovePost(1);
        }
    }
}