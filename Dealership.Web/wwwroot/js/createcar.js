//$(function () {
//    $(".brand-scriptSelect").change(function () {
//        var selectedBrandId = $(this).val();
//        if (dropDonwBrandModels.has(selectedBrandId)) {
//            var modelDropDown = $(".model-scriptSelect");
//            modelDropDown.empty();
//            dropDonwBrandModels[selectedBrandId].forEach((element) => modelDropDown.append(element));
//        }
//        else {
//            dropDonwBrandModels.set(selectedBrandId, []);
//            var url = "/Car/GetModelsByBrandId?brandId=" + selectedBrandId;
//            $.get({
//                url: url
//            }).done(function (data) {
//                var modelDropDown = $(".model-scriptSelect");
//                modelDropDown.empty();

//                data.forEach(function (currentElement) {
//                    var modelOption = $("<option />").val(currentElement.id).text(currentElement.name);
//                    dropDonwBrandModels[selectedBrandId].push(modelOption);
//                    modelDropDown.append(modelOption);
//                });
//            });
//        }
//    });
//});
//var brandModelsMap = new Map();
//$(function () {
//    $(".brand-scriptSelect").change(function () {
//        var selectedBrandId = $(this).val();
//        if (brandModelsMap.has(selectedBrandId)) {
//            console.log('Bam');
//        }
//        else {
//            var url = "/Car/GetModelsByBrandId?brandId=" + selectedBrandId;
//            $.get({
//                url: url
//            }).done(function (data) {
//                var modelDropDown = $(".model-scriptSelect");
//                modelDropDown.empty();
//                brandModelsMap.set(selectedBrandId, []);
//                data.forEach(function (currentElement) {
//                    var modelOption = $("<option />").val(currentElement.id).text(currentElement.name);
//                    brandModelsMap.get(selectedBrandId).push(currentElement);
//                    modelDropDown.append(modelOption);
//                });
//            });
//        }
//    });
//});

$(function () {
    $(".brand-scriptSelect").change(function () {
        var selectedBrandId = $(this).val();
        var url = "/Car/GetModelsByBrandId?brandId=" + selectedBrandId;
        $.get({
            url: url
        }).done(function (data) {
            var modelDropDown = $(".model-scriptSelect");
            modelDropDown.empty();
            data.forEach(function (currentElement) {
                var modelOption = $("<option />").val(currentElement.id).text(currentElement.name);
                modelDropDown.append(modelOption);
            });
        });
    });
});

$(function () {
    $(".gearbox-scriptSelect").change(function () {
        var selectedGear = $(this).val();
        var url = "/Car/GetGearsDependingOnGearBoxType?id=" + selectedGear;
        $.get({
            url: url
        }).done(function (data) {
            var modelDropDown = $(".nGears-scriptSelect");
            modelDropDown.empty();
            data.forEach(function (currentElement) {
                var modelOption = $("<option />").val(currentElement.numberOfGears).text(currentElement.numberOfGears);
                modelDropDown.append(modelOption);
            });
        });
    });
});