

/* Put Footer at Bottom if page is less than window height */

    $(document).ready(function () {
        if ($(document).height() <= $(window).height()) {
            $("page-footer").addClass("w-to-bottom");
        }
    });


// Form Handling - Login Form 


$(document).ready(function(){
    $('#w-login-form').submit(function(e){

        var errors = checkLogin();

        if (errors == true) {
                e.preventDefault();
        }

        else if (errors == false) {
                e.preventDefault();
                sendLogin();
        }
    });
});


// Form Handling - New Account Form 


$(document).ready(function(){
    $('#w-account-form').submit(function(e){

        var errors = checkAccount();

        if (errors == true) {
                e.preventDefault();
        }

        else if (errors == false) {
                e.preventDefault();
                createAccount();
        }
    });
});

// Form Handling - Add Feature Form

    $(document).ready(function() {
        $('#w-feature-form').submit(function (e) {
            var errors = checkValues();
            if (errors == true) {
                e.preventDefault();
            }

            else if (errors == false) {
                e.preventDefault();
                addWebFeature();
            
            }
        });
    });


    // Form Handling - Add Category Form 

    $(document).ready(function() {
        $('#w-category-form').submit(function (e) {
            var errors = checkCatValues();
            if (errors == true) {
                e.preventDefault();
                alert("Please Correct the following Errors below");
            }

            else if (errors == false) {
                e.preventDefault();

                var cat = $("#w_catnew").val();
                var catdesc = $("#w_catdesc").val();
                var cattags = $("#w_cattag").val();

                var cat = cat.trim();
                var catdesc = catdesc.trim();
                var cattags = cattags.trim();

                var cat = stripSlashes(cat);
                var catdesc = stripSlashes(catdesc);
                var cattags = stripSlashes(cattags);

                var formData = new FormData();

                var file_data = $('#w_catimg').prop('files')[0]; 

                formData.append('w_catnew', cat);
                formData.append('w_catdesc', catdesc);
                formData.append('w_cattag', cattags);

                formData.append('file', file_data);


            $.ajax({
                url: '/handlers/update-categories.php',
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (status) {
                    $('#w-category-form')[0].reset();
                    $('#w-category-form').fadeOut();
                    $('#w-category-form').after('<p class="w-submit-message">New Category Submitted ' + status + '</p><p class="w-submit-message"><a href="/add-category.php">Submit Again </a></p><br /><p class="w-submit-message"><a href="/add-subcategory.php">Create New Subcategory </a></p>').fadein();
                    console.log(data);
                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    alert('An error has occured with your submisssion');
                    console.log('Details: ' + desc + '\nError:' + err);
                    console.log(data);
                }
            }); // end ajax call
            
            }
        });
    });


// Form Handling - Add Subcategory Form

    $(document).ready(function() {
        $('#w-subcat-form').submit(function (e) {
            
            var errors = false;
            //var errors = checkSubcatValues();
            if (errors == true) {
                e.preventDefault();
            }

            else if (errors == false) {
                e.preventDefault();
                add_subcategory();
            
            }
        });
    });


//Get Subcategory Based on Category

$(document).ready(function(){
    $('.w-resource-cat #w_cat').change(function () {
     getSubcategories();
    });
});



//AJAX functions


function sendLogin(){

    var w_user = escapeHtml($('#w-user').val());
    var w_pass = escapeHtml($('#w-pass').val());

     $.ajax({
                url: '/handlers/login-user.php',
                type: 'POST',
                data: { 'w_user': w_user, 'w_pass': w_pass },
                dataType: 'text',
                success: function(status){                    
                        //alert(status);
                       $('body').html(status)
                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    alert('An error has occured with your submisssion');
                    console.log('Details: ' + desc + '\nError:' + err);
                }
            }); // end ajax call
}


function createAccount(){

    var w_firstname = escapeHtml($('#w-firstname').val());
    var w_lastname = escapeHtml($('#w-lastname').val());

    var w_username = escapeHtml($('#w-username').val());
    var w_password = escapeHtml($('#w-password').val());

     $.ajax({
                url: '/handlers/create-account.php',
                type: 'POST',
                data: { 'w_username': w_username, 'w_password': w_password, 'w_firstname': w_firstname, 'w_lastname': w_lastname },
                dataType: 'text',
                success: function(status){                    
                        // $('body').html(status)
                        //alert(status);
                       
                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    alert('An error has occured with your submisssion');
                    console.log('Details: ' + desc + '\nError:' + err);
                }
            }); // end ajax call
}



