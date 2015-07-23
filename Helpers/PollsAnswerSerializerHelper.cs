using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Helpers
{
    internal class PollsAnswerSerializerHelper
    {
        public static string SerializeAnswerList(IList<PollAnswer> answers)
        {
            return JsonConvert.SerializeObject(answers);
        }

        public static string GenerateDefaultAnswerList(string answers)
        {
            if (string.IsNullOrEmpty(answers)) return "";

            var splitAnswers = answers.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
            splitAnswers.RemoveAll(l => string.IsNullOrWhiteSpace(l));
            return JsonConvert.SerializeObject(
                splitAnswers
                    .Select(item => new PollAnswer { Text = item, VoteCount = 0 }));
        }

        public static List<PollAnswer> DeserializeAnswerList(string serializedAnswers)
        {
            return JsonConvert.DeserializeObject<List<PollAnswer>>(serializedAnswers);
        }
    }

}