$(function () {
    $(".brand-scriptSelect").change(function () {
        var selectedBrandId = $(this).val();
        var url = "/Car/GetModelsByBrandId?brandId=" + selectedBrandId;
        $.get({
            url: url
        }).done(function (data) {
            var modelDropDown = $(".model-scriptSelect");
            modelDropDown.empty();
            var defaultOption = $("<option />").val("0").text("All");
            modelDropDown.append(defaultOption);
            data.forEach(function (currentElement) {
                var modelOption = $("<option />").val(currentElement.id).text(currentElement.name);
                modelDropDown.append(modelOption);
            });
        });
    });


    $(".searchBtn-scriptSelect").click(function () {
        var selectedBrandId = $(".brand-scriptSelect").val();
        var selectedModelId = $(".model-scriptSelect").val();
        var sortKey = $(".sort-scriptSelect").val();
        var url = "/Car/LoadCars?brandId=" + selectedBrandId + "&modelId=" + selectedModelId + "&sort=" + sortKey + "&page=0";
        $.get({
            url: url
        }).done(function (response) {
            $(".search-result-partial").html(response);
        });
    });

    $(document).on("click", ".page-scriptSelect", function () {
        var selectedBrandId = $(".brand-scriptSelect").val();
        var selectedModelId = $(".model-scriptSelect").val();
        var pageN = $(this).val();
        var sortKey = $(".sort-scriptSelect").val();
        var url = "/Car/LoadCars?brandId=" + selectedBrandId + "&modelId=" + selectedModelId + "&sort=" + sortKey + "&page=" + pageN;
        $.get({
            url: url
        }).done(function (response) {
            $(".search-result-partial").html(response);
        });
    });
});