﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AviBlog.Core.Services;
using AviBlog.Core.ViewModel;

namespace AviBlog.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ITagService _tagService;

        public TagsController(IPostService postService, ITagService tagService)
        {
            _postService = postService;
            _tagService = tagService;
        }

        public ActionResult Tag(string id)
        {
            PostListViewModel postList = _postService.GetAllPostsForTag(id);
             return View(postList);
         }

        public ActionResult TagCloud()
        {
            IList<TagCloudViewModel> view = _tagService.GetTagCloud();
            return PartialView("_TagCloud",view);
        }

        public ActionResult TagStringContentMetaTag()
        {
            IList<TagCloudViewModel> view = _tagService.GetTagCloud();
            var tagList = view.Select(x => x.TagName).Distinct().ToList();
            string tags = tagList.Aggregate(string.Empty, (current, tagName) => string.Format("{0}{1},", current, tagName));
            return PartialView("_ContentMetaTag", tags);
        }
    }
}