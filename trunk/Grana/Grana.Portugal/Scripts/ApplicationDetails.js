ApplicationDetails =
{
    ChangeEmploymentStatus: function (dropdown) {

        var status = $(dropdown).val();
        $('#employmentStatusTable').removeClass();
        $('#employmentStatusTable').addClass(status);
    },


    //this method will show the section that was clicked by the user
    ShowInfoSection: function (section) {

        //Unmark the row that was previously clicked
        $('.stripe').css('color', '');
        $('.stripe').css('font-weight', '');
        $('.stripe').css('background-color', '');
        $('.stripe .grid_tags_and_date').css('background-image', '');
        $('.stripe .grid_tags_and_date').css('background-color', '');

        //Mark the row that was clicked
        $('#' + section).css('font-weight', 'bolder');
        $('#' + section).css('background-color', '#A7CCEA');
        $('#' + section + ' .grid_tags_and_date').css('background-image', 'none');
        $('#' + section + ' .grid_tags_and_date').css('background-color', '#A7CCEA');

        //open the right panel and fade in
        ApplicationDetails.ShowRightPanelSection(section);
    },
    ShowRightPanelSection: function (section) {

        //hide the success message
        $('#successValidator').hide();
        $('.section').hide();

        $('#rightPanel').show();
        $('.' + section).fadeToggle("100", function () { });
        $('.dropdown').hide();

    },


    CloseRightPanel: function () {

        $('#rightPanel').fadeOut();
    },

    ShowElement: function (element) {

        $('.' + element).show();

    },
    HideElement: function (element) {

        $('.' + element).hide();

    },

    SendSms: function (url, applicationId) {

        var message = $('#smsMessage').val();

        //call the action method
        $.post(url, { "message": message, "applicationId": applicationId }, function (result) {

            if (!result.Success) {
                //set the error message span and show it
                $("#fieldvalsms").show();
                $("#fieldvalsms").text(result.ErrorMessage);
                $('#successValidator').hide();

            } else {
                //hide error span in case is showing and show the success validator message with custom method
                $("#fieldvalsms").hide();
                $('.complete-text').text('Sms enviado com sucesso');
                $('#successValidator').show();

            }
        });
    },

    SendEmail: function (url) {

        //get subject and message from input boxes
        var subject = $('#subject').val();
        var message = $('#message').val();
        //get customer's first name
        var firtName = $('#Applicant_FirstName').val();

        //call the action method
        $.post(url, { "subject": subject, "message": message, "firstName": firtName }, function (result) {

            if (!result.Success) {
                //set the error message span and show it
                $("input[name='subject']").nextAll('.field-validation-error').text(result.ErrorMessage);
                $('#successValidator').hide();

            } else {
                //hide error span in case is showing and show the success validator message with custom method
                $("input[name='subject']").nextAll('.field-validation-error').hide();
                $('.complete-text').text('Email enviado com sucesso');
                $('#successValidator').show();

            }



        });
    },
    ApplicationPopup: function (idPopup) {
        $('#decision_menu_dropdown_menu').hide();
        Popup.centerPopup(idPopup);
        Popup.loadPopup(idPopup);

    },
    //this method is used for the decision status update
    UpdateApplicationStatus: function (url, applicationId, applicationStatus) {
        var confirm = window.confirm('Tem a certeza?');
        if (confirm) {

            var reasonId;
            if (applicationStatus == 'Approved') {
                reasonId = $('#reasonsApproved').val();
            }
            if (applicationStatus == 'Declined') {
                reasonId = $('#reasonsDeclined').val();
            }
            if (applicationStatus == 'Undecided') {
                reasonId = $('#reasonsPending').val();
            }
            $.post(url, { "applicationId": applicationId, "applicationStatus": applicationStatus, "reasonId": reasonId }, function (result) {

                if (result.Success) {

                }
            });
        }
    },
    AddNote: function (url, applicationId, applicantId) {

        var textToSet = $('#noteText').val();

        $.post(url, { "applicationId": applicationId, "applicantId": applicantId, "noteText": textToSet }, function (result) {

            if (!result.Success) {
                //set the error message span and show it
                $("#noteText").nextAll('.field-validation-error').text(result.ErrorMessage);
                $('#successValidator').hide();

            } else {
                //hide error span in case is showing and show the success validator message with custom method
                $("input[name='noteText']").nextAll('.field-validation-error').hide();
                $('.complete-text').text('Nota adicionada com sucesso');
                $('#successValidator').show();
                $(result.Html).appendTo('#notesTable');
                //$('#notesTable tr:last').after(result.Html);
            }

        });
    },

    GetNotes: function (url, applicationId) {

        $.post(url, { "applicationId": applicationId }, function (result) {

            if (result.Success) {
                //remove all li
                $('.list-notes li').remove();
                //add li to ul
                $.each(result.Notes, function (index, value) {

                    $('.list-notes').append('<li>' + value + '</li>');

                });
                ApplicationDetails.ShowRightPanelSection('listnotes');
            }

        });

    },
    UpdateApplicationDetails: function (url) {
        var formData = $(':input').serialize();

        $('.error-section').html('');
        $('.error-section').hide();
        $('.field-validation-error').text('');

        //Unmark the row that was previously clicked
        $('.stripe').css('color', '');
        $('.stripe').css('font-weight', '');
        $('.stripe').css('background-color', '');
        $('.stripe .grid_tags_and_date').css('background-image', '');
        $('.stripe .grid_tags_and_date').css('background-color', '');

        $.post(url, formData, function (result) {

            if (result.Success) {


                $('.field-validation-error').text('');
                $('.error-section').html('');
                $('.error-section').hide();
                $('#first-name').html(result.ApplicationDetails.Applicant.FirstName);
                $('#last-name').html(result.ApplicationDetails.Applicant.LastName);
                $('.complete-text').text('Dados modificados com sucesso');
                $('#successValidator').show();
            }
            else {
                $('.error-section').show();


                $.each(result.ApplicationDetails.SectionsWithErrors, function (index, value) {

                    $('#' + value).css('font-weight', 'bolder');
                    $('#' + value).css('background-color', 'red');
                    $('#' + value + ' .grid_tags_and_date').css('background-image', 'none');
                    $('#' + value + ' .grid_tags_and_date').css('background-color', 'red');
                });

                $('.error-section').html('Corrija os erros');

                for (var i = 0; i < result.ApplicationDetails.NumberOfErrors; i++) {
                    var propertyName = result.ApplicationDetails.Errors[i].PropertyName;
                    var errorMessage = result.ApplicationDetails.Errors[i].ErrorMessage;

                    $("input[name='" + propertyName + "']").nextAll('.field-validation-error').text(errorMessage);
                    $('#successValidator').hide();
                }
            }

        });

    },
    Init: function () {

        //  $('.date-birth').mask("99/99/9999");
        $(function () {
            $(".date").datepicker(
                { dateFormat: 'dd/mm/yy'
                });
            $('#decision_menu').click(function () {

                $('#decision_menu_dropdown_menu').toggle();
                $('#decision_menu_dropdown_menu').focusout(function () {
                    $('#decision_menu_dropdown_menu').hide();
                });
            });

            $('#notes_menu').click(function () {

                $('#notes_menu_dropdown_menu').toggle();
                $('#notes_menu_dropdown_menu').focusout(function () {
                    $('#notes_menu_dropdown_menu').hide();
                });
            });




            $('#documents_menu').click(function () {

                $('#documents_menu_dropdown_menu').focusout(function () {
                    $('#documents_menu_dropdown_menu').hide();
                });

                $('#documents_menu_dropdown_menu').toggle();

            });

            //CLOSING POPUP  
            //Click the x event!  
            $(".popupClose").click(function () {
                Popup.disablePopup();
            });
            //Click out event!  
            $("#backgroundPopup").click(function () {
                Popup.disablePopup();
            });
            //Press Escape event!  
            $(document).keypress(function (e) {
                if (e.keyCode == 27 & popupStatus == 1) {
                    Popup.disablePopup();
                }
            });

        });
    }
}