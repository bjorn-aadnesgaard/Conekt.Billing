var payments = {
	demo: function (model) {
		var $target = $("#demo-content");
		$target.spin();

		var json = JSON.stringify(model);
		$.ajax({
			url: '/demo/create',
			type: 'POST',
			data: json,
			contentType: 'application/json;charset=utf-8',
			success: function (data) {
				$target.html(data);
				payments.tooltips();
			},
			error: function (jqXHR, textStatus, errorThrown) {
				$target.html(jqXHR.responseText);
			}
		});
	},

	setup: function (model) {
		var $target = $("#conekt-payment-form");
		$target.spin();

		var json = JSON.stringify(model);
		$.ajax({
			url: '/payment/setup',
			type: 'POST',
			data: json,
			contentType: 'application/json;charset=utf-8',
			success: function (data) {
				$target.html(data);
				payments.tooltips();
			},
			error: function (jqXHR, textStatus, errorThrown) {
				$target.html(jqXHR.responseText);
			}
		});
	},

	process: function (model) {
		var json = JSON.stringify(model);
		$.ajax({
			url: '/payment/process',
			type: 'POST',
			data: json,
			contentType: 'application/json;charset=utf-8',
			success: function (data) {
				$paymentForm.html(data)
			},
			error: function (jqXHR, textStatus, errorThrown) {
				$spinTarget.spin(false);
				$submitButton.attr("disabled", false);
				$errorTarget.html(jqXHR.responseText);
			}
		});
	},

	tooltips: function () {
		$('body').tooltip({ selector: "[rel=tooltip]" });

		//Custom event for displaying/hiding popover. See http://stackoverflow.com/a/8948587/612113
		var isVisible = false;
		var clickedAway = false;
		$('[rel=popover]').popover({ html: true, trigger: 'manual' }).click(function (e) {
			$(this).popover('show');
			clickedAway = false
			isVisible = true
			e.preventDefault()
		});

		$(document).click(function (e) {
			if (isVisible & clickedAway) {
				$('[rel=popover]').popover('hide')
				isVisible = clickedAway = false
			}
			else {
				clickedAway = true
			}
		});
	},

	toggle: function (target, callback) {
		var $target = $(target);
		if ($target.is(":visible")) {
			$target.animate({ opacity: "0" }, 150, function () {
				$target.slideUp(150);
			});
		} else {
			$target.css({ opacity: '0' });
			$target.slideDown(150, function () {
				$target.animate({ opacity: "1" }, 150);
			});
		}

		if (callback && typeof (callback) === "function") {
			callback();
		};
	},

	trim: function (months, years) {
		var $months = $(months);
		var $years = $(years);
		var now = new Date();
		alert($years.val())
	}
};