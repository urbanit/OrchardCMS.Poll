(function ($) {
    $(function () {
        $.extend(true, {
            Polls: {
                vote: function (votingId, radioButtonName, userName, targetUrl, divName, polls_, result, answer) {
                    var answerId = $('input:radio[name=' + radioButtonName + ']:checked').val();

                    $.ajax({
                        type: "POST",
                        url: targetUrl + "?answerId=" + answerId
                    })
                    .success(function (data) {
                        var textAfterVote = "";
                        data = JSON.parse(data);
                        $.each(data, function (i, field) {

                            textAfterVote = textAfterVote + field.Text + ": " + field.VoteCount + " vote(s)<br>";

                        });
                        $.Polls.createvotingcookie(votingId, userName, divName, polls_);
                        $.Polls.showvotingresult(votingId, textAfterVote, divName, polls_, result, answer);

                        console.log(data);
                    })

                    .error(function (error) {
                        console.log(error);
                    });
                },

                showvotingresult: function (votingId, text, divName, polls_, result, answer) {
                    var divId = '#' + divName + polls_ + votingId;
                    $(divId + divName + result).append(text);
                    $(divId + " div." + answer).hide();
                    $(divId + " input").attr("disabled", true);
                },

                createvotingcookie: function (votingId, userName, divName, polls_) {
                    var exdate = new Date();
                    exdate.setDate(exdate.getDate() + 60);
                    var value = 1;
                    var cookieName = divName + polls_ + votingId + "_" + userName;

                    document.cookie = escape(cookieName) + "=" + escape(value) + "; expires=" + exdate.toUTCString() + "; path=/";
                },

                readvotingcookie: function (votingId, userName, divName, polls_) {
                    var cookieName = divName + polls_ + votingId + "_" + userName;
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
    });
}(jQuery));
