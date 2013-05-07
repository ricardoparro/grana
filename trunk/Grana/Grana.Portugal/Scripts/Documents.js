Documents = {

    UploadDocument: function (urlToPostTo) {
        //starting setting some animation when the ajax starts and completes
        $("#uploadProgress").show();
        $("#uploadDialog").hide();
        var documentName = $("#documentName").val();

        urlToPostTo = urlToPostTo + "&documentName=" + documentName;

        /*
        http://www.phpletter.com/Our-Projects/AjaxFileUpload/
        preparing ajax file upload
        url: the url of script file handling the uploaded files
        fileElementId: the file type of input element id and it will be the index of  $_FILES Array()
        dataType: it support json, xml
        secureuri:use secure protocol
        success: call back function when the ajax complete
        error: callback function when the ajax failed
        */

        $.ajaxFileUpload({

            url: urlToPostTo,
            secureuri: false,
            fileElementId: 'fileToUpload',
            dataType: 'json',
            success: function (data, status) {

                if (data.success) {


                    $("input[name='documentName']").nextAll('.field-validation-error').text('');
                    $("#uploadProgress").hide();
                    $("#uploadDialog").show();
                    $('#fileToUpload').val('');
                    $('.complete-text').text('Documento guardado com sucesso');
                    $('#successValidator').show();
                    $(data.html).appendTo('#documentsTable');
                    
                }
                else {
                    $("#uploadProgress").hide();
                    $("#uploadDialog").show();
                    $('#fileToUpload').val('');
                    //set the error message span and show it
                    $("input[name='documentName']").nextAll('.field-validation-error').text(data.errorMsg);
                    $('#successValidator').hide();
                }
            },
            error: function (data, status, e) {
                alert(e);
            }
        });

    },
  
    ShowUploadBox: function () {
        $("#uploadDialog").show();
        $("#changePhotoButton").hide();
    }


};