(function () {
    var service = abp.services.app.survey;

    $("#surveyCreateForm").on("submit", function (e) {
        e.preventDefault();
        var data = $(this).serializeFormToObject();
        console.log(data);
        service.createEditSurvey(data).done(function () {
            window.location.reload();
        });
    });

    $("body").on("click",
        ".js-delete",
        function () {
            var id = $(this).data("id");
            var result = window.confirm("¿Desea eliminar esta encuesta?");
            if (result) {
                service.deleteSurvey(id).done(function () {
                    window.location.reload();
                });
            }
        });
})();