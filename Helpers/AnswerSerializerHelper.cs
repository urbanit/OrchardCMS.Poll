using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Urbanit.Polls.Models;
using Newtonsoft.Json;

namespace Urbanit.Polls.Helpers {
    internal class AnswerSerializerHelper {
        public static string SerializeAnswerList(IList<Answer> answers) {
            return JsonConvert.SerializeObject(answers);
        }

        public static string SerializeAnswerList(string answers) {
            return JsonConvert.SerializeObject(GenerateAnswerList(answers));
        }

        public static IList<Answer> DeserializeAnswerList(string serializedAnswers) {
            return JsonConvert.DeserializeObject<List<Answer>>(serializedAnswers);
        }

        private static IList<Answer> GenerateAnswerList(string answers) {
            string[] splitted = answers.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            return splitted.Select(item => new Answer { Text = item, VoteCount = 0 }).ToList();
        }
    }

}