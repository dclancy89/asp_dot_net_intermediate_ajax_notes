$("#new_note").submit(function(e){
    e.preventDefault();
    var url = $(this).attr('action');
    var data = $(this).serialize();

    $.ajax({
		url: $(this).attr('action'),
		method: 'post',
		data: $(this).serialize(),
		success: function(serverResponse) {
			$('#notes').prepend(serverResponse);
			$('#new_note')[0].reset();
		}
	});
});

$("#notes").on("click", ".note p", function(e){
    e.preventDefault();

    $(this).parent().append("<textarea name='content'>" + $(this).text() + "</textarea>")
    $(this).remove();

});

$("#notes").on("focusout", ".note textarea", function(){
    var url = $(this).parent().attr('action') + "/update";
    var data = $(this).parent().serialize();
    $(this).parent().append("<p>" + $(this).val() + "</p>");
    $(this).remove();
    $.ajax({
		url: url,
		method: 'post',
		data: data,
		success: function(serverResponse) {
			console.log("note updated");
		}
	});
});

$("#notes").on("click", ".note input[type='submit']", function(e){
    e.preventDefault();
    var url = $(this).parent().attr('action') + "/delete";
    var data = $(this).parent().serialize();
    $(this).closest("form.note").remove();
    $.ajax({
		url: url,
		method: 'post',
		data: data,
		success: function(serverResponse) {
			console.log("note removed");
		}
	});

    
});