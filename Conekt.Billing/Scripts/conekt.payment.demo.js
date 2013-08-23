var demo = {
	create: function (model) {
		var $target = $('#demo-content');
		var $wizard = $('.demo-wizard');
		$wizard.spin();

		var json = JSON.stringify(model);
		$.ajax({
			url: '/demo/create',
			type: 'POST',
			data: json,
			contentType: 'application/json;charset=utf-8',
			success: function (data) {
				$wizard.spin(false);
				$wizard.modal('hide');
				$target.html(data);
				payments.tooltips();
			},
			error: function (jqXHR, textStatus, errorThrown) {
				$target.html(jqXHR.responseText);
			}
		});
	}
}