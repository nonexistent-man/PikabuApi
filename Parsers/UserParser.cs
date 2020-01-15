using AngleSharp.Html.Parser;
using PikabuApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PikabuApi.Parsers
{
    public class UserParser : IParser<User>
    {
        public async Task<User> ParseAsync(string htmlSource)
        {
            var htmlParser = new HtmlParser();
            var htmlDocument = await htmlParser.ParseDocumentAsync(htmlSource);
            User result = new User();

            var user_name = "";

            if (htmlDocument.QuerySelector("h1.profile__nick>span") != null)
            {
                user_name = htmlDocument.QuerySelector("h1.profile__nick>span").InnerHtml;
            }
            else
            {
                user_name = htmlDocument.QuerySelector("h1.profile__nick>a").InnerHtml;
            }

            int user_rating = 0;
            var ratingNode = htmlDocument.QuerySelectorAll("span.profile__digital")
                                        .Where(n => n.QuerySelectorAll("span")
                                                    .Where(s => s.InnerHtml == "рейтинг")
                                                    .Count() != 0)
                                        .FirstOrDefault();
            if(ratingNode != null)
            {
                if(ratingNode.HasAttribute("aria-label"))
                {
                    var rating_str = ratingNode.Attributes["aria-label"].Value;
                    int.TryParse(Regex.Replace(rating_str, @"\s+", ""), out user_rating);
                }
                else
                {
                    int.TryParse(ratingNode.QuerySelector("b").InnerHtml, out user_rating);
                }
            }

            int user_hot_posts = 0;
            var hotPostsNode = htmlDocument.QuerySelectorAll("span.profile__digital")
                                            .Where(n => n.QuerySelectorAll("span")
                                                        .Where(s => s.InnerHtml == "в горячем")
                                                        .Count() != 0)
                                            .FirstOrDefault();
            if (hotPostsNode != null)
            {
                if (hotPostsNode.HasAttribute("aria-label"))
                {
                    var subs_str = hotPostsNode.Attributes["aria-label"].Value;
                    int.TryParse(Regex.Replace(subs_str, @"\s+", ""), out user_hot_posts);
                }
                else
                {
                    int.TryParse(hotPostsNode.QuerySelector("b").InnerHtml, out user_hot_posts);
                }
            }

            int user_posts = 0;
            var postsNode = htmlDocument.QuerySelectorAll("span.profile__digital")
                                            .Where(n => n.QuerySelectorAll("span")
                                                        .Where(s => s.InnerHtml == "пост" ||
                                                                    s.InnerHtml == "поста" ||
                                                                    s.InnerHtml == "постов")
                                                        .Count() != 0)
                                            .FirstOrDefault();
            if (postsNode != null)
            {
                if (postsNode.HasAttribute("aria-label"))
                {
                    var subs_str = postsNode.Attributes["aria-label"].Value;
                    int.TryParse(Regex.Replace(subs_str, @"\s+", ""), out user_posts);
                }
                else
                {
                    int.TryParse(postsNode.QuerySelector("b").InnerHtml, out user_posts);
                }
            }

            int user_subs = 0;
            var subsNode = htmlDocument.QuerySelectorAll("span.profile__digital")
                                            .Where(n => n.QuerySelectorAll("span")
                                                        .Where(s => s.InnerHtml == "подписчик" ||
                                                                    s.InnerHtml == "подписчика" ||
                                                                    s.InnerHtml == "подписчиков")
                                                        .Count() != 0)
                                            .FirstOrDefault();
            if (subsNode != null)
            {
                if (subsNode.HasAttribute("aria-label"))
                {
                    var subs_str = subsNode.Attributes["aria-label"].Value;
                    int.TryParse(Regex.Replace(subs_str, @"\s+", ""), out user_subs);
                }
                else
                {
                    int.TryParse(subsNode.QuerySelector("b").InnerHtml, out user_subs);
                }
            }

            int user_comments = 0;
            var commentsNode = htmlDocument.QuerySelectorAll("span.profile__digital")
                                            .Where(n => n.QuerySelectorAll("span")
                                                        .Where(s => s.InnerHtml == "комментариев" ||
                                                                    s.InnerHtml == "комментарий" ||
                                                                    s.InnerHtml == "комментария")
                                                        .Count() != 0)
                                            .FirstOrDefault();
            if (commentsNode != null)
            {
                if (commentsNode.HasAttribute("aria-label"))
                {
                    var comments_str = commentsNode.Attributes["aria-label"].Value;
                    int.TryParse(Regex.Replace(comments_str, @"\s+", ""), out user_comments);
                }
                else
                {
                    int.TryParse(commentsNode.QuerySelector("b").InnerHtml, out user_comments);
                }
            }

            int user_positive = 0;
            int.TryParse(htmlDocument.QuerySelector(".profile__pluses").InnerHtml, out user_positive);
            int user_negative = 0;
            int.TryParse(htmlDocument.QuerySelector(".profile__minuses").InnerHtml, out user_negative);

            var userInfoNode = htmlDocument.QuerySelector(".profile__user-information");

            byte user_gender = 0;
            if (userInfoNode != null)
            {
                if(userInfoNode.InnerHtml.Contains("пикабушник"))
                    user_gender = 0;
                else
                    user_gender = 1;
            }

            var dateString = userInfoNode.QuerySelector("time").Attributes["datetime"].Value;
            var user_register_date = DateTime.ParseExact(dateString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);

            List<Community> user_communities = new List<Community>();
            var communityNodes = htmlDocument.QuerySelectorAll("a.community__title");
            foreach(var i in communityNodes)
            {
                Community community = new Community();
                community.Address = i.Attributes["href"].Value.Replace("/community/", "");
                community.Name = i.InnerHtml;
                user_communities.Add(community);
            }

            result.Name = user_name;
            result.Rating = user_rating;
            result.SubscribersCount = user_subs;
            result.CommentsCount = user_comments;
            result.PostsCount = user_posts;
            result.HotPostsCount = user_hot_posts;
            result.PositiveCount = user_positive;
            result.NegativeCount = user_negative;
            result.Gender = user_gender;
            result.SinceRegisterDate = user_register_date;
            result.Communities = user_communities;

            return result;
        }
    }
}
