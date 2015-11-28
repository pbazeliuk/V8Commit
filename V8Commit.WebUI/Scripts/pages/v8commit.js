
(function () {

    var isV8File = function checkFile($el) {

        var fileName = $el.val();
        var fileExtension = "";

        if (fileName.lastIndexOf(".") > 0) {
            fileExtension = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
        }

        if (fileExtension.toLowerCase() == "cf"  ||
            fileExtension.toLowerCase() == "cfu" ||
            fileExtension.toLowerCase() == "erf" ||
            fileExtension.toLowerCase() == "epf" ) {
            return true;
        }
        else {
            $el.val("");
            $el.change();
            alert("Ошибка: файл не является файлом платформы «1C:Предприятие 8»");
            return false;
        }

    };

    var onChangeFile = function (e) {
        var el = e.target;
        var $el = $(el);

        if ($el.val() == "") {
            return;
        }

        if (isV8File($el)) {  
            
            var files = el.files;
            if (files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < files.length; x++) {
                        data.append("file" + x, files[x]);
                    }

                    $.ajax({
                        method: "POST",
                        url: "/V8Commit/FileUpload", //Server script to process data
                        enctype: 'multipart/form-data',
                        //dataType: "html",
                        //xhr: function () {  // Custom XMLHttpRequest
                        //    var myXhr = $.ajaxSettings.xhr();
                        //    if (myXhr.upload) { // Check if upload property exists
                        //        myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
                        //   }
                        //    return myXhr;
                        //},
                        //Ajax events
                        success: function () {
                            console.log("Data uploaded! (success)");
                        },
                        // Form data
                        data: data,
                        //Options to tell jQuery not to process data or worry about content-type.
                        cache: false,
                        contentType: false,
                        processData: false
                    })
                    .done(function (data) {
                        //$(".TasksPartial").html(data);

                        console.log("Data uploaded! (done)");
                    })
                    .fail(function (msg) {
                        console.log("Some error with file loading...");
                    });
                }
            }
        }
    };

    var init = function () {
        $(".filestyle").on("change ", onChangeFile);
    };

    $(function () { 
        init();
    });

})();