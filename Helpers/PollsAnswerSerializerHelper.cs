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
            return JsonConvert.SerializeObject(GenerateAnswerList(answers));
        }

        public static IList<PollAnswer> DeserializeAnswerList(string serializedAnswers)
        {
            return JsonConvert.DeserializeObject<List<PollAnswer>>(serializedAnswers);
        }

        private static IList<PollAnswer> GenerateAnswerList(string answers)
        {
            string[] splitted = answers.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            return splitted.Select(item => new PollAnswer { Text = item, VoteCount = 0 }).ToList();
        }
    }

}