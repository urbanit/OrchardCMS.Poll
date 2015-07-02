﻿(function ($) {
    $.extend(true, {
        Polls: {
            vote: function (votingId, radioButtonName, userName, url) {
                if (votingId && radioButtonName) {
                    var answerId = $('input:radio[name=' + radioButtonName + ']:checked').val();

                    $.getJSON(
                    url,
                    { votingId: votingId, answerId: answerId },
                    function (data) {
                        var textAfterVote = "";
                        $.each(data, function (i, field) {
                            textAfterVote = textAfterVote + field.Text + ": " + field.VoteCount + " votekuki(s)";
                        });
                        $.Polls.createvotingcookie(votingId, userName);
                        $.Polls.showvotingresult(votingId, textAfterVote);
                    });
                }
            },

            showvotingresult: function (votingId, text) {
                var divId = '#polls_' + votingId;
                $(divId + "_result").append(text);
                $(divId + " div.polls_answers").hide();
                $(divId + " input").attr("disabled", true);
            },

            createvotingcookie: function (votingId, userName) {
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + 60);
                var value = 1;
                var cookieName = "polls_" + votingId + "_" + userName;

                document.cookie = escape(cookieName) + "=" + escape(value) + "; expires=" + exdate.toUTCString() + "; path=/";
            },

            readvotingcookie: function (votingId, userName) {
                var cookieName = "polls_" + votingId + "_" + userName;
                var nameEq = escape(cookieName) + "=";
                var ca = document.cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                    if (c.indexOf(nameEq) == 0) {
                        return unescape(c.substring(nameEq.length, c.length));
                    }
                }
                return null;
            }
        }
    });
})(jQuery);