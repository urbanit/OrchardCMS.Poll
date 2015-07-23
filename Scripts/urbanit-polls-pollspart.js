; (function ($, window, document, undefined) {

    var pluginName = "lombiq_Urbanit_Polls_PollsPart",
        defaults = {
            pollButtonId: "",
            votingId: "",
            radioButtonName: "",
            userName: "",
            targetUrl: "",
            divName: "",
            result: "",
            answer: "",
            showResults: "",
            defaultResultText: ""
        };

    function Plugin(element, options) {
        this.element = element;

        this.options = $.extend({}, defaults, options);

        this._defaults = defaults;
        this._name = pluginName;

        this.init();
    }

    Plugin.prototype.init = function () {
        if (this.readVotingCookie() != null || this.options.showResults) {
            this.showVotingResult(this.options.defaultResultText);
        }
        else {
            var options = this.options;
            var plugin = this;
            // Initializing click event on vote button.
            $("#" + this.options.pollButtonId).click(function () {
                $.ajax({
                    type: "POST",
                    url: options.targetUrl + "?answerId=" + $('input:radio[name=' + options.radioButtonName + ']:checked').val()
                })
                .success(function (data) {
                    var textResultAfterVote = "";
                    data = JSON.parse(data);
                    $.each(data, function (i, field) {
                        textResultAfterVote = textResultAfterVote + field.Text + ": " + field.VoteCount + " vote(s)<br>";
                    });
                    plugin.createVotingCookie();
                    plugin.showVotingResult(textResultAfterVote);
                })
                .error(function (error) {
                    alert(error.responseJSON.Text);
                });
            });
        }
    };

    Plugin.prototype.showVotingResult = function (resultText) {
        var divId = '#' + this.options.divName + this.options.votingId;
        $(divId + this.options.divName + this.options.result).append(resultText);
        $(divId + " div." + this.options.answer).hide();
        $(divId + " input").attr("disabled", true);
        // Removing Vote button
        $("#" + this.options.pollButtonId).remove();
    };

    Plugin.prototype.createVotingCookie = function () {
        console.log(this.options);
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + 60);
        var value = 1;
        var cookieName = this.options.divName + this.options.votingId + "_" + this.options.userName;

        document.cookie = escape(cookieName) + "=" + escape(value) + "; expires=" + exdate.toUTCString() + "; path=/";
    };

    Plugin.prototype.readVotingCookie = function () {
        var cookieName = this.options.divName + this.options.votingId + "_" + this.options.userName;
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
    };

    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName,
                new Plugin(this, options));
            }
        });
    }

})(jQuery, window, document);