function getSubcategories(){

    var w_cat = $("#w_cat").val();

    $.ajax({
                url: '/handlers/get-subcategories.php',
                async: true,
                type: 'POST',
                data: { 'w_cat': w_cat },
                dataType: 'html',
                success: function(response){                    
                        //alert(response);
                        $('#w_subcat').html(response)

                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    alert('An error has occured with your submisssion');
                    console.log('Details: ' + desc + '\nError:' + err);
                }
            }); // end ajax call
        }


    function addWebFeature() {

        var url = $("#w_url").val();
        var devtools = $("#w_dev_tools").val();
        var cat = $("#w_cat").val();
        var subcat = $("#w_subcat").val();
        var tags = $("#w_tag").val();
        var snippet = $("#w_code").val();
        var description = $("#w_desc").val();
        var mode = $("#mode").val();


        var url = url.trim();
        var devtools = devtools.trim();
        var cat = cat.trim();
        var subcat = subcat.trim();
        var tags = tags.trim();
        var snippet = snippet.trim();
        var description = description.trim();
        var mode = mode.trim();

        var url = stripSlashes(url);
        var devtools = stripSlashes(devtools);
        var cat = stripSlashes(cat);
        var subcat = stripSlashes(subcat);
        var tags = stripSlashes(tags);
        var snippet = stripSlashes(snippet);
        var description = stripSlashes(description);



            $.ajax({
                url: '/handlers/update-resources.php',
                type: 'post',
                data: { 'url': url, 'devtools': devtools, 'cat': cat, 'subcat': subcat, 'tags': tags, 'snippet': snippet, 'description': description, 'mode': mode },
                dataType: 'text',
                success: function (status) {
                    $("#w-feature-form")[0].reset();
                    $('#w-feature-form').fadeOut();
                    $('#w-feature-form').after('<p class="w-submit-message">Resource Submitted ' + status + '</p><p class="w-submit-message"><a href="/add-resource.php">Submit Another Resource </a></p>').fadein();

                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    alert('An error has occured with your submisssion');
                    console.log('Details: ' + desc + '\nError:' + err);
                }
            }); // end ajax call
        }

   
    function add_subcategory(){

        var w_cat = $("#w_cat").val();
        var w_subcatname = $("#w_subcatname").val();
        var w_subcaturl = $("#w_subcaturl").val();
        var w_subcatdesc = $("#w_subcatdesc").val();


        var w_cat = w_cat.trim();
        var w_subcatname = w_subcatname.trim();
        var w_subcaturl = w_subcaturl.trim();
        var w_subcatdesc = w_subcatdesc.trim();

        var w_subcatname = stripSlashes(w_subcatname);
        var w_subcaturl = stripSlashes(w_subcaturl);
        var w_subcatdesc = stripSlashes(w_subcatdesc);


        $.ajax({
             url: '/handlers/update-subcategories.php',
                type: 'post',
                data: { 'w_cat': w_cat, 'w_subcaturl': w_subcaturl, 'w_subcatname': w_subcatname, 'w_subcatdesc': w_subcatdesc },
                dataType: 'text',
                success: function (status) {
                    $("#w-subcat-form")[0].reset();
                    $('#w-subcat-form').fadeOut();
                    $('#w-subcat-form').after('<p class="w-submit-message">Resource Submitted ' + status + '</p><p class="w-submit-message"><a href="/add-subcategory.php">Add Another Subcategory </a></p>').fadein();

                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    alert('An error has occured with your submisssion');
                    console.log('Details: ' + desc + '\nError:' + err);
                }


        });


    }

    
        //Initialize with HTML
        var code = $(".codemirror-textarea")[0];
        var editor = CodeMirror.fromTextArea(code, {
                lineNumbers : true,
                mode: "null",
                htmlMode: true
        });
        
        function loadModeForSelectedOption() {     

            var script = $("#mode option:selected").attr('data-script');
            var mode = $("#mode option:selected").attr('data-mime-type');
            loadJS(script, function () {
                editor.setOption("mode", mode);
            });
        }

        function loadJS(src, callback) {
            var s = document.createElement('script');
            s.src = src;
            s.async = true;
            s.onreadystatechange = s.onload = function () {
                var state = s.readyState;
                if (!callback.done && (!state || /loaded|complete/.test(state))) {
                    callback.done = true;
                    callback();
                }
            };
            document.getElementsByTagName('head')[0].appendChild(s);
        }


    

    // Validation Functions


    function checkSubcatValues() {

        var error = false;

        $('.error').hide();

        if ($('#w_url').val() == "") {
            $('#w_url').after('<span class="error"> Please enter the Name of the Subcategory.</span>');
            error = true;
        }

        if ($('#w_dev_tools').val() == "") {
            $('#w_dev_tools').after('<span class="error"> Please enter relevant Developer Tools.</span>');
            error = true;
        }


        /*   var captcha = grecaptcha.getResponse();

            if(captcha.length == 0){
                error = true;
            $('.g-recaptcha').after('<span class="error"> Please check the box.</span>');
            }*/

        return error;
    }



    function checkLogin(){

        var loginError = false;

        var loginUser = $('#w-user').val();
        var loginPass = $('#w-pass').val();

       if ( loginUser == "") {
            $('#w-user').after('<span class="error"> Please enter Your Username.</span>');
            error = true;
        }

        if ( loginPass == "") {
            $('#w-pass').after('<span class="error"> Please enter Your Password.</span>');
            error = true;
        }

        return loginError;

    }





    function checkValues() {

        var error = false;

        $('.error').hide();

        if ($('#w_url').val() == "") {
            $('#w_url').after('<span class="error"> Please enter the URL of the Resource.</span>');
            error = true;
        }

        if ($('#w_dev_tools').val() == "") {
            $('#w_dev_tools').after('<span class="error"> Please enter relevant Developer Tools.</span>');
            error = true;
        }


        /*   var captcha = grecaptcha.getResponse();

            if(captcha.length == 0){
                error = true;
            $('.g-recaptcha').after('<span class="error"> Please check the box.</span>');
            }*/

        return error;
    }


    function checkCatValues() {

        var error = false;

        $('.error').hide();

        if ($('#cat').val() == "") {
            $('#cat').after('<span class="error"> Please enter the URL of the Resource.</span>');
            error = true;
        }

        if ($('#catdesc').val() == "") {
            $('#catdesc').after('<span class="error"> Please enter relevant Developer Tools.</span>');
            error = true;
        }

        return error;

    }





    function escapeHtml(text) {
        var map = {
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#039;'
        };

        return text.replace(/[&<>"']/g, function (m) { return map[m]; });
    }


    function stripSlashes(str) {
        return str.replace(/\\/g, '');
    }