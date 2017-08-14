(function () {
    $("body").on("click", ".next-step", function () {
        var surveyId = $(this).data("survey");
        var nextId = $(this).data("next");

        var service = abp.services.app.survey;
        var getAnswers = function() {
            var value = $("input:radio[name=Question]:checked").val();
            if (!value) return [];
            return [value];
        };
        service.answerQuestion({
            SurveyId: surveyId,
            QuestionId: $("#QuestionId").val(),
            OfferedAnswerIds: getAnswers(),
            OtherText: $(".OtherText").val()
        }).done(function () {
            if (nextId == 0) {
                window.location.reload();
            } else {
                $("#survey_section").load("/Survey/GetNextQuestion?questionId=" + nextId + "&surveyId=" + surveyId);
            }

        });
    });
})();
