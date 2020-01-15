using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using PikabuApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PikabuApi.Parsers
{
    public class PostsParser : IParser<List<Post>>
    {
        public async Task<List<Post>> ParseAsync(string htmlSource)
        {
            var htmlParser = new HtmlParser();
            var htmlDocument = await htmlParser.ParseDocumentAsync(htmlSource);
            var htmlPostNodes = htmlDocument.QuerySelectorAll("article");

            List<Post> result = new List<Post>();
            foreach (var postNode in htmlPostNodes)
            {
                try
                {
                    int post_id = 0;
                    int post_rating = 0;
                    bool post_has_rating = false;
                    var post_tags = new List<string>();
                    string post_header;
                    string post_author;
                    var post_content = new List<PostContent>();
                    DateTime post_date = new DateTime();

                    int.TryParse(postNode.Attributes["data-story-id"].Value, out post_id);

                    if (int.TryParse(postNode.Attributes["data-rating"].Value, out post_rating))
                    {
                        post_has_rating = true;
                    }

                    foreach (var tagNode in postNode.QuerySelectorAll(".tags__tag"))
                    {
                        post_tags.Add(tagNode.InnerHtml);
                    }
                    
                    post_header = postNode.QuerySelector(".story__title-link").InnerHtml;
                    post_author = postNode.QuerySelector(".story__user").QuerySelector("a").Attributes["href"].Value.Replace("/@", "");

                    post_date = DateTime.ParseExact(postNode.QuerySelector("time").Attributes["datetime"].Value, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);

                    foreach (var contentNode in postNode.QuerySelectorAll(".story-block"))
                    {
                        PostContent content_block = new PostContent();

                        if (contentNode.ClassList.Contains("story-block_type_text"))
                        {
                            content_block.Data = contentNode.InnerHtml;
                            content_block.Type = 0;
                        }
                        else if (contentNode.ClassList.Contains("story-block_type_image"))
                        {
                            if (null != contentNode.QuerySelector("div.player"))
                            {
                                content_block.Type = 1;
                                content_block.Data = contentNode.QuerySelector("div.player").Attributes["data-source"].Value;
                            }
                            else
                            {
                                content_block.Type = 2;
                                content_block.Data = contentNode.QuerySelector("img").Attributes["data-large-image"].Value;
                            }
                        }
                        else if (contentNode.ClassList.Contains("story-block_type_video"))
                        {
                            content_block.Data = contentNode.QuerySelector("div.player").Attributes["data-source"].Value;
                            content_block.Type = 3;
                        }
                        content_block.PostId = post_id;
                        content_block.Index = post_content.Count;
                        post_content.Add(content_block);
                    }


                    Post post = new Post();
                    post.PikabuId = post_id;
                    post.Header = post_header;
                    post.Author = post_author;
                    post.Tags = JsonConvert.SerializeObject(post_tags);
                    post.HasRating = post_has_rating;
                    post.Rating = post_rating;
                    post.Content = post_content;
                    post.Comments = null;
                    post.CreationDate = post_date;

                    result.Add(post);
                }
                catch { }
            }

            return result;
        }
    }
}
