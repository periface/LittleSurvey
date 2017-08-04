(function (container, surveyId, config) {
    if (!config) config = {};
    config.answerBtn = ".js-answer";
    var $container = $("#container");

    var $body = $("body");

    $body.on("click",config.answerBtn,answerQuestion);

    function answerQuestion() {

        var $self = $(this);

        var questionId = $self.data("questionid"), answer = $self.data("answer");

        

        loadNextQuestion();
    }
    function loadNextQuestion() {

    }

    return {
        answerQuestion
    }
});