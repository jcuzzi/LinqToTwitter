﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToTwitter
{
    public partial class TwitterContext
    {
        /// <summary>
        /// Hides a reply to a tweet
        /// </summary>
        /// <param name="userID">ID of user who is liking tweet</param>
        /// <param name="tweetID">ID of the replying tweet</param>
        /// <param name="cancelToken">Optional cancellation token</param>
        /// <exception cref="TwitterQueryException">Will receive 403 Forbidden if <see cref="tweetID"/> is for a tweet that is not a reply</exception>
        /// <returns>Hidden status of reply - true if reply is hidden</returns>
        public async Task<LikedResponse?> LikeAsync(string userID, string tweetID, CancellationToken cancelToken = default)
        {
            _ = userID ?? throw new ArgumentNullException(nameof(userID), $"{nameof(userID)} is required.");
            _ = tweetID ?? throw new ArgumentNullException(nameof(tweetID), $"{nameof(tweetID)} is required.");

            string url = $"{BaseUrl2}users/{userID}/likes";

            var postData = new Dictionary<string, string>();
            var postObj = new LikedTweetID { TweetID = tweetID };

            RawResult =
                await TwitterExecutor.SendJsonToTwitterAsync(
                    HttpMethod.Post.ToString(),
                    url,
                    postData,
                    postObj,
                    cancelToken)
                   .ConfigureAwait(false);

            LikedResponse? result = JsonSerializer.Deserialize<LikedResponse>(RawResult);

            return result;
        }

        /// <summary>
        /// Hides a reply to a tweet
        /// </summary>
        /// <param name="userID">ID of user who is liking tweet</param>
        /// <param name="tweetID">ID of the replying tweet</param>
        /// <param name="cancelToken">Optional cancellation token</param>
        /// <exception cref="TwitterQueryException">Will receive 403 Forbidden if <see cref="tweetID"/> is for a tweet that is not a reply</exception>
        /// <returns>Hidden status of reply - false if reply is no longer hidden</returns>
        public async Task<LikedResponse?> UnlikeAsync(string userID, string tweetID, CancellationToken cancelToken = default)
        {
            _ = userID ?? throw new ArgumentNullException(nameof(userID), $"{nameof(userID)} is required.");
            _ = tweetID ?? throw new ArgumentNullException(nameof(tweetID), $"{nameof(tweetID)} is required.");

            string url = $"{BaseUrl2}users/{userID}/likes/{tweetID}";

            var postData = new Dictionary<string, string>();
            var postObj = new LikedTweetID { TweetID = tweetID };

            RawResult =
                await TwitterExecutor.SendJsonToTwitterAsync(
                    HttpMethod.Delete.ToString(),
                    url,
                    postData,
                    postObj,
                    cancelToken)
                   .ConfigureAwait(false);

            LikedResponse? result = JsonSerializer.Deserialize<LikedResponse>(RawResult);

            return result;
        }
    }
}
