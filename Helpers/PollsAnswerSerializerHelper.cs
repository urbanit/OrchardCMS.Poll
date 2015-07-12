using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Urbanit.Polls.Models;

namespace Urbanit.Polls.Helpers
{
    internal class PollsAnswerSerializerHelper
    {
        public static string SerializeAnswerList(IList<PollsAnswer> answers)
        {
            return JsonConvert.SerializeObject(answers);
        }

        public static string SerializeAnswerList(string answers)
        {
            return JsonConvert.SerializeObject(GenerateAnswerList(answers));
        }

        public static IList<PollsAnswer> DeserializeAnswerList(string serializedAnswers)
        {
            return JsonConvert.DeserializeObject<List<PollsAnswer>>(serializedAnswers);
        }

        private static IList<PollsAnswer> GenerateAnswerList(string answers)
        {
            string[] splitted = answers.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            return splitted.Select(item => new PollsAnswer { Text = item, VoteCount = 0 }).ToList();
        }
    }

}