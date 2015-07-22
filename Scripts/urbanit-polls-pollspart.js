(function ($) {
    $(function () {
        $.extend(true, {
            Polls: {
                vote: function (votingId, radioButtonName, userName, targetUrl, divName, result, answer) {
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
                        $.Polls.createVotingCookie(votingId, userName, divName);
                        $.Polls.showVotingResult(votingId, textAfterVote, divName, result, answer);
                    })
                    .error(function (error) {
                        alert(error.responseJSON.Text);
                    });
                },

                showVotingResult: function (votingId, text, divName, result, answer) {
                    var divId = '#' + divName + votingId;
                    $(divId + divName + result).append(text);
                    $(divId + " div." + answer).hide();
                    $(divId + " input").attr("disabled", true);
                },

                createVotingCookie: function (votingId, userName, divName) {
                    var exdate = new Date();
                    exdate.setDate(exdate.getDate() + 60);
                    var value = 1;
                    var cookieName = divName + votingId + "_" + userName;

                    document.cookie = escape(cookieName) + "=" + escape(value) + "; expires=" + exdate.toUTCString() + "; path=/";
                },

                readVotingCookie: function (votingId, userName, divName) {
                    var cookieName = divName + votingId + "_" + userName;
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